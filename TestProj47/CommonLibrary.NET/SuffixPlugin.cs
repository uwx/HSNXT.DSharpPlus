// <lang:using>

using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Parsing;

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>	
    // Suffix plugin enables the use of functions as postfix operators on constants.
    
    // create hours with timespan object using number of hours supplied
    function hours( num )
    {
         return new TimeSpan( 0, num, 0 , 0 )
    }

    // create minutes with timespan object using number of minutes supplied
    function minutes( num )
    {
        return new TimeSpan(0, 0, num, 0 )
    }

    // timespan objects can be added together
    var time = 3 hours + 20 minutes;
     
    </doc:example>
    ***************************************************************************/

    /// <summary>
    /// Combinator for handling comparisons.
    /// </summary>
    public class SuffixPlugin : ExprPlugin
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public SuffixPlugin()
        {
            this.StartTokens = new[] { "$Suffix" };
            this.IsStatement = true;
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "<literal> <identifier>";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "3 shares",
            "20 products"
        };


        /// <summary>
        /// Whether or not this plugin can handle current token(s).
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public override bool CanHandle(Token current)
        {
            var t = _tokenIt.Peek(1, false);
            if (_parser.Context.Symbols.IsFunc(t.Token.Text))
                return true;
            return false;
        }


        /// <summary>
        /// Sorts expression
        /// </summary>
        /// <returns></returns>
        public override Expr Parse(object context)
        {
            var constExp = context as ConstantExpr;
            var ctx = _parser.Context;
            var c = _tokenIt.NextToken;
            var t = _tokenIt.Advance();

            // Get the function symbol.
            var fce = new FunctionCallExpr();
            fce.NameExp = new VariableExpr(t.Token.Text);
            fce.ParamListExpressions.Add(constExp);

            // Set the position of the exp.
            _parser.SetScriptPosition(constExp, c);
            constExp.Ctx = _parser.Context;
            
            // Move the postfix token.
            _tokenIt.Advance();

            return fce;
        }
    }
}