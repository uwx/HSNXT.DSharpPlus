﻿// <lang:using>

using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Parsing;
using HSNXT.ComLib.Lang.Types;

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>	
    // Terminates a program abruptly with a message.
    
    fail 'settings file not found, exiting application'
    
    fail "file : #{path} not found, exiting application"
    
    fail 0
    
    </doc:example>
    ***************************************************************************/

    /// <summary>
    /// Plugin for throwing errors from the script.
    /// </summary>
    public class FailPlugin : ExprPlugin
    {
        /// <summary>
        /// Intialize.
        /// </summary>
        public FailPlugin()
        {
            this.StartTokens = new[] { "fail" };
            this.IsStatement = true;
            this.IsAutoMatched = true;
            this.IsEndOfStatementRequired = true;
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "fail <expression> <statementterminator>";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "fail 'file not found';",
            "fail \"file #{filepath} not found\"\r\n"
        };


        /// <summary>
        /// throw error;
        /// </summary>
        /// <returns></returns>
        public override Expr Parse()
        {
            _tokenIt.ExpectIdText("fail");
            var exp = _parser.ParseExpression(Terminators.ExpStatementEnd);
            return new FailExpr { Exp = exp };
        }
    }



    /// <summary>
    /// For loop Expression data
    /// </summary>
    public class FailExpr : Expr
    {
        /// <summary>
        /// Name for the error in the catch clause.
        /// </summary>
        public Expr Exp;


        /// <summary>
        /// Execute
        /// </summary>
        public override object DoEvaluate()
        {
            object message = null;
            if (Exp != null)
                message = Exp.Evaluate();

            var text = (message == null || message == LObjects.Null)
                     ? ""
                     : ((LObject)message).GetValue().ToString();

            throw new LangFailException(text, this.Ref.ScriptName, this.Ref.Line);
        }
    }
}
