using System.Collections.Generic;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Parsing;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{
    /// <summary>
    /// Combinator for handling comparisons.
    /// </summary>
    public class LexReplacePlugin : LexPlugin
    {        
        /// <summary>
        /// List of replacement word to their values.
        /// </summary>
        protected string[,] _replacements;


        /// <summary>
        /// A map of the replacement words to their value.
        /// </summary>
        protected Dictionary<string, string> _replaceMap;
        

        /// <summary>
        /// Initialize
        /// </summary>
        public LexReplacePlugin()
        {
            _canHandleToken = true;
        }


        /// <summary>
        /// Initialize multi-token replacements.
        /// </summary>
        /// <param name="replacements"></param>
        public void Init(string[,] replacements)
        {
            _replacements = replacements;
            _replaceMap = new Dictionary<string, string>();
            for(var ndx = 0; ndx < replacements.GetLength(0); ndx++)
            {
                var tokenToReplace = (string)replacements.GetValue(ndx, 0);
                var replaceVal = (string)replacements.GetValue(ndx, 1);
                _replaceMap[tokenToReplace] = replaceVal;
            }
        }


        /// <summary>
        /// run step 123.
        /// </summary>
        /// <returns></returns>
        public override Token[]  Parse()
        {
            var tokenText = _replaceMap[_lexer.LastTokenData.Token.Text];
            Token replacement = null;
            if(Core.Tokens.AllTokens.ContainsKey(tokenText))
            {
                replacement = Core.Tokens.AllTokens[tokenText];
                _lexer.LastTokenData.Token = replacement;
            }
            _lexer.ParsedTokens.Add(_lexer.LastTokenData);
            return new[] { replacement };
        }
    }
}
