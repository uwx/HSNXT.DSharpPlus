// <lang:using>

using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Parsing;
using HSNXT.ComLib.Lang.Types;

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>	
    // Throw plugin provides throwing of errors from the script.
    
    throw 'user name is required';
    </doc:example>
    ***************************************************************************/

    /// <summary>
    /// Plugin for throwing errors from the script.
    /// </summary>
    public class ThrowPlugin : ExprPlugin
    {
        /// <summary>
        /// Intialize.
        /// </summary>
        public ThrowPlugin()
        {
            this.ConfigureAsSystemStatement(false, true, "throw");
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "throw <expression> <statementterminator>";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "throw 'invalid amount';",
            "throw 300\r\n"
        };


        /// <summary>
        /// throw error;
        /// </summary>
        /// <returns></returns>
        public override Expr Parse()
        {
             _tokenIt.Expect(Tokens.Throw);
            var exp = _parser.ParseExpression(Terminators.ExpStatementEnd, passNewLine: true);
            return new ThrowExpr { Exp = exp };
        }
    }



    /// <summary>
    /// For loop Expression data
    /// </summary>
    public class ThrowExpr : Expr
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
            var message = "";
            if (Exp != null)
            {
                var result = Exp.Evaluate() as LObject;
                if (result != LObjects.Null)
                    message = result.GetValue().ToString();
            }

            throw new LangException("TypeError", message, this.Ref.ScriptName, this.Ref.Line);
        }
    }
}
