﻿using System.Collections.Generic;
using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Helpers;
using HSNXT.ComLib.Lang.Parsing;
using HSNXT.ComLib.Lang.Types;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{
    /// <summary>
    /// Plugin for throwing errors from the script.
    /// </summary>
    public class IfPlugin : ExprBlockPlugin
    {
        /// <summary>
        /// Intialize.
        /// </summary>
        public IfPlugin()
        {
            this.ConfigureAsSystemStatement(true, false, "if");
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "if ( ( <expression> then <statementblock> ) | ( '(' <expression> ')' <statementblock> ) )";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "if true then print hi",
            "if ( total > 10 ) print hi",
            "if ( isActive && age >= 21  ) { print('fluent'); print('script'); }"
        };


        /// <summary>
        /// return value;
        /// </summary>
        /// <returns></returns>
        public override Expr Parse()
        {            
            var stmt = new IfExpr();
            var statements = new List<Expr>();

            // While ( condition expression )
            _tokenIt.Expect(Tokens.If);

            // Parse the if
            ParseConditionalBlock(stmt);

            // Handle "else if" and/or else
            if (_tokenIt.NextToken.Token == Tokens.Else)
            {
                // _tokenIt.NextToken = "else"
                _tokenIt.Advance();

                // What's after else? 
                // 1. "if"      = else if statement
                // 2. "{"       = multi  line else
                // 3. "nothing" = single line else
                // Peek 2nd token for else if.
                var token = _tokenIt.NextToken;
                if (_tokenIt.NextToken.Token == Tokens.If)
                {
                    stmt.Else = Parse() as BlockExpr;
                }
                else // Multi-line or single line else
                {
                    var elseStmt = new BlockExpr();
                    ParseBlock(elseStmt);
                    elseStmt.Ctx = Ctx;
                    stmt.Else = elseStmt;
                    _parser.SetScriptPosition(stmt.Else, token);
                }
            }
            return stmt;
        }
    }



    /// <summary>
    /// For loop Expression data
    /// </summary>
    public class IfExpr : ConditionalBlockExpr
    {
        /// <summary>
        /// Create new instance
        /// </summary>
        public IfExpr() : base(null, null) { }


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="condition"></param>
        public IfExpr(Expr condition)
            : base(condition, null)
        {
            InitBoundary(true, "}");            
        }


        /// <summary>
        /// Else statement.
        /// </summary>
        public BlockExpr Else;



        /// <summary>
        /// Execute
        /// </summary>
        public override object DoEvaluate()
        {
            // Case 1: If is true
            if (this.Condition.EvaluateAs<bool>())
            {
                LangHelper.Evaluate(_statements, this);
            }
            // Case 2: Else available to execute
            else if (Else != null)
            {
                Else.Evaluate();
            }
            return LObjects.Null;
        }
    }    
}
