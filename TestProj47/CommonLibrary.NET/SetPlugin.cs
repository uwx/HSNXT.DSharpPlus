// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>	
    // Set plugin allows using the word 'set' instead of 'var' when declaring variables
        
    set age = 33
    
    </doc:example>
    ***************************************************************************/

    /// <summary>
    /// Combinator for handling days of the week.
    /// </summary>
    public class SetPlugin : AliasTokenPlugin
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public SetPlugin() : base("set", Core.Tokens.Var )
        {
        }
    }
}
