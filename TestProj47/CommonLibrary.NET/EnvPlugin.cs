using System;
using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Parsing;
using HSNXT.ComLib.Lang.Types;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>	
    // Env plugin allows direct access to environment variables via the "env" object.
    
    // Example 1: Access to user variables only via the ".user" property of env.
    result = env.user.path;
    
    // Example 2: Access to system variables via the ".sys" property of env.
    result = env.sys.path;
    
    // Example 3: Access to environment variable without specifying sys or user.
    result = env.path;
    result = env.SystemRoot;
    </doc:example>
    ***************************************************************************/
    
    /// <summary>
    /// Token specifically for referencing environment variables.
    /// </summary>
    public class EnvToken : Token
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="name"></param>
        public EnvToken(string scope, string name) : base(TokenKind.Other, 1001, string.Empty, string.Empty)
        {
            VarName = name;
            Scope = scope;

            if (!string.IsNullOrEmpty(scope))
                this._text = "env." + scope + "." + name;
            else
                this._text = "env." + name;            
        }


        /// <summary>
        /// Either sys or user
        /// </summary>
        public string Scope;


        /// <summary>
        /// The variable name.
        /// </summary>
        public string VarName;
    }



    /// <summary>
    /// Plugin allows emails without quotes such as john.doe@company.com
    /// </summary>
    public class EnvLexPlugin : LexPlugin
    {        
        /// <summary>
        /// Initialize
        /// </summary>
        public EnvLexPlugin()
        {
            _tokens = new[] { "env" };
        }


        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "env '.' ( ( sys | user ) '.' )? <ident>";


        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "env.computername",
            "env.path",
            "env.sys.path",
            "env.user.path"
        };


        /// <summary>
        /// Whether or not this uri plugin can handle the current token.
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public override bool CanHandle(Token current)
        {
            if( _lexer.State.CurrentChar == '.')
                return true;
            return false;
        }


        /// <summary>
        /// run step 123.
        /// </summary>
        /// <returns></returns>
        public override Token[] Parse()
        {
            // env.<ident>
            // env.sys.<ident>
            // env.user.<ident>
            var takeoverToken = _lexer.LastTokenData;
            var line = _lexer.LineNumber;
            var pos = _lexer.LineCharPos;            

            // First "."
            _lexer.ReadChar();

            // Read the next part.
            // Case 1: variable env.path
            // Case 2: sys or user env.user or env.sys
            var part = _lexer.ReadWord();
            var varName = part.Text;
            var scope = string.Empty;

            if (string.Compare(part.Text, "sys", StringComparison.InvariantCultureIgnoreCase) == 0
                || string.Compare(part.Text, "user", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                // Second "."
                _lexer.ReadChar();

                // "env. (sys | user )
                scope = part.Text.ToLower();

                // Final variable name.
                part = _lexer.ReadWord();
                varName = part.Text;
            }
            var finalText = varName;
            var envToken = new EnvToken(scope, varName);
            var t = new TokenData { Token = envToken, Line = line, LineCharPos = pos };
            _lexer.ParsedTokens.Add(t);
            return new Token[] { envToken };
        }
    }


    /// <summary>
    /// Combinator for getting environment variables in format $env.name $env.user.name $env.sys.name.
    /// </summary>
    public class EnvPlugin : ExprPlugin
    {
        /// <summary>
        /// Whether or not this parser can handle the supplied token.
        /// </summary>
        /// <returns></returns>
        public EnvPlugin()
        {
            this.IsAutoMatched = true;
            this.StartTokens = new[] { "$EnvToken" };
        }


        /// <summary>
        /// Parses the day expression.
        /// Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
        /// </summary>
        /// <returns></returns>
        public override Expr Parse()
        {
            var token = _tokenIt.NextToken.Token as EnvToken;
            var expr = new EnvExpr(token.Scope, token.VarName);
            _tokenIt.Advance();
            return expr;
        }
    }


    /// <summary>
    /// Variable expression data
    /// </summary>
    public class EnvExpr : Expr
    {
        private readonly string _scope;
        private readonly string _varName;


        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="scope">The scope of the environment variable : either "sys" or "user" or ""</param>
        /// <param name="varName">The name of the envrionment variable</param>
        public EnvExpr(string scope, string varName)
        {
            _scope = scope;
            _varName = varName;
        }


        /// <summary>
        /// Evaluate
        /// </summary>
        /// <returns></returns>
        public override object DoEvaluate()
        {
            var val = "";
            // Case 1: $env.sys.systemroot
            // Case 2: $env.user.systemroot            
            if (string.IsNullOrEmpty(_scope))
            {
                val = Environment.GetEnvironmentVariable(_varName);
                return new LString(val);
            }
            var target = (_scope == "sys")
                                                ? EnvironmentVariableTarget.Machine
                                                : EnvironmentVariableTarget.User;
            val = Environment.GetEnvironmentVariable(_varName, target);
            return new LString(val);
        }
    }
}
