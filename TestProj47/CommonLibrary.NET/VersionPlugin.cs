﻿using System.Text.RegularExpressions;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Parsing;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>	
    // Version plugin enables representation of versions using format 1.2.3.4.
    // This is particularily useful for when fluentscript is used for build automation.
    // e.g. 0.9.8.7
    
    version  = 0.9.8.7
    version2 = 0.9.8
    
    print( version.Major )
    print( version.Minor )
    print( version.Revision )
    print( version.Build )
    
    </doc:example>
    ***************************************************************************/

    /// <summary>
    /// Plugin allows emails without quotes such as john.doe@company.com
    /// </summary>
    public class VersionPlugin : LexPlugin
    {
        private const string _versionRegex = "^[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}(\\.[0-9]{1,4})?";


        /// <summary>
        /// Initialize
        /// </summary>
        public VersionPlugin()
        {
            _tokens = new[] { "$NumberToken" };
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}(\\.[0-9]{1,4})?";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "0.9.8.7",
            "2.9.3.355",
            "1.2.8"                    
        };


        /// <summary>
        /// Whether or not this uri plugin can handle the current token.
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public override bool CanHandle(Token current)
        {
            // Given 2.1.5.82
            // Current = 2.1
            if (_lexer.State.CurrentChar != '.') return false;
            var result = _lexer.PeekPostiveNumber();

            var versionText = current.Text + "." + result;
            if (Regex.IsMatch(versionText, _versionRegex))
                return true;

            return false;
        }


        /// <summary>
        /// run step 123.
        /// </summary>
        /// <returns></returns>
        public override Token[] Parse()
        {
            // http https ftp ftps www 
            var takeoverToken = _lexer.LastTokenData;
            var line = _lexer.LineNumber;
            var pos = _lexer.LineCharPos;

            _lexer.ReadChar();
            var token = _lexer.ReadNumber();
            var finalText = takeoverToken.Token.Text + "." + token.Text;
            var lineToken = Core.Tokens.ToLiteralVersion(finalText);
            var t = new TokenData { Token = lineToken, Line = line, LineCharPos = pos };
            _lexer.ParsedTokens.Add(t);
            return new[] { lineToken };
        }
    }
}
