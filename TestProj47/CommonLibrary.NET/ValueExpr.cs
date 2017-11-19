using System;

namespace HSNXT.ComLib.Lang.AST
{
    /// <summary>
    /// Variable expression data
    /// </summary>
    public class ValueExpr : Expr
    {
        /// <summary>
        /// Name of the variable.
        /// </summary>
        public string Name;


        /// <summary>
        /// Datatype of the variable.
        /// </summary>
        public Type DataType;


        /// <summary>
        /// Value of the variable.
        /// </summary>
        public object Value;
    }
}
