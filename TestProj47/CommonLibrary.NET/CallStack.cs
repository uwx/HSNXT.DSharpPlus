﻿using System;
using System.Collections.Generic;
using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Parsing
{
    /// <summary>
    /// The call stack
    /// </summary>
    public class CallStack
    {
        private readonly List<Tuple<string,FunctionCallExpr>> _stack;
        private int _lastIndex = -1;
        private readonly Action<AstNode, int> _limitCheck;

        
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="limitCheck"></param>
        public CallStack(Action<AstNode, int> limitCheck)
        {
            _stack = new List<Tuple<string, FunctionCallExpr>>();
            _limitCheck = limitCheck;
        }


        /// <summary>
        /// Push the function expression on the call stack
        /// </summary>
        /// <param name="qualifiedName">Name of function call</param>
        /// <param name="exp">Function Call expression</param>
        public bool Push(string qualifiedName, FunctionCallExpr exp)
        {   
            _stack.Add(new Tuple<string, FunctionCallExpr>(qualifiedName, exp));
            _lastIndex++;

            if (_limitCheck != null)
                _limitCheck(exp, _lastIndex);

            return true;
        }


        /// <summary>
        /// Pop the function expression from the call stack.
        /// </summary>
        public void Pop()
        {
            var count = _stack.Count;
            if (count == 0) return;
            _stack.RemoveAt(_lastIndex);
            count--;
            _lastIndex = count - 1;
        }


        /// <summary>
        /// Total item in the call stack.
        /// </summary>
        public int Count => _stack.Count;


        /// <summary>
        /// Get by index.
        /// </summary>
        /// <param name="ndx"></param>
        /// <returns></returns>
        public Tuple<string, FunctionCallExpr> this[int ndx] => _stack[_lastIndex];
    }
}
