﻿using System.Collections;
using System.Collections.Generic;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Helpers;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.AST
{        
    /// <summary>
    /// Function call expression data.
    /// </summary>
    public class FunctionCallExpr : Expr, IParameterExpression
    {
        /// <summary>
        /// Function call expression
        /// </summary>
        public FunctionCallExpr()
        {
            InitBoundary(true, ")");
            ParamList = new List<object>();
            ParamListExpressions = new List<Expr>();
        } 


        /// <summary>
        /// Expression represnting the name of the function call.
        /// </summary>
        public Expr NameExp;


        /// <summary>
        /// List of expressions.
        /// </summary>
        public List<Expr> ParamListExpressions { get; set; }


        /// <summary>
        /// List of arguments.
        /// </summary>
        public List<object> ParamList { get; set; }


        /// <summary>
        /// Arguments to the function.
        /// </summary>
        public IDictionary ParamMap;


        /// <summary>
        /// The function expression.
        /// </summary>
        public FunctionExpr Function;


        /// <summary>
        /// Whether or not this is a method call or a member access.
        /// </summary>
        public bool IsScopeVariable { get => _isScopeVariable;
            set => _isScopeVariable = value;
        }


        /// <summary>
        /// Evauate and run the function
        /// </summary>
        /// <returns></returns>
        public override object DoEvaluate()
        {
            object result = null;
            var isNameEmpty = string.IsNullOrEmpty(_name);
            
            // CASE 1: Exp is variable -> internal/external script. "getuser()".            
            if (this.NameExp is VariableExpr)
            {
                return FunctionHelper.CallFunction(this.Ctx, this, null, true);
            }

            // At this point, is a method call on an object.
            var member = this.NameExp.Evaluate();
            result = member;
            var isMemberAccessType = member is MemberAccess;
            if (!isMemberAccessType) return result;

            var callStackName = this.NameExp.ToQualifiedName();
            var maccess = member as MemberAccess;
            if (!IsMemberCall(maccess)) return result;

            this.Ctx.State.Stack.Push(callStackName, this);
            // CASE 3: object "." method call from script is a external/internal function e.g log.error -> external c# callback.
            if (maccess.IsInternalExternalFunctionCall())
            {
                result = FunctionHelper.CallFunction(Ctx, this, maccess.FullMemberName, false);
            }
            // CASE 4: Method call / Property on Language types
            else if (maccess.Type != null)
            {
                result = FunctionHelper.CallMemberOnBasicType(this.Ctx, this, maccess, this.ParamListExpressions, this.ParamList);
            }
            // CASE 5: Member call via "." : either static or instance method call. e.g. Person.Create() or instance1.FullName() e.g.
            else if (maccess.Mode == MemberMode.CustObjMethodStatic || maccess.Mode == MemberMode.CustObjMethodInstance)
            {
                result = FunctionHelper.CallMemberOnClass(this.Ctx, this, maccess, this.ParamListExpressions, this.ParamList);
            }
            // Pop the function name off the call stack.
            this.Ctx.State.Stack.Pop();
            return result;
        }


        private bool IsMemberCall(MemberAccess maccess)
        {
            if (maccess.IsInternalExternalFunctionCall()
                || (maccess.Mode == MemberMode.MethodMember || maccess.Mode == MemberMode.PropertyMember && maccess.Type != null)
                || maccess.Mode == MemberMode.CustObjMethodInstance || maccess.Mode == MemberMode.CustObjMethodStatic
              )
                return true;
            return false;
        }


        private bool _isScopeVariable;
        private string _name;
        private string _member;
        
        /// <summary>
        /// Get the name of the function.
        /// </summary>
        public string Name
        {
            get
            {
                if (_name != null)
                    return _name;

                if (NameExp is VariableExpr)
                    return ((VariableExpr)NameExp).Name;

                var name = NameExp.Evaluate();
                if (name is string) return (string)name;
                if (name is MemberAccess) return ((MemberAccess)name).FullMemberName;
                return string.Empty;
            }
            set
            {
                _name = value;
                if (_name.Contains("."))
                {
                    var ndxDot = _name.IndexOf(".");
                    _member = _name.Substring(ndxDot + 1); 
                    _name = _name.Substring(0, ndxDot);                    
                }
                _isScopeVariable = true;
            }
        }


        /// <summary>
        /// Returns the fully qualified name of this node.
        /// </summary>
        /// <returns></returns>
        public override string ToQualifiedName()
        {
            if (_name != null) return _name;
            if (NameExp != null)
                return NameExp.ToQualifiedName();
            return string.Empty;
        }
    }
}
