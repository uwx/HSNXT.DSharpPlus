﻿using System.Collections.Generic;
using HSNXT.ComLib.Lang.Types;

namespace HSNXT.ComLib.Lang.AST
{

    /// <summary>
    /// Expression to handle string interpolations such as "${username}'s email is ${email}".
    /// </summary>
    public class InterpolatedExpr : Expr
    {
        private List<Expr> _expressions;


        /// <summary>
        /// Adds an expression to be interpolated.
        /// </summary>
        /// <param name="exp"></param>
        public void Add(Expr exp)
        {
            if (_expressions == null)
                _expressions = new List<Expr>();

            _expressions.Add(exp);
        }


        /// <summary>
        /// Clears the expression.
        /// </summary>
        public void Clear()
        {
            if (_expressions != null)
                _expressions.Clear();
        }


        /// <summary>
        /// Evaluates the expression by appending all the sub-expressions.
        /// </summary>
        /// <returns></returns>
        public override object DoEvaluate()
        {
            if (_expressions == null || _expressions.Count == 0)
                return string.Empty;

            var total = "";
            foreach (var exp in _expressions)
            {
                if (exp != null)
                {
                    var val = exp.Evaluate();
                    var text = "";
                    if (val is LObject)
                    {
                        var lobj = (LObject)val;
                        text = lobj.GetValue().ToString();
                    }
                    else
                        text = val.ToString();
                    total += text;
                }
            }
            return new LString(total);
        }
    }
}
