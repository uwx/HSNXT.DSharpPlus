using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Helpers;
using HSNXT.ComLib.Lang.Parsing;
using HSNXT.ComLib.Lang.Types;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Plugins
{

    /* *************************************************************************
    <doc:example>	
    // Aggregate plugin allows sum, min, max, avg, count aggregate functions to 
    // be applied to lists of objects.
    
    var numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];
    var result = 0;
    
    // Example 1: Using format sum of <expression>
    result = sum of numbers;
    result = avg of numbers;
    result = min of numbers;
    result = max of numbers;
    result = count of numbers;
    
    // Example 2: Using format sum(<expression>)
    result = sum( numbers );
    result = avg( numbers );
    result = min( numbers );
    result = max( numbers );
    result = count( numbers );    
    </doc:example>
    ***************************************************************************/
    /// <summary>
    /// Combinator for handling comparisons.
    /// </summary>
    public sealed class AggregatePlugin : ExprPlugin
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public AggregatePlugin()
        {
            this.IsAutoMatched = true;
            this.StartTokens = new[] 
            { 
                "avg", "min", "max", "sum", "count", "number", 
                "Avg", "Min", "Max", "Sum", "Count", "Number"
            };
        }


        /// <inheritdoc />
        /// <summary>
        /// Whether or not this parser can handle the supplied token.
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public override bool CanHandle(Token current)
        {
            var next = _tokenIt.Peek().Token;
            return next == Tokens.LeftParenthesis || string.Compare(next.Text, "of", StringComparison.InvariantCultureIgnoreCase) == 0;
        }


        /// <inheritdoc />
        /// <summary>
        /// The grammer for the function declaration
        /// </summary>
        public override string Grammar => "( avg | min | max | sum | count | number ) ( ( '(' <expression> ')' ) | ( of <expression> ) )";


        /// <inheritdoc />
        /// <summary>
        /// Examples
        /// </summary>
        public override string[] Examples => new[]
        {
            "min( numbers )",
            "Min( numbers )",
            "min of numbers",
            "Min of numbers"
        };


        /// <summary>
        /// run step 123.
        /// </summary>
        /// <returns></returns>
        public override Expr Parse()
        {
            // avg min max sum count
            var aggregate = _tokenIt.NextToken.Token.Text.ToLower();

            var next = _tokenIt.Peek().Token;
            Expr exp = null;

            // 1. sum( <expression> )
            if (next == Tokens.LeftParenthesis)
            {
                _tokenIt.Advance(2);
                exp = _parser.ParseExpression(Terminators.ExpParenthesisEnd, passNewLine: false);
                _tokenIt.Expect(Tokens.RightParenthesis);
            }
            // 2. sum of <expression>
            else if (string.Compare(next.Text, "of", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                _tokenIt.Advance(2);
                exp = _parser.ParseExpression(null, false, true, passNewLine: false);
            }
            
            var aggExp = new AggregateExpr(aggregate, exp);
            return aggExp;
        }
    }



    /// <inheritdoc />
    /// <summary>
    /// Expression to represent a Linq like query.
    /// </summary>
    public sealed class AggregateExpr : Expr
    {        
        private readonly string _aggregateType;
        private readonly Expr _source;


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="aggregateType">sum avg min max count total</param>
        /// <param name="source"></param>
        public AggregateExpr(string aggregateType, Expr source)
        {
            InitBoundary(true, ")");
            _aggregateType = aggregateType;
            _source = source;
        }


        /// <inheritdoc />
        /// <summary>
        /// Evaluate the aggregate expression.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public override object Evaluate()
        {
            var dataSource = _source.Evaluate() as LObject;
            ExceptionHelper.NotNull(this, dataSource, "aggregation(min/max)");
            
            List<object> items;

            // Get the right type of list.
            if (dataSource.Type == LTypes.Array)
                items = dataSource.GetValue() as List<object>;
            else
                throw new NotSupportedException(_aggregateType + " not supported for list type of " + dataSource.GetType());

            double val = 0;
            if (_aggregateType == "sum")
                val = items.Sum(item => GetValue(item));

            else if (_aggregateType == "avg")
                val = items.Average(item => GetValue(item));

            else if (_aggregateType == "min")
                val = items.Min(item => GetValue(item));

            else if (_aggregateType == "max")
                val = items.Max(item => GetValue(item));

            else if (_aggregateType == "count" || _aggregateType == "number")
                val = items.Count;

            return new LNumber(val);
        }


        private double GetValue(object item)
        {
            // Check 1: Null
            if (item == LObjects.Null) return 0;
            var lobj = (LObject) item;

            // Check 2: Number ? ok
            if (lobj.Type == LTypes.Number) return ((LNumber) lobj).Value;

            return 0;
        }
    }
}