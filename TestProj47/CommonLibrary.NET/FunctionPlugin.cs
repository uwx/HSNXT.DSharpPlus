﻿using System.Collections.Generic;
using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Parsing;
using HSNXT.ComLib.Lang.Types;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{
    /// <summary>
    /// Plugin for throwing errors from the script.
    /// </summary>
    public class FuncDeclarePlugin : ExprBlockPlugin, IParserCallbacks
    {
        /// <summary>
        /// Intialize.
        /// </summary>
        public FuncDeclarePlugin()
        {
            this.ConfigureAsSystemStatement(true, false, "function");
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "function ( <id> | <stringliteral> ) ( ',' ( <id> | <stringliteral> ) )* <statementblock>";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "function hour( num ) { ... }",
            "function hours, hour, hrs, hr( num ) { ... }",
            "function order_toBuy, 'order to buy'( num ) { ... }"
        };


        /// <summary>
        /// return value;
        /// </summary>
        /// <returns></returns>
        public override Expr Parse()
        {
            return Parse(_tokenIt.NextToken, true);
        }



        /// <summary>
        /// Parses a function declaration statement.
        /// This method is made public to allow other plugins to be used to allow different 
        /// words to represent "function" e.g. "def" instead of "function"
        /// </summary>
        /// <param name="token">The tokendata representing the starting token e.g. "function".</param>
        /// <param name="expectToken">Whether or not to expect the token in tokenData. 
        /// If false, advances the token iterator</param>
        /// <returns></returns>
        public Expr Parse(TokenData token, bool expectToken)
        {
            var stmt = new FuncDeclareExpr();
            stmt.Function.Ctx = Ctx;
            _parser.SetScriptPosition(stmt.Function, token);

            if (expectToken) _tokenIt.Expect(token.Token);
            else _tokenIt.Advance();

            // Function name.
            var name = _tokenIt.ExpectId(true, true);
            var aliases = new List<string>();
            var nextToken = _tokenIt.NextToken;
            List<string> argNames = null;

            // Option 1: Wild card 
            if (nextToken.Token == Tokens.Multiply)
            {
                stmt.Function.Meta.HasWildCard = true;
                nextToken = _tokenIt.Advance();
            }
            // Option 2: Aliases
            else if (nextToken.Token == Tokens.Comma)
            {
                // Collect all function aliases
                while (nextToken.Token == Tokens.Comma)
                {
                    _tokenIt.Advance();
                    var alias = _tokenIt.ExpectId(true, true);
                    aliases.Add(alias);
                    nextToken = _tokenIt.NextToken;
                }
                if (aliases.Count > 0)
                    stmt.Function.Meta.Aliases = aliases;
            }

            // Get the parameters.
            if (nextToken.Token == Tokens.LeftParenthesis)
            {
                _tokenIt.Expect(Tokens.LeftParenthesis);
                argNames = _parser.ParseNames();
                _tokenIt.Expect(Tokens.RightParenthesis);
            }

            stmt.Function.Meta.Init(name, argNames);
            
            // Now parser the function block.
            ParseBlock(stmt.Function);
            
            return stmt;
        }


        /// <summary>
        /// Parses a block by first pushing symbol scope and then popping after completion.
        /// </summary>
        public override void ParseBlock(BlockExpr stmt)
        {
            var fs = stmt as FunctionExpr;
            var funcName = fs.Name;
            
            // 1. Define the function in global symbol scope
            var funcSymbol = new SymbolFunction(fs.Meta);
            funcSymbol.FuncExpr = stmt;

            this.Ctx.Symbols.Define(funcSymbol);

            // 2. Define the aliases.
            if (!fs.Meta.Aliases.IsNullOrEmpty())
                foreach (var alias in fs.Meta.Aliases)
                    this.Ctx.Symbols.DefineAlias(alias, fs.Meta.Name);
            
            // 3. Push the current scope.
            this.Ctx.Symbols.Push(new SymbolsFunction(fs.Name), true);

            // 4. Register the parameter names in the symbol scope.
            if( !fs.Meta.Arguments.IsNullOrEmpty())
                foreach(var arg in fs.Meta.Arguments)
                    this.Ctx.Symbols.DefineVariable(arg.Name, LTypes.Object);

            stmt.SymScope = this.Ctx.Symbols.Current;
            _parser.ParseBlock(stmt);
            this.Ctx.Symbols.Pop();
        }


        /// <summary>
        /// Called by the framework after the parse method is called
        /// </summary>
        /// <param name="node">The node returned by this implementations Parse method</param>
        public void OnParseComplete(AstNode node)        
        {
            var function = (node as FuncDeclareExpr).Function;
            Ctx.Functions.Register(function.Name, function);

            // Register the functions aliases.
            if (function.Meta.Aliases != null && function.Meta.Aliases.Count > 0)
            {
                foreach (var alias in function.Meta.Aliases)
                {
                    Ctx.Functions.Register(alias, function);
                }
            }
        }
    }



    /// <summary>
    /// Represents a function declaration
    /// </summary>
    public class FuncDeclareExpr : BlockExpr
    {
        private readonly FunctionExpr _function = new FunctionExpr();


        /// <summary>
        /// Function 
        /// </summary>
        public FunctionExpr Function => _function;


        /// <summary>
        /// String representation
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="incrementTab"></param>
        /// <param name="includeNewLine"></param>
        /// <returns></returns>
        public override string AsString(string tab = "", bool incrementTab = false, bool includeNewLine = true)
        {
            return _function.AsString(tab, incrementTab, includeNewLine);
        }
    }
}
