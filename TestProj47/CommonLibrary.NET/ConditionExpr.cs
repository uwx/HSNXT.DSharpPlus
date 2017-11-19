﻿using System;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Types;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.AST
{
    /// <summary>
    /// Condition expression less, less than equal, more, more than equal etc.
    /// </summary>
    public class ConditionExpr : Expr
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="left">Left hand expression</param>
        /// <param name="op">Operator</param>
        /// <param name="right">Right expression</param>
        public ConditionExpr(Expr left, Operator op, Expr right)
        {
            Left = left;
            Right = right;
            AddChild(left);
            AddChild(right);
            Op = op;
        }


        /// <summary>
        /// Left hand expression
        /// </summary>
        public Expr Left;


        /// <summary>
        /// Operator > >= == != less less than
        /// </summary>
        public Operator Op;


        /// <summary>
        /// Right hand expression
        /// </summary>
        public Expr Right;


        /// <summary>
        /// Evaluate > >= != == less less than
        /// </summary>
        /// <returns></returns>
        public override object DoEvaluate()
        {
            // Validate
            if (Op != Operator.And && Op != Operator.Or)
                throw new ArgumentException("Only && || supported");

            var result = false;
            var lhs = Left.Evaluate();
            var rhs = Right.Evaluate();
            var left = false;
            var right = false;
            if (lhs != null) left = ((LBool) lhs).Value;
            if (rhs != null) right = ((LBool)rhs).Value;

            if (Op == Operator.Or)
            {
                result = left || right;
            }
            else if (Op == Operator.And)
            {
                result = left && right;
            }
            return new LBool(result);
        }
    }    
}
