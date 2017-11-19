﻿// <lang:using>

using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Helpers;
using HSNXT.ComLib.Lang.Parsing;
using HSNXT.ComLib.Lang.Types;

// </lang:using>

namespace HSNXT.ComLib.Lang.AST
{
    /// <summary>
    /// Variable expression data
    /// </summary>
    public class UnaryExpr : VariableExpr
    {
        private readonly double Increment;
        private readonly Operator Op;

        /// <summary>
        /// The expression to apply a unary symbol on. e.g. !
        /// </summary>
        public Expr Expression;


        /// <summary>
        /// Initialize
        /// </summary>
        public UnaryExpr()
        {
        }


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="incValue">Value to increment</param>
        /// <param name="op">The unary operator</param>
        /// <param name="name">Variable name</param>
        /// <param name="ctx">Context of the script</param>
        public UnaryExpr(string name, double incValue, Operator op, Context ctx)
        {
            this.Name = name;
            this.Op = op;
            this.Increment = incValue;
            this.Ctx = ctx;
        }


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="exp">Expression representing value to increment by</param>
        /// <param name="op">The unary operator</param>
        /// <param name="name">Variable name</param>
        /// <param name="ctx">Context of the script</param>
        public UnaryExpr(string name, Expr exp, Operator op, Context ctx)
        {
            this.Name = name;
            this.Op = op;
            this.Expression = exp;
            this.Ctx = ctx;
        }
        

        /// <summary>
        /// Evaluate
        /// </summary>
        /// <returns></returns>
        public override object DoEvaluate()
        {
            // Logical not?
            if (Op == Operator.LogicalNot)
                return HandleLogicalNot();

            var valobj = (LObject)this.Ctx.Memory.Get<object>(this.Name);

            // Double ? 
            if (valobj.Type == LTypes.Number ) 
                return IncrementNumber((LNumber)valobj);

            // String ?
            if (valobj.Type == LTypes.String) 
                return IncrementString((LString)valobj);

            throw new LangException("Syntax Error", "Unexpected operation", Ref.ScriptName, Ref.Line, Ref.CharPos);
        }


        private LString IncrementString(LString sourceVal)
        {
            // Check 1: Can only do += on strings.
            if (Op != Operator.PlusEqual)
                throw new LangException("Syntax Error", "string operation with " + Op + " not supported", Ref.ScriptName, Ref.Line, Ref.CharPos);

            this.DataType = typeof(string);
            var val = this.Expression.Evaluate() as LObject;

            // Check 2: Check for null
            if (val == LObjects.Null)
                return sourceVal;

            // Check 3: Limit size if string
            Ctx.Limits.CheckStringLength(this, sourceVal.Value, val.GetValue().ToString());

            // Finally do the appending.
            var appended = sourceVal.Value + val.GetValue();
            sourceVal.Value = appended;
            this.Value = appended;
            this.Ctx.Memory.SetValue(this.Name, sourceVal);
            return sourceVal;
        }


        private LNumber IncrementNumber(LNumber val)
        {
            this.DataType = typeof(double);
            var inc = this.Increment == 0 ? 1 : this.Increment;
            if (this.Expression != null)
            {
                var incval = this.Expression.Evaluate();
                // TODO: Check if null and throw langexception?
                inc = ((LNumber)incval).Value;
            }

            // 1. Calculate the unary value
            val = EvalHelper.CalcUnary(val, Op, inc);

            // 2. Set the value back into scope
            this.Value = val;
            this.Ctx.Memory.SetValue(this.Name, val);
            return val;
        }


        private object HandleLogicalNot()
        {
            var result = this.Expression.Evaluate() as LObject;
            
            // Check 1:  This is actually an assert and should not happen.
            if (result == null)
                throw this.BuildRunTimeException("Null value encountered");

            var retVal = false;
            
            // Only handle bool for logical not !true !false
            if (result.Type == LTypes.Bool)
                retVal = !((LBool)result).Value;
            else if (result == LObjects.Null)
                retVal = true;

            return new LBool(retVal);
        }
    }
}
