﻿using System;
using System.Collections.Generic;
using System.Text;
using HSNXT.ComLib.Lang.Helpers;
using HSNXT.ComLib.Lang.Types;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.AST
{
    /// <summary>
    /// For loop Expression data
    /// </summary>
    public class BlockExpr : Expr
    {
        /// <summary>
        /// List of statements
        /// </summary>
        protected List<Expr> _statements = new List<Expr>();


        /// <summary>
        /// Public access to statments.
        /// </summary>
        public List<Expr> Statements => _statements;


        /// <summary>
        /// Evaluate
        /// </summary>
        /// <returns></returns>
        public override object Evaluate()
        {
            object result = null;
            if (this.Ctx != null && this.Ctx.Callbacks.HasAny)
            {
                Ctx.Callbacks.Notify("expression-on-before-execute", this, this);
                result = ExecuteBlock();
                Ctx.Callbacks.Notify("expression-on-after-execute", this, this);
                return result;
            }
            result = ExecuteBlock();
            return result;
        }


        /// <summary>
        /// Execute the statements.
        /// </summary>
        public override object  DoEvaluate()
        {
            LangHelper.Evaluate(this._statements, this.Parent);
            return LObjects.Null;
        }


        /// <summary>
        /// Executes the block with callback/template methods.
        /// </summary>
        protected virtual object ExecuteBlock()
        {
            object result = LObjects.Null;
            try
            {
                OnBlockEnter();
                result = DoEvaluate();
            }
            finally
            {
                OnBlockExit();
            }
            return result;
        }


        /// <summary>
        /// On enter of the block.
        /// </summary>
        protected virtual void OnBlockEnter()
        {
            this.Ctx.Memory.Push();
        }


        /// <summary>
        /// On exit of the block.
        /// </summary>
        protected virtual void OnBlockExit()
        {
            this.Ctx.Memory.Pop();
        }


        /// <summary>
        /// String representation
        /// </summary>
        /// <param name="tab">Tab to use for nested statements in blocks</param>
        /// <param name="incrementTab">Whether or not to add another tab</param>
        /// <param name="includeNewLine">Whether or not to include a new line.</param>
        /// <returns></returns>
        public override string AsString(string tab = "", bool incrementTab = false,  bool includeNewLine = true)
        {
            var info = base.AsString(tab, incrementTab);

            // Empty statements?
            if (_statements == null || _statements.Count == 0) return info;

            var buffer = new StringBuilder();

            // Now iterate over all the statements in the block
            foreach (var stmt in _statements)
            {
                buffer.Append(stmt.AsString(tab, true));
            }

            var result = info + buffer;
            if (includeNewLine) result += Environment.NewLine;

            return result;
        }
    }
}
