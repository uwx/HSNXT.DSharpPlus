﻿// <lang:using>

using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Parsing;

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>	
    // Units plugin allows the usage of units of measure such as length, weight
    
    enable units;

    // inches, bytes are base types. everything is relative to the base types
    // In examples below, the units will get converted to the measurement on the
    // left hand side. e.g. 3 feet + 5 inches will get converted to datatype
    // LUnit with value in feet and basevalue in inches.    
    var result1 = 3 feet + 5 inches + 10 yards + 2 miles
    var result2 = 1 meg + 30 kb + 50 B + 2 gigs

    // Each unit of measure is based on a basevalue with a relative value.
    // e.g. 
    // type             basevalue   relative values
    // length           inches      feet, yard, mile
    // weight           ounces      milligrams, grams, kilograms
    // computerspace    bytes       kilobytes, megabytes, gigabytes
   
    print feet: #{result1.Value} , inches: #{result1.BaseValue}
    print megs: #{result2.Value} , bytes:  #{result2.BaseValue}
     
    </doc:example>
    ***************************************************************************/

    /// <summary>
    /// Plugin to allow using units such as length, weight, etc.
    /// </summary>
    public class UnitsPlugin : ExprPlugin
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public UnitsPlugin()
        {
            this.StartTokens = new[] { "$Suffix" };
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
            "3 inches",
            "20 feet"
        };


        /// <summary>
        /// Whether or not this plugin can handle current token(s).
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public override bool CanHandle(Token current)
        {
            var t = _tokenIt.Peek(1, false);
            if (_parser.Context.Units.Contains(t.Token.Text))
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
            var t = _tokenIt.Advance();

            // Validate.
            if (!(constExp.Value is double))
                throw _tokenIt.BuildSyntaxException("number required when using units : " + t.Token.Text, t);

            var result = ctx.Units.ConvertToUnits((double)constExp.Value, t.Token.Text);
            var finalExp = new ConstantExpr(result);
            
            // Move past the plugin.
            _tokenIt.Advance();
            return finalExp;
        }
    }
}