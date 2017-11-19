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
    public class ReturnPlugin : ExprPlugin
    {
        /// <summary>
        /// Intialize.
        /// </summary>
        public ReturnPlugin()
        {
            this.ConfigureAsSystemExpression(false, true, "return");
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "return <expression> <statementterminator>";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "return 3;",
            "return 3\r\n",
            "return result",
            "return add(1,2)"
        };


        /// <summary>
        /// return value;
        /// </summary>
        /// <returns></returns>
        public override Expr  Parse()
        {
            var stmt = new ReturnExpr();
            _tokenIt.Expect(Tokens.Return);
            if (_tokenIt.IsEndOfStmtOrBlock())
                return stmt;

            var exp = _parser.ParseExpression(Terminators.ExpStatementEnd, passNewLine: false);
            stmt.Exp = exp;
            return stmt;
        }
    }



    /// <summary>
    /// For loop Expression data
    /// </summary>
    public class ReturnExpr : Expr
    {
        /// <summary>
        /// Return value.
        /// </summary>
        public Expr Exp;


        /// <summary>
        /// Execute the statement.
        /// </summary>
        public override object  DoEvaluate()
        {
            var parent = this.FindParent<FunctionExpr>();
            if (parent == null) throw new LangException("syntax error", "unable to return, parent not found", string.Empty, 0);

            var result = this.Exp == null ? LObjects.Null : Exp.Evaluate();
            var hasReturnVal = Exp != null;
            parent.Return(result, hasReturnVal);
            return LObjects.Null;
        }
    }
}
