// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>	
    // Bool plugin allows aliases for true/false
    
    var result = on;
    var result = off;
    var result = yes;
    var result = no;
    </doc:example>
    ***************************************************************************/
    
    
    /// <summary>
    /// Combinator for handling days of the week.
    /// </summary>
    public class BoolPlugin : AliasTokenPlugin
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public BoolPlugin()
            : base("yes", Core.Tokens.True)
        {
            Register("Yes", Core.Tokens.True);
            Register("on",  Core.Tokens.True);
            Register("On",  Core.Tokens.True);
            Register("no",  Core.Tokens.False);
            Register("No",  Core.Tokens.False); 
            Register("off", Core.Tokens.False);
            Register("Off", Core.Tokens.False);            
            _tokens = new[] { "yes", "Yes", "no", "No", "on", "On", "off", "Off" };
        }
    }
}
