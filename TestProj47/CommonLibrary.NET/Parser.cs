﻿using System;
using System.Collections.Generic;
using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Helpers;
using HSNXT.ComLib.Lang.Plugins;
using HSNXT.ComLib.Lang.Types;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Parsing
{
    /// <summary>
    /// Uses the Lexer to parse script in terms of sequences of Statements and Expressions;
    /// Each statement and expression is a sequence of Tokens( see Lexer )
    /// Main method is Parse(script) and ParseStatement();
    /// 
    /// 1. var name = "kishore";
    /// 2. if ( name == "kishore" ) print("true");
    /// 
    /// Statements:
    /// 
    /// VALUE:         TYPE:
    /// 1. AssignStmt ( "var name = "kishore"; )
    /// 2. IfStmt ( "if (name == "kishore" ) { print ("true"); }
    /// </summary>
    public class Parser : ParserBase, ILangParser
    {
        /// <summary>
        /// Initialize the context.
        /// </summary>
        /// <param name="context"></param>
        public Parser(Context context) : base(context)
        {      
            
        }


        #region Public API
        /// <summary>
        /// Initializes the parser with the script and setups various components.
        /// </summary>
        /// <param name="script"></param>
        /// <param name="memory"></param>
        public override void Init(string script, Memory memory)
        {
            // NOTE: Maybe move this setup code elsewhere ????

            // 1. Initalize data members
            base.Init(script, memory);

            // 2. Register default methods if not present.
            LTypesLookup.Init();
            Context.Methods.RegisterIfNotPresent(LTypes.Array, new LJSArrayMethods());
            Context.Methods.RegisterIfNotPresent(LTypes.Date,   new LJSDateMethods());
            Context.Methods.RegisterIfNotPresent(LTypes.String, new LJSStringMethods());
            Context.Methods.RegisterIfNotPresent(LTypes.Time, new LJSTimeMethods());
            Context.Methods.RegisterIfNotPresent(LTypes.Map, new LJSMapMethods());
            
            // 2. Convert script to sequence of tokens.
            Tokenize();

            // 3. Initialize the combinators.
            _context.Plugins.RegisterAllSystem();
            _context.Plugins.ForEach<IExprPlugin>(plugin => plugin.Init(this, _tokenIt));
            _context.Plugins.ForEach<ITokenPlugin>(plugin => plugin.Init(this, _tokenIt));
            _context.Plugins.ExecuteSetupPlugins(_context);

            // 4. Move to first token
            _tokenIt.Advance();
        }


        /// <summary>
        /// Parses the script into statements and expressions.
        /// </summary>
        /// <param name="script">Script text</param>
        /// <param name="memory">Memory scope object</param>
        public List<Expr> Parse(string script, Memory memory = null)
        {
            Init(script, memory);

            while (true)
            {
                if (_tokenIt.NextToken.Token == Tokens.EndToken)
                    break;

                if (_tokenIt.NextToken.Token != Tokens.NewLine)
                {
                    // Get next statement.
                    var stmt = ParseStatement();
                    
                    if (stmt != null)
                    {
                        // Limit case:
                        _context.Limits.CheckParserStatement(stmt);

                        // Add to list of statements.
                        _statements.Add(stmt);
                    }
                }
                else
                    _tokenIt.Advance();
            }
            return _statements;
        }


        /// <summary>
        /// Parses a statement.
        /// </summary>
        /// <returns></returns>
        public Expr ParseStatement()
        {
            _state.StatementNested++;
            Expr stmt = null;
            var stmtToken = _tokenIt.NextToken;
            var nexttoken = _tokenIt.NextToken.Token;

            _context.Limits.CheckParserStatementNested(_tokenIt.NextToken, _state.StatementNested);
            var hasTokenReplacePlugins = _context.Plugins.HasTokenBasedPlugins;

            var last = stmtToken;
            // The loop is here for new lines and comments.
            while (stmt == null && !_tokenIt.IsEnded)
            {
                if (nexttoken == Tokens.EndToken)
                    break;

                // Token replacements                
                if (hasTokenReplacePlugins && (nexttoken.Kind == TokenKind.Ident) && _context.Plugins.CanHandleTok(nexttoken, true))
                {
                    var plugin = _context.Plugins.LastMatchedTokenPlugin;
                    nexttoken = plugin.Parse();
                    _tokenIt.NextToken.Token = nexttoken;
                }

                // 1. System statements.
                if (_context.Plugins.CanHandleSysStmt(nexttoken))
                {
                    stmt = ParseSystemStatement();
                }
                // 2. Custom expressions/statements.
                else if (_context.Plugins.CanHandleStmt(nexttoken))
                {
                    stmt = ParseExtensionStatement();
                }
                // 3. Identifier based statements.
                else if (nexttoken.Kind == TokenKind.Ident)
                {
                    stmt = ParseIdBasedStatement();
                }
                else if (nexttoken.Kind == TokenKind.Comment)
                {
                    HandleComment(stmtToken, nexttoken);
                }
                else if (nexttoken == Tokens.NewLine)
                {
                    _tokenIt.Advance();
                }
                else if (nexttoken != Tokens.CommentMLine || nexttoken.Type == TokenTypes.Unknown)
                {
                    CollectError();
                    throw _tokenIt.BuildSyntaxUnexpectedTokenException();
                }
                if (stmt != null)
                {
                    // 1. Assign the symbol scope for this statement.
                    stmt.SymScope = this.Context.Symbols.Current;

                    // 2. Set the script position
                    SetScriptPosition(stmt, stmtToken);
                    stmt.Ctx = _context;

                    _state.LastStmt = stmt;
                    _state.StatementNested--;

                    // If function statement apply doc tags.
                    if (stmt is FuncDeclareExpr)
                        ApplyDocTagsToFunction(stmt);
                }
                stmtToken = _tokenIt.NextToken;
                nexttoken = _tokenIt.NextToken.Token;

                // Debuging: did not move past token.
                if (last == stmtToken)
                    throw _tokenIt.BuildSyntaxUnexpectedTokenException();
            }
            return stmt;
        }


        /// <summary>
        /// Parses expressions that are either built-in or possibly combinators/plugins.
        /// Examples include:
        /// 1. constant : 21, true, false, 'user01', null, 34.56
        /// 2. id       : currentUser
        /// 3. array    : [
        /// 4. map      : {
        /// 5. oper     : + - * / > >= == != ! etc.
        /// 6. new      : new 
        /// </summary>
        /// <remarks>
        /// This is one of the most important and relatively complex methods of the parser.
        /// This also needs to be fairly efficient and so the method is rather lengthy.
        /// </remarks>
        /// <param name="endTokens"></param>
        /// <param name="handleMathOperator">Whether or not to handle the mathematical expressions.</param>
        /// <param name="handleSingleExpression">Whether or not to handle only 1 expression.</param>
        /// <param name="enablePlugins">Whether or not the enable usage of plugins just for parsing current expression</param>
        /// <param name="passNewLine">Whether or not to pass the new line when advancing to the next token.</param>
        /// <param name="enableIdentTokenTextAsEndToken">Whether or not use ident tokens from the endTokens as expression terminators. This 
        /// is used for making fluent function calls where the parameter names terminate an expression.</param>
        /// <returns></returns>
        public Expr ParseExpression(IDictionary<Token, bool> endTokens, bool handleMathOperator = true, bool handleSingleExpression = false, bool enablePlugins = true, bool passNewLine = true, bool enableIdentTokenTextAsEndToken = false)
        {
            Expr exp = null, lastExp = null;
            var hasPlugins = _context.Plugins.TotalExpressions > 0;
            var hasLiteralBasedPlugins = _context.Plugins.HasLiteralTokenPlugins;
            var hasTokenReplacePlugins = _context.Plugins.HasTokenBasedPlugins;
            var expEndsInParenthesis = false;
            IDictionary<string, bool> identEndTokens = null;

            // For fluent function calls, this creates dictionary of strings ( ids )
            // representing the names of the functions parameters which will be used 
            // to terminate an expression.
            if (enableIdentTokenTextAsEndToken && endTokens != null)
            {
                identEndTokens = new Dictionary<string, bool>();
                foreach (var pair in endTokens)
                    if (pair.Key.Kind == TokenKind.Ident)
                        identEndTokens[pair.Key.Text] = true;
            }
            
            while (true)
            {
                var isUnknownToken = false;

                // CHECK_LIMITS: +1 because 0 based count
                _context.Limits.CheckParserExpression(_state.LastExpPart, _state.ExpressionCount + 1);

                // Break loop
                if (_tokenIt.IsEnded || IsEndOfExpressionPart(exp, endTokens, identEndTokens))
                    break;

                var token = _tokenIt.NextToken.Token;
                var tokenData = _tokenIt.NextToken;

                // Move past new lines.
                if (token == Tokens.NewLine)
                {
                    _tokenIt.Advance();
                    token = _tokenIt.NextToken.Token;
                    tokenData = _tokenIt.NextToken;
                }
                // OPTIMIZATION: 
                // If identifier followed by "." | "[" | "=" there is no need to check 
                // plugins. Checking plugins can be expensive. Avoid if applicable.
                // Case 1: user.  -> member access
                // Case 2: user[  -> array index access
                // Case 3: user = -> assignment
                var okToCheckPlugins = !IsExplicitIdentQualifierExpression(token);

                // Token replacements                
                if (okToCheckPlugins && enablePlugins && hasTokenReplacePlugins && _context.Plugins.CanHandleTok(token, true))
                {
                    var plugin = _context.Plugins.LastMatchedTokenPlugin;                    
                    token = plugin.Parse();
                    tokenData = _tokenIt.NextToken;
                    _tokenIt.NextToken.Token = token;
                }

                // PREVENT PLUGIN TAKEOVER ON IDENT BASED ENDTOKENS
                var next = _tokenIt.Peek();
                if (enableIdentTokenTextAsEndToken && next.Token.Kind == TokenKind.Ident && identEndTokens.ContainsKey(next.Token.Text))
                    okToCheckPlugins = false;

                
                // 1. Check for combinators.
                if (okToCheckPlugins && enablePlugins && hasPlugins && _context.Plugins.CanHandleExp(token))
                {
                    var combinator = _context.Plugins.LastMatchedExpressionPlugin;
                    exp = combinator.Parse();
                    _state.ExpressionCount++;
                }
                // CASE: array access on an indexable expression
                // description: this involves an index access on an expression that was initially not an identifier
                // examples: "abcd".length, [0, 1, 3].<method>
                else if (token == Tokens.LeftBracket && exp != null && (exp is IndexableExpr))
                {
                    _state.ExpressionCount++;
                    exp = ParseIdExpression(null, exp, true);
                }
                // CASE: dot access on a non-identifer based expression.
                // description: this involves member access on an expression that was initially not an identifier
                // examples: "abcd".length, [0, 1, 3].<method>
                else if (token == Tokens.Dot && exp != null)
                {
                    _state.ExpressionCount++;
                    exp = ParseIdExpression(null, exp, true);
                }
                // Error Check: Prevent consequtive expressions 
                // That are not bound together by some expression combination operator.
                // e.g. var result = a 2 is an error. consequite expressions a 2 do not make sense.
                //      var result = a + 2 | a < 2 | a && b would make sense.
                else if (lastExp != null && !Tokens.ExpressionCombinatorOps.ContainsKey(token.Text))
                {
                    throw _tokenIt.BuildSyntaxUnexpectedTokenException();
                }
                // CASE: Literal
                // description: Get any literal which is really a basic datatype
                // examples: 123, true, false, "john", null, new Date(2012, 9, 1), new Time(8, 30, 0)
                else if (token.IsLiteralAny())
                {
                    exp = token == Tokens.Null
                        ? new ConstantExpr(LObjects.Null)
                        : new ConstantExpr(token.Value);
                    _state.ExpressionCount++;
                    var expPosix = ParsePostfix(exp, enablePlugins, hasTokenReplacePlugins);

                    // Same literal so no postfix in which case advance.
                    if (exp == expPosix)
                        _tokenIt.Advance();
                    else
                        exp = expPosix;
                }
                // CASE: Identifier 
                // description: This will get any identifier based expression
                // examples: name, user.isactive, user.activate(), getuser()
                else if (token.Kind == TokenKind.Ident)
                {
                    exp = ParseIdExpression();
                    _state.ExpressionCount++;                    
                }
                // CASE: List 
                // description: This gets an array/list
                // examples: [ 1, 2, age, gettotal() ]
                else if (token == Tokens.LeftBracket)
                {
                    _state.ExpressionCount++;
                    exp = ParseArray();
                }
                // CASE: Map 
                // description: This gets an array/list
                // examples: [ 1, 2, age, gettotal() ]
                else if (token == Tokens.LeftBrace)
                {
                    _state.ExpressionCount++;
                    exp = ParseMap();
                }
                // Exp 5. Not !
                else if (token == Tokens.LogicalNot)
                {
                    var op = Operators.ToOp(token.Text);
                    _tokenIt.Advance();
                    var right = ParseExpression(endTokens);
                    exp = new UnaryExpr(string.Empty, right, op, _context);
                    SetScriptPosition(exp, tokenData);
                }
                // Exp 6. Symbol ( * / + - < <= > >= == != && || )
                // Also... symbol should not be a right parenthesis. ")"
                // Because this indicates a syntax error.
                // The ")" can only be handled by :
                // 1. conditional statement  : "if ( a < b )"
                // 2. shunting yard algorithm: "result = a * ( b + 3 )"
                else if (token.Kind == TokenKind.Symbol && handleMathOperator
                         && token != Tokens.RightParenthesis && Terminators.ExpMathShuntingYard.ContainsKey(token))
                {
                    if (exp != null) SetScriptPosition(exp, tokenData);
                    var result = ParseExpressionsWithPrecedence(endTokens, exp, true, identEndTokens, enableIdentTokenTextAsEndToken);
                    exp = result.Item2;
                    expEndsInParenthesis = result.Item1;
                }
                // Exp 7. Interpolated token "${first} name ${last} name".
                else if (token.Kind == TokenKind.Multi)
                {
                    exp = ParseInterpolatedExpression(token);
                    _state.ExpressionCount++;
                }
                else if (((token.Kind == TokenKind.Keyword) || (token.Kind == TokenKind.Symbol))
                    && token != Tokens.RightBracket && token != Tokens.RightBrace)
                {
                    throw _tokenIt.BuildSyntaxUnexpectedTokenException();
                }
                else // Unknown token. End here and allow other code to handle expected tokens.
                {
                    isUnknownToken = true;
                }
                SetScriptPosition(exp, tokenData);                                
                lastExp = exp;

                // Set the context on the expression.
                if ( exp != null ) exp.Ctx = _context;

                // Break loop based off of current token.
                if (handleSingleExpression || isUnknownToken || IsEndOfExpressionPart(exp, endTokens, identEndTokens))
                    break;
            }
            return exp;
        }


        /// <summary>
        /// Parses an interpolated token into a set of tokens making up an interpolated expression.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Expr ParseInterpolatedExpression(Token t)
        {
            var iexp = new InterpolatedExpr();

            // Convert each token in the interpolated string, need to convert it into
            // it's own expression.
            var tokens = t.Value as List<TokenData>;
            foreach (var tokenData in tokens)
            {
                var token = tokenData.Token;
                Expr exp = null;

                // 1. true / false / "name" / 123 / null;
                if (token.IsLiteralAny())
                {
                    exp = token == Tokens.Null
                        ? new ConstantExpr(LObjects.Null)
                        : new ConstantExpr(token.Value);
                    exp.Ctx = _context;
                    _state.ExpressionCount++;
                }
                // 2. ${first + 'abc'} or ${ result / 2 + max }
                else if (token.Kind == TokenKind.Multi)
                {
                    var tokenIterator = new TokenIterator();
                    var tokens2 = token.Value as List<TokenData>;
                    tokenIterator.Init(tokens2, 1, 100);
                    tokenIterator.Advance();
                    var exisiting = _tokenIt;

                    // a. Temporarily set the token iterator for the parser to the one for the interpolation.
                    _tokenIt = tokenIterator;

                    // b. Now parse only the tokens supplied.
                    exp = ParseExpression(null);

                    // c. Reset the token iterator to the global one for the entire script.
                    _tokenIt = exisiting;
                }
                iexp.Add(exp);
            }
            _tokenIt.Advance();
            return iexp;
        }


        /// <summary>
        /// Barses a block of code.
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public BlockExpr ParseBlock(BlockExpr block)
        {
            // { statemnt1; statement2; }
            var isMultiLine = false;

            // Check for single line block 
            if (_tokenIt.NextToken.Token == Tokens.NewLine)
                _tokenIt.Advance();

            if (_tokenIt.NextToken.Token == Tokens.LeftBrace) isMultiLine = true;
            if (block == null) block = new BlockExpr();

            // Case 1: Single line block.
            if (!isMultiLine)
            {
                var stmt = ParseStatement();
                stmt.Ctx = _context;
                stmt.Parent = block;
                block.Statements.Add(stmt);
                return block;
            }

            // Case 2: Multi-line block
            _tokenIt.Expect(Tokens.LeftBrace);
                        
            while (true)
            {
                // Check for end of statment or invalid end of script.
                if (IsEndOfStatementOrEndOfScript(Tokens.RightBrace))
                    break;

                
                // New line?
                if (_tokenIt.NextToken.Token == Tokens.NewLine)
                {
                    _tokenIt.Advance();
                    continue;
                }

                var stmt = ParseStatement();

                stmt.Parent = block;
                stmt.Ctx = _context;

                // Parse statement by statement.
                block.Statements.Add(stmt);
            }

            Expect(Tokens.RightBrace);

            return block;
        }


        /// <summary>
        /// Parses unary
        /// 1. ndx++
        /// 2. ndx--
        /// 3. ndx += 2 
        /// 4. ndx -= 2
        /// 5. ndx *= 2
        /// 6. ndx /= 2
        /// </summary>
        /// <param name="name"></param>
        /// <param name="useSemicolonAsTerminator"></param>
        /// <returns></returns>
        public Expr ParseUnary(string name, bool useSemicolonAsTerminator = true)
        {
            var op = Operators.ToOp(_tokenIt.NextToken.Token.Text);
            var opToken = _tokenIt.NextToken;
            double incrementValue = 1;            
            AssignExpr stmt = null;
            _tokenIt.Advance();

            // ++ -- 
            if (_tokenIt.IsEndOfStmtOrBlock())
            {
                var u1 = new UnaryExpr(name, incrementValue, op, _context);
                SetScriptPosition(u1, opToken);
                stmt = new AssignExpr(false, new VariableExpr(name), u1);
                _tokenIt.ExpectEndOfStmt();
            }
            else // += -= *= -=
            {
                var endTokens = useSemicolonAsTerminator ? Terminators.ExpStatementEnd : Terminators.ExpParenthesisEnd;
                var exp = ParseExpression(endTokens);
                var u2 = new UnaryExpr(name, exp, op, _context);
                SetScriptPosition(u2, opToken);
                stmt = new AssignExpr(false, new VariableExpr(name), u2);
            }
            stmt.Ctx = _context;
            return stmt;
        }


        /// <summary>
        /// Parses a conditional statement.
        /// </summary>
        /// <param name="stmt"></param>
        /// <param name="textToEndCondition"></param>
        /// <returns></returns>
        public ConditionalBlockExpr ParseConditionalStatement(ConditionalBlockExpr stmt, string textToEndCondition = "then")
        {
            if (stmt == null) stmt = new ConditionalBlockExpr(null, null);

            // Case 1: if ( <expression> ) 
            // Case 2: if   <expression> then
            var hasParenthesis = _tokenIt.NextToken.Token == Tokens.LeftParenthesis;
            var terminator = hasParenthesis ? Terminators.ExpParenthesisEnd : Terminators.ExpThenEnd;

            if (hasParenthesis)
                Expect(Tokens.LeftParenthesis);

            _state.Conditional++;

            var condition = ParseExpression(terminator, true);

            if (hasParenthesis)
                Expect(Tokens.RightParenthesis);
            else if (_tokenIt.NextToken.Token == Tokens.NewLine 
                || _tokenIt.NextToken.Token == Tokens.Then)
                _tokenIt.Advance();

            stmt.Condition = condition;
            stmt.Ctx = _context;
            _state.Conditional--;

            // Parse the block of statements.
            ParseBlock(stmt);
            return stmt;
        }
        #endregion


        #region Parse Statments
        /*
        private Stmt ParseStmt(Func<Stmt> action)
        {
            Stmt stmt = null;
            try
            {
                stmt = action();
            }
            catch (LangException ex)
            {
                _parseErrors.Add(ex);
                
                // Recover from error.
                // try going to the next end of statement ; newline end of script
                while(!_tokenIt.IsEndOfStmt())
                {
                    _tokenIt.Advance();
                }
                // Move past the new line
                _tokenIt.Advance();
            }
            return stmt;
        }
        */

        private Expr ParseSystemStatement()
        {
            var token = _tokenIt.NextToken.Token;
            var plugin = _context.Plugins.LastMatchedSysStmtPlugin;
            plugin.Ctx = _context;
            var stmt = plugin.Parse();

            // Allow the statement to do some post parsing actions.
            if (plugin is IParserCallbacks)
                ((IParserCallbacks)plugin).OnParseComplete(stmt);

            if (plugin.IsEndOfStatementRequired)
                _tokenIt.ExpectEndOfStmt();
            return stmt;        
        }


        /// <summary>
        /// Parses a combinator into a statement.
        /// </summary>
        /// <returns></returns>
        private Expr ParseExtensionStatement()
        {
            var token = _tokenIt.NextToken.Token;
            var plugin = _context.Plugins.LastMatchedExtStmtPlugin;
            var expPlugin = plugin;
            Expr result = null;
            if(!expPlugin.IsAssignmentSupported )
            {
                expPlugin.Ctx = _context;
                result = expPlugin.Parse();

                if (expPlugin is IParserCallbacks)
                    ((IParserCallbacks)expPlugin).OnParseComplete(result);

                if (expPlugin.IsEndOfStatementRequired)
                    _tokenIt.ExpectEndOfStmt();

                return result;
            }

            result = expPlugin.Parse();
            result.Ctx = _context;

            if (_tokenIt.NextToken.Token == Tokens.Assignment)
            {
                if (result is MemberAccessExpr)
                    ((MemberAccessExpr)result).IsAssignment = true;
                result = ParseAssignment(result);
            }
            //else
            //    stmt = new ExpressionStmt(exp);

            // This is a combinator expression that can also be a statement.
            // Since this is method ParseCombinatorStatement can only be called from ParseStatement,
            // this must be a statement. In which case, it must terminate.
            _tokenIt.ExpectEndOfStmt();
            return result;
        }


        /// <summary>
        /// Parses an id based statement.
        /// - user = 'john';
        /// - activateuser();
        /// - ndx++;
        /// 
        /// Complex:
        ///     - users[0] = new User();
        ///     - user.name = 'john';
        ///     - getuser().name = 'john';
        /// </summary>
        /// <returns></returns>
        private Expr ParseIdBasedStatement()
        {
            var tokenData = _tokenIt.NextToken;
            var name = tokenData.Token.Text;
            var exp = ParseIdExpression();
            if (exp is FunctionCallExpr)
            {
                _tokenIt.ExpectEndOfStmt();
                return exp;
            }
            var token = _tokenIt.NextToken.Token;
            Expr stmt = null;
            if (token == Tokens.Assignment)
            {
                stmt = ParseAssignment(exp);
            }
            else if (IsIncrementOp(token))  // ++ -- += *= /= -=
            {
                stmt = ParseUnary(name);                
            }
            if (stmt != null)
            {
                if (_tokenIt.IsEndOfStmtOrBlock())
                    _tokenIt.ExpectEndOfStmt();            
                return stmt;
            }

            throw _tokenIt.BuildSyntaxUnexpectedTokenException(tokenData);
        }


        /// <summary>
        /// Parses assignment.
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        private Expr ParseAssignment(Expr left)
        {
            var plugin = this.Context.Plugins.GetSysStmt(Tokens.Var) as VarPlugin;
            return plugin.Parse(left);            
        }


        /// <summary>
        /// This is an implementation of the Shunting Yard Algorithm to handle expressions
        /// with operator precedence.
        /// </summary>
        /// <param name="endTokens"></param>
        /// <param name="initial"></param>
        /// <param name="enableTokenPlugins"></param>
        /// <param name="identTokens"></param>
        /// <param name="enableIdentTokensAsEndTokens"></param>
        /// <returns></returns>
        private Tuple<bool, Expr> ParseExpressionsWithPrecedence(IDictionary<Token, bool> endTokens, Expr initial, 
            bool enableTokenPlugins = true, IDictionary<string, bool> identTokens = null, bool enableIdentTokensAsEndTokens = false)
        {
            _state.PrecedenceParseStackCount++;

            Expr finalExp = null;
            var token = _tokenIt.NextToken.Token;
            var tokenData = _tokenIt.NextToken;
            var ops = new List<TokenData>();
            var stack = new List<object>();
            if(initial != null) stack.Add(initial);
            int lastPrecendence = 0, leftParenCount = 0;
            var continueParsing = true;
            var hasTokenReplacePlugins = _context.Plugins.HasTokenBasedPlugins;
            Expr lastExpression = null;
            var current = token;

            while ((Terminators.ExpMathShuntingYard.ContainsKey(token) || continueParsing) && !_tokenIt.IsEnded)
            {
                var lastOpIndex = ops.Count - 1;
                var lastOp = lastOpIndex < 0 ? null : ops[lastOpIndex];

                if (IsEndOfExpressionPrecedence(token, leftParenCount, continueParsing, lastExpression))
                    break;

                var isOp = Operators.IsOp(token.Text);
                // Expression.
                if( !isOp || token == Tokens.LogicalNot )
                {
                    // Must be followed by an expression.
                    var exp = ParseExpression(endTokens, false, true);
                    lastExpression = exp;
                    stack.Add(exp);
                    continueParsing = false;
                } 
                // Operator ( * / + - ( ) > >= < <= == != && || 
                else if(isOp)
                {
                    continueParsing = token != Tokens.RightParenthesis;
                    // Get precedence of the current operator token.
                    var precendence = Operators.Precedence(token.Text);

                    // Need to always have the last precedence ( last op on the ops stack ).
                    if (ops.Count > 0) lastPrecendence = Operators.Precedence(ops[ops.Count - 1].Token.Text);

                    // 1st op.
                    if (ops.Count == 0)
                    {
                        ops.Add(tokenData);
                        if (token == Tokens.LeftParenthesis)
                        {
                            leftParenCount++;
                            //hasAtLeast1ParenthesisCapture = true;
                            _state.ParenthesisCount++;
                        }
                    }
                    // (
                    // This is the highest precedence.
                    else if (token == Tokens.LeftParenthesis)
                    {
                        ops.Add(tokenData);
                        leftParenCount++;
                        //hasAtLeast1ParenthesisCapture = true;
                        _state.ParenthesisCount++;
                    }
                    // Inside parenthesis ( last token "(" and current token is "+"
                    else if (lastOpIndex >= 0 && lastOp.Token == Tokens.LeftParenthesis && token != Tokens.RightParenthesis)
                    {
                        ops.Add(tokenData);
                    }
                    // ) 
                    // 1. Make sure parenthesesis count match up.
                    // 2. Restructure the postfix ??
                    else if (token == Tokens.RightParenthesis)
                    {
                        // Invalid.
                        if (leftParenCount == 0) throw _tokenIt.BuildSyntaxExpectedException(Tokens.LeftParenthesis.Text);

                        // Keep popping operators off the operator stack until "(" is hit.
                        // Then remove the ")"
                        var lastIndexToPop = ops.Count - 1;
                        var foundParen = false;
                        while (lastIndexToPop >= 0)
                        {
                            if (ops[lastIndexToPop].Token == Tokens.LeftParenthesis)
                            {
                                foundParen = true;
                                break;
                            }
                            var op = ops[lastIndexToPop];
                            ops.RemoveAt(lastIndexToPop);
                            stack.Add(op);
                            lastIndexToPop--;
                        }
                        if (!foundParen) throw _tokenIt.BuildSyntaxExpectedException(Tokens.LeftParenthesis.Text);
                        // Get rid of "(" on the op stack.
                        ops.RemoveAt(lastIndexToPop);
                        leftParenCount--;
                        _state.ParenthesisCount--;
                    }
                    // * / higher than + -
                    // Add operator to postfix stack
                    else if (precendence > lastPrecendence)
                    {
                        //stack.Add(tokenData);
                        ops.Add(tokenData);
                    }
                    // * / have same precedence.
                    // 1. Move the previous op to postfix stack.
                    // 2. Add the current op to the ops stack.
                    else if (precendence <= lastPrecendence)
                    {
                        // * / have same precedence so add last from ops into stack.                    
                        var op = ops[lastOpIndex];
                        ops.RemoveAt(lastOpIndex);
                        stack.Add(op);
                        ops.Add(tokenData);
                    }
                }
                
                // Operator token e.g. < > && || + etc.
                // We did not move the token forward but only add the operator token to the stack/ops.
                if (current == _tokenIt.NextToken.Token)
                    _tokenIt.Advance();

                // Ok to skip newlines?
                if( endTokens != null && !endTokens.ContainsKey(Tokens.NewLine))
                    _tokenIt.AdvancePastNewLines();

                current = _tokenIt.NextToken.Token;
                
                // This handles token replacements for operators.
                // Start:
                var replaceToken = false;
                ITokenPlugin tokenPlugin = null;
 
                // Token replacements                
                if (current != Tokens.EndToken && enableTokenPlugins && hasTokenReplacePlugins && _context.Plugins.CanHandleTok(current, false))
                {
                    tokenPlugin = _context.Plugins.LastMatchedTokenPlugin;
                    current = tokenPlugin.Peek();
                    replaceToken = true;
                }
                // End

                if (IsEndOfExpressionPrecedence(current, leftParenCount, continueParsing, lastExpression))
                    break;
                
                // Now move to next token.
                if (replaceToken)
                {
                    var replacement = tokenPlugin.Parse(false, 1);
                    tokenData = _tokenIt.NextToken;
                    _tokenIt.NextToken.Token = replacement;
                    token = replacement;
                }
                else
                {
                    tokenData = _tokenIt.NextToken;
                    token = _tokenIt.NextToken.Token;
                }
            }

            // Last rule ops left ?
            if (ops.Count > 0)
                for(var ndx = ops.Count - 1; ndx >= 0; ndx--)
                    stack.Add(ops[ndx]);

            finalExp = LangHelper.ProcessShuntingYardList(_context, this, stack);

            var endsInParenthesis = false;
            _state.PrecedenceParseStackCount--;

            return new Tuple<bool, Expr>(endsInParenthesis, finalExp);
        }


        private bool IsEndOfExpressionPart(Expr lastExp, IDictionary<Token, bool> endTokens, IDictionary<string, bool> identEndTokens)
        {
            // End of script ?
            var token = _tokenIt.NextToken.Token;
            if (token == Tokens.EndToken) return true;
            if (endTokens == null) return false;
            var isend = endTokens.ContainsKey(token);

            // Check if identTokens supplied.
            if (!isend && identEndTokens != null && identEndTokens.Count > 0)
                if (token.Kind == TokenKind.Ident && identEndTokens.ContainsKey(token.Text))
                    isend = true;

            return isend;
        }


        private bool IsEndOfExpressionPrecedence(Token token, int leftParenCount, bool continueParsing, Expr lastExpression)
        {
            // Is the token a valid operator that has precedence ?
            // Should continue parsing 
            if ((!Terminators.ExpMathShuntingYard.ContainsKey(token) && !continueParsing)) return true;
            
            // Termination case 1: Avoid handling ) when doing math expressions inside a function call.
            // Termination case 2: Avoid handling ) of an if / while statement.            
            if (token == Tokens.RightParenthesis)
            {                
                if (leftParenCount == 0) return true;
            }
            
            // Case 3: End token - invalid script.
            if (token == Tokens.EndToken) return true;

            return false;
        }


        /// <summary>
        /// [ "user01", true, false, 123, 45.6, 'company.com']
        /// </summary>
        /// <returns></returns>
        private Expr ParseArray()
        {
            // Validate.
            Expect(Tokens.LeftBracket);

            // list of each item in the array.
            var items = new List<Expr>();

            while (true)
            {
                // Stop when ] is hit
                if (IsEndOfStatementOrEndOfScript(Tokens.RightBracket))
                    break;

                if (_tokenIt.NextToken.Token == Tokens.NewLine)
                    _tokenIt.Advance();

                // This is an empty item ',,'
                if (_tokenIt.NextToken.Token == Tokens.Comma)
                    items.Add(null);
                else
                {
                    var exp = ParseExpression(Terminators.ExpArrayDeclareEnd);
                    items.Add(exp);
                }
                // More items? 
                if (_tokenIt.NextToken.Token == Tokens.Comma)
                    _tokenIt.Advance();
            }
            Expect(Tokens.RightBracket);
            return new DataTypeExpr(items);
        }


        /// <summary>
        /// { Name: "user01", IsActive: true, IsAdmin: false, Id: 123, Sales: 45.6, Company: 'company.com' }
        /// </summary>
        /// <returns></returns>
        private Expr ParseMap()
        {
            // Validate
            Expect(Tokens.LeftBrace);
            var items = new List<Tuple<string, Expr>>();

            while (true)
            {
                // Stop when } is hit
                if (IsEndOfStatementOrEndOfScript(Tokens.RightBrace))
                    break;

                if (_tokenIt.NextToken.Token == Tokens.NewLine)
                    _tokenIt.Advance();

                var token = _tokenIt.NextToken.Token;

                // Check for error: Format must be <key> : <value>
                // Example 1: "Name" : "kishore" 
                // Example 2: Name   : "kishore"
                var key = string.Empty;
                if (!(token.IsLiteralAny() || token.Kind == TokenKind.Ident))
                {
                    throw _tokenIt.BuildSyntaxExpectedException("Text based key for map");
                }

                // 1. key : 
                key = token.Text;
                _tokenIt.AdvanceAndExpect(Tokens.Colon);

                // 3. Get value (expression)
                var exp = ParseExpression(Terminators.ExpMapDeclareEnd);
                items.Add(new Tuple<string, Expr>(key, exp));

                // More items?
                if (_tokenIt.NextToken.Token == Tokens.Comma)
                    _tokenIt.Advance();
            }
            Expect(Tokens.RightBrace);
            return new DataTypeExpr(items);
        }


        /// <summary>
        /// Parses function expression :
        /// 1. getAdminUser()
        /// 2. getUser(1)
        /// 3. getUserByNameOrEmail("user01", "kishore@company.com")
        /// </summary>
        /// <param name="nameExp">Expression representing the function name.</param>
        /// <param name="members">The individual members if function call on a member expression e.g. math.min(1,2);</param>
        /// <returns></returns>
        public Expr ParseFuncExpression(Expr nameExp, List<string> members)
        {
            // Validate
            var funcExp = new FunctionCallExpr();
            funcExp.NameExp = nameExp;
            funcExp.ParamMap = new Dictionary<string, object>();
            funcExp.Ctx = _context; 
            _state.FunctionCall++;
            _context.Limits.CheckParserFuncCallNested(_tokenIt.NextToken, _state.FunctionCall);

            // Check for optional parenthesis.
            var expectParenthesis = _tokenIt.NextToken.Token == Tokens.LeftParenthesis;

            // Handle named parameters only for internal functions right now.
            var fname = nameExp.ToQualifiedName();
            var hasMemberAccess = false; // members != null && members.Count > 1;

            // Case 1: External c# function.
            if (_context.ExternalFunctions.Contains(fname))
            {
                ParseParameters(funcExp, expectParenthesis, false, !expectParenthesis);
            }
            // Case 2: Internal function in global namespace.
            else if(!hasMemberAccess)
            {
                FunctionMetaData meta = null;
                if(_context.Functions.Contains(fname))
                    meta = _context.Functions.GetByName(fname).Meta;
                FluentHelper.ParseFuncParameters(funcExp.ParamListExpressions, _tokenIt, this, expectParenthesis, !expectParenthesis, meta);
            }
            // Case 3: Member acccess
            else if (hasMemberAccess)
            {
                var symScope = this._context.Symbols.Current;
                FunctionMetaData meta = null;
                FunctionExpr fexpr = null;
                for (var ndx = 0; ndx < members.Count; ndx++)
                {
                    var member = members[ndx];
                    
                    // module ?
                    var sym = symScope.GetSymbol(member);
                    if( sym.Category == SymbolCategory.CustomScope)
                    {
                        symScope = ((SymbolModule)sym).Scope;
                    }
                    // last one ?
                    else if (sym.Category == SymbolCategory.Func)
                    {
                        meta = ((SymbolFunction)sym).Meta;
                        break;
                    }
                }
                funcExp.Function = fexpr;
                FluentHelper.ParseFuncParameters(funcExp.ParamListExpressions, _tokenIt, this, expectParenthesis, !expectParenthesis, meta); 
            }
            _state.FunctionCall--;
            return funcExp;
        }


        /// <summary>
        /// Parses an Id based expression:
        /// 1. user         : variable
        /// 2. getUser()    : function call
        /// 3. users[       : index expression
        /// 4. user.name    : member access
        /// 
        /// ASSIGNMENT:						EXPRESSION:						
        /// result = 2;						result					-> variableexpression				
        /// items[0] = 'kishore';			items[0]				-> indexexpression( variableexpression  | name)
        /// getuser();					    getuser()				-> functionexpression()
        /// user.age = 30;					user.age				-> memberexpression( variableexpression | name/member )
        /// items[0].name = 'kishore';		items[0].name			-> memberexpression( indexexpression( variableexpression ) )
        /// getuser()[0] = 0;				getuser()[0]			-> indexexpression( functionexpression )
        /// user.name.last = 'kishore';		user.name.last			-> memberexpression( memberexpression )
        /// </summary>
        /// <param name="name"></param>
        /// <param name="existing"></param>
        /// <param name="isCurrentTokenAMember">Whether or not the current token is a '[', '(' or a '.'</param>
        /// <returns></returns>
        public Expr ParseIdExpression(string name = null, Expr existing = null, bool isCurrentTokenAMember = false)
        {
            var exp = existing;
            var aheadToken = isCurrentTokenAMember ? _tokenIt.NextToken : _tokenIt.Peek();
            var currentName = "";
            if (existing == null)
            {
                currentName = string.IsNullOrEmpty(name) ? _tokenIt.NextToken.Token.Text : name;
                exp = new VariableExpr(currentName);
                exp.SymScope = _context.Symbols.Current;
                exp.Ctx = _context;
            }
            if(!isCurrentTokenAMember)
                _tokenIt.Advance();   
             
            var memberAccess = 0;
            var isFunction = (exp is VariableExpr && _context.Symbols.IsFunc(((VariableExpr)exp).Name));
            
            // CASE 1: function call without out parenthesis.
            if (isFunction && aheadToken.Token != Tokens.LeftParenthesis)
            {
                exp = ParseFuncExpression(exp, null);
                exp.Ctx = _context;
                return exp;
            }

            // CASE 2: Simple variable expression - e.g. result + 2
            var isMemberAccessAhead = ( aheadToken.Token == Tokens.LeftParenthesis || aheadToken.Token == Tokens.LeftBracket || aheadToken.Token == Tokens.Dot );
            if( !isFunction && !isMemberAccessAhead )
            {
                exp.Ctx = _context;
                return exp;
            }
           
            // CASE 3: Member access of some sort using either "." | "[]" | "()"
            // 1. result.total  : Dot access
            // 2. result[0]     : Array access
            // 3. add( 2, 3 )   : Function access/call
            var tokenData = _tokenIt.NextToken;
            var token = _tokenIt.NextToken.Token;
            var members = new List<string>();
            members.Add(exp.ToQualifiedName());

            while (token == Tokens.LeftParenthesis || token == Tokens.LeftBracket || token == Tokens.Dot)
            {
                // Case 2: "("- function call
                if (token == Tokens.LeftParenthesis)
                {
                    exp = ParseFuncExpression(exp, members); 
                }

                // Case 3: "[" - indexing
                else if (token == Tokens.LeftBracket)
                {
                    _tokenIt.Advance();
                    _state.IndexExp++;

                    // Get index exp ( n+1 ) or n, etc.
                    var index = ParseExpression(Terminators.ExpBracketEnd);
                    _tokenIt.Expect(Tokens.RightBracket);

                    _state.IndexExp--;
                    // Note: if = sign then property access should return a property info.
                    // otherwise get the value of the property.
                    var isAssignment = _tokenIt.NextToken.Token == Tokens.Assignment;
                    exp = new IndexExpr(exp, index, isAssignment);
                }
            
                // Case 4: "." - member access
                else if (_tokenIt.NextToken.Token == Tokens.Dot)
                {
                    _tokenIt.Advance();
                    var member = _tokenIt.ExpectId();

                    // Keep list of each member name.
                    members.Add(member);

                    // Note: if = sign then property access should return a property info.
                    // otherwise get the value of the property.
                    var isAssignment = _tokenIt.NextToken.Token == Tokens.Assignment;
                    exp = new MemberAccessExpr(exp, member, isAssignment);
                    //exp.SymScope = _context.Symbols.Current;
                }
                exp.Ctx = _context;
                SetScriptPosition(exp, tokenData);
                memberAccess++;

                // Check limit.
                _context.Limits.CheckParserMemberAccess(exp, memberAccess);
                tokenData = _tokenIt.NextToken;
                token = tokenData.Token;
            }
            return exp;
        }


        /// <summary>
        /// Parses all the dot "." members. e.g. "user.address.name"
        /// </summary>
        /// <param name="rootName"></param>
        /// <returns></returns>
        public DotAccess ParseDotAccess(string rootName)
        {
            var tokenData = _tokenIt.NextToken;
            var token = tokenData.Token;
            var dotAccess = new DotAccess();
            dotAccess.RootName = rootName;
            dotAccess.RootScope = _context.Symbols.Global;

            // Keep processing members until "." is not there.
            while (token == Tokens.Dot && !_tokenIt.IsEnded )
            {                
                // 1. Move past "."
                _tokenIt.Advance();

                // 2. Expect an identifier.
                var member = _tokenIt.ExpectId();

                // 3. Keep list of each member name ( after the first one )
                dotAccess.Members.Add(member);

                // 4. if = sign then property access should return a property info.
                // otherwise get the value of the property.
                var isAssignment = _tokenIt.NextToken.Token == Tokens.Assignment;

                // 5. Reference the next tokens.
                tokenData = _tokenIt.NextToken;
                token = tokenData.Token;
            }
            return dotAccess;
        }
        #endregion


        /// <summary>
        /// Parses parameters.
        /// </summary>
        /// <param name="pexp">The expression that can hold parameters.</param>
        /// <param name="expectParenthesis">Whether or not to expect parenthis to designate the start of the parameters.</param>
        /// <param name="advanceAfter">Whether or not to advance after the ending left parenthesis</param>
        /// <param name="enableNewLineAsEnd">Whether or not to treat a newline as end</param>
        public void ParseParameters(IParameterExpression pexp, bool expectParenthesis, bool advanceAfter, bool enableNewLineAsEnd = false)
        {
            var totalParameters = 0;
            if (_tokenIt.NextToken.Token == Tokens.LeftParenthesis)
                expectParenthesis = true;

            if(expectParenthesis)
                _tokenIt.Expect(Tokens.LeftParenthesis);

            var passNewLine = !enableNewLineAsEnd;
            var endTokens = enableNewLineAsEnd ? Terminators.ExpFluentFuncExpParenEnd : Terminators.ExpFuncExpEnd;
            while (true)
            {
                // Check for end of statment or invalid end of script.
                if (IsEndOfParameterList(Tokens.RightParenthesis, enableNewLineAsEnd))
                    break;

                if (_tokenIt.NextToken.Token == Tokens.Comma) 
                    _tokenIt.Advance();
                                 
                var exp = ParseExpression(endTokens, true, false, true, passNewLine);
                pexp.ParamListExpressions.Add(exp);

                totalParameters++;
                _context.Limits.CheckParserFunctionParams(exp, totalParameters);

                // Check for end of statment or invalid end of script.
                if (IsEndOfParameterList(Tokens.RightParenthesis, enableNewLineAsEnd))
                    break;

                // Advance.
                Expect(Tokens.Comma);
            }
            if(expectParenthesis)
                _tokenIt.Expect(Tokens.RightParenthesis);
        }


        /// <summary>
        /// Is end of parameter list.
        /// </summary>
        /// <returns></returns>
        public bool IsEndOfParameterList(Token endToken, bool enableNewLineAsEnd)
        {
            var next = _tokenIt.NextToken.Token;
            var last = _tokenIt.LastToken.Token;

            // Copied code... to avoid 2 function calls.
            if (next == endToken) return true;
            if (next == Tokens.Semicolon) return true;
            if (next == Tokens.EndToken) return true;

            if(enableNewLineAsEnd)
                if (last != Tokens.Comma && next == Tokens.NewLine) return true;

            return false;
        }


        /// <summary>
        /// Whether or not the token represents the start of an explicit ident expressions e.g. ident( "." | "[" | "=" )
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsExplicitIdentQualifierExpression(Token token)
        {            
            if (!(token.Kind == TokenKind.Ident)) return false;
            
            // Check if variable.
            var isVar = _context.Symbols.IsVar(token.Text);
            var ahead = _tokenIt.Peek(1, false);

            // variable member access, index access, assignment
            if (isVar && ( ahead.Token == Tokens.Dot || ahead.Token == Tokens.LeftBracket || ahead.Token == Tokens.Assignment))
                return true;

            // function call
            if (ahead.Token == Tokens.LeftParenthesis && _context.Symbols.IsFunc(token.Text))
                return true;

            return false;
        }


        /// <summary>
        /// Whether or not the token supplied is an increment operator.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected bool IsIncrementOp(Token token)
        {
            if (token == Tokens.Increment || token == Tokens.Decrement || token == Tokens.IncrementAdd ||
                 token == Tokens.IncrementDivide || token == Tokens.IncrementMultiply || token == Tokens.IncrementSubtract)
                return true;
            return false;
        }


        /// <summary>
        /// Parses a postfix function call
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="enablePlugins">Whether or not plugins are enabled</param>
        /// <param name="hasTokenReplacePlugins"></param>
        /// <param name="isCurrentToken">Whether or not hte current token in the token iterator is the current token to use for checking for postfix plugins</param>
        /// <returns></returns>
        protected Expr ParsePostfix(Expr exp, bool enablePlugins, bool hasTokenReplacePlugins, bool isCurrentToken = false)
        {            
            var next = isCurrentToken ? _tokenIt.NextToken : _tokenIt.Peek(1, false);

            // Validate.
            // 1. Not an id token then it doesn't represent a function.
            // 2. The function does not exist?
            //if (!(t.Token.Kind == TokenKind.Ident)) return exp;
            if (enablePlugins && hasTokenReplacePlugins && _context.Plugins.CanHandleTok(next.Token, false))
                return exp;

            var exprPlugin = _context.Plugins.GetPostFix(next.Token);
            if (exprPlugin == null)
                return exp;

            var finalExp = exprPlugin.Parse(exp);
            return finalExp;
        }
    }
}
