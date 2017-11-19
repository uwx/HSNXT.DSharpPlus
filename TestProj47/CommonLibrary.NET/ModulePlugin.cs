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
    // Module plugin allows you to create modules ( namespaces ) with the word "mod"
    
    mod math
    {
        function min( a, b )
        {
            if( a <= b ) return a
            return b
        }
    }
    
    
    var min = math.min( 2, 3 )
    
    </doc:example>
    ***************************************************************************/
    /// <summary>
    /// Combinator for handling comparisons.
    /// </summary>
    public class ModulePlugin : ExprPlugin
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public ModulePlugin()
        {
            this.IsAutoMatched = true;
            this.StartTokens = new[] { "mod" };
            this.IsStatement = true;
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "mod '{' <statement_list> '}'";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "mod math { function inc( a ) { return a + 1; } }"
        };


        /// <summary>
        /// run step 123.
        /// </summary>
        /// <returns></returns>
        public override Expr Parse()
        {
            // Move past "mod"
            _tokenIt.Advance();

            // Get the name of the module "e.g." "math"
            var name = _tokenIt.ExpectId();

            // 1. Create the symbol to represent module
            var symbol = new SymbolModule();
            symbol.Name = name;
            symbol.Category = SymbolCategory.CustomScope;
            symbol.DataType = new LModuleType(name, name);
            symbol.Scope = new SymbolsNested(name);
            symbol.ParentScope = this.Ctx.Symbols.Current;

            // 2. Add the module symbol to the current scope
            this.Ctx.Symbols.Define(symbol);

            // 3. Now push the scope on top of the current scope. ( since modules can be nested )
            this.Ctx.Symbols.Push(symbol.Scope, true);

            var block = new BlockExpr();
            _parser.ParseBlock(block);
            this.Ctx.Symbols.Pop();
            return block;
        }
    }
}