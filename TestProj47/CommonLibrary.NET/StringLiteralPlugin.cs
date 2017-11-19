// <lang:using>

using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Parsing;

// </lang:using>


namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>	
    // Allows using ruby-style string literals such as :user01 where :user01 equals 'user01'
    
    name = :user01
    lang = :fluentscript
    
    if( :batman == 'batman' ) print( "works" );
    
    </doc:example>
    ***************************************************************************/

    /// <summary>
    /// Plugin allows emails without quotes such as john.doe@company.com
    /// </summary>
    public class StringLiteralPlugin : ExprPlugin
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public StringLiteralPlugin()
        {
            this.Precedence = 1;
            this.IsAutoMatched = true;
            this.StartTokens = new[] { ":" };
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => ":username";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "var name = :kishore",
            "var lang = :fluent_script"
        };


        /// <summary>
        /// Whether or not this parser can handle the supplied token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public override bool CanHandle(Token token)
        {
            var next = _tokenIt.Peek();
            if (next.Token.Kind == TokenKind.Ident) 
                return true;
            return false;
        }


        /// <summary>
        /// Parses the day expression.
        /// Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
        /// </summary>
        /// <returns></returns>
        public override Expr Parse()
        {
            _tokenIt.Advance();
            var word = _tokenIt.NextToken.Token.Text;
            _tokenIt.Advance();
            return new ConstantExpr(word);
        }
    }
}
