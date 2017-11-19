using System;
using System.Collections.Generic;
using HSNXT.ComLib.Lang.Types;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.AST
{
    /// <summary>
    /// Variable expression data
    /// </summary>
    public class DataTypeExpr : IndexableExpr
    {
        private readonly List<Expr> _arrayExpressions;
        private readonly List<Tuple<string, Expr>> _mapExpressions;


        /// <summary>
        /// Initialize
        /// </summary>
        public DataTypeExpr(List<Expr> expressions)
        {
            // Used for lists/arrays
            InitBoundary(true, "]");
            _arrayExpressions = expressions;
        }


        /// <summary>
        /// Initialize
        /// </summary>
        public DataTypeExpr(List<Tuple<string, Expr>> expressions)
        {
            // Used for maps
            InitBoundary(true, "}");
            _mapExpressions = expressions;
        }


        /// <summary>
        /// Evaluate
        /// </summary>
        /// <returns></returns>
        public override object DoEvaluate()
        {
            // Case 1: array type
            if (_arrayExpressions != null)
            {
                var items = new List<object>();

                foreach (var exp in _arrayExpressions)
                {
                    var result = exp == null ? null : exp.Evaluate();
                    items.Add(result);
                }
                var array = new LArray(items);
                return array;
            }

            // Case 2: Map type
            var dictionary = new Dictionary<string, object>();
            foreach (var pair in _mapExpressions)
            {
                var expression = pair.Item2;
                var result = expression == null ? null : expression.Evaluate();
                dictionary[pair.Item1] = result;
            }
            var map = new LMap(dictionary);
            return map;
        }


        /// <summary>
        /// Evaluate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T EvaluateAs<T>()
        {
            var result = Evaluate();
            return (T)result;
        }
    }
}
