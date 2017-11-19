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
    // Return plugin provides return values
    
    return false;
    </doc:example>
    ***************************************************************************/

    /// <summary>
    /// Plugin for throwing errors from the script.
    /// </summary>
    public class ContinuePlugin : ExprPlugin
    {
        /// <summary>
        /// Intialize.
        /// </summary>
        public ContinuePlugin()
        {
            this.ConfigureAsSystemStatement(false, true, "continue");
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "continue <statementterminator>";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "continue;",
            "continue\r\n"
        };


        /// <summary>
        /// continue;
        /// </summary>
        /// <returns></returns>
        public override Expr Parse()
        {
            var expr = new ContinueExpr();
            _tokenIt.Expect(Tokens.Continue);
            return expr;
        }
    }



    /// <summary>
    /// For loop Expression data
    /// </summary>
    public class ContinueExpr : Expr
    {
        /// <summary>
        /// Execute the statement.
        /// </summary>
        public override object DoEvaluate()
        {
            var loop = this.FindParent<ILoop>();
            if (loop == null) throw new LangException("syntax error", "unable to break, loop not found", string.Empty, 0);

            loop.Continue();
            return LObjects.Null;
        }
    }
}
