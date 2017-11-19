﻿using System;
using System.Reflection;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Helpers;
using HSNXT.ComLib.Lang.Types;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.AST
{   
    /// <summary>
    /// Information for an index access operation.
    /// </summary>
    public class IndexAccess
    {
        /// <summary>
        /// Instance of the member
        /// </summary>
        public LObject Instance;


        /// <summary>
        /// The name of the member being accessed
        /// </summary>
        public LObject MemberName;
    }



    /// <summary>
    /// Represents the member access
    /// </summary>
    public class MemberAccess
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="mode"></param>
        public MemberAccess(MemberMode mode)
        {
            Mode = mode;
        }


        /// <summary>
        /// The mode of access
        /// </summary>
        public MemberMode Mode;


        /// <summary>
        /// The name of the member.
        /// </summary>
        public string Name;


        /// <summary>
        /// The name of the member being accessed
        /// </summary>
        public string MemberName;


        /// <summary>
        /// Instance of the member
        /// </summary>
        public object Instance;


        /// <summary>
        /// The datatype of the member being accessed.
        /// </summary>
        public Type DataType;


        /// <summary>
        /// The type if the member access is on a built in a fluentscript type.
        /// </summary>
        public LType Type;


        /// <summary>
        /// The method represetning the member.
        /// </summary>
        public MethodInfo Method;


        /// <summary>
        /// The property representing the member.
        /// </summary>
        public PropertyInfo Property;


        /// <summary>
        /// The full member name.
        /// </summary>
        public string FullMemberName => Name + "." + MemberName;


        /// <summary>
        /// Whether or not this represents an external or internal function call.
        /// </summary>
        /// <returns></returns>
        public bool IsInternalExternalFunctionCall()
        {
            return Mode == MemberMode.FunctionExternal || Mode == MemberMode.FunctionScript;
        }


        /// <summary>
        /// Whether or not this represents a property access on a basic built in type (date, array, etc )
        /// </summary>
        /// <returns></returns>
        public bool IsPropertyAccessOnBuiltInType()
        {
            return this.Type != null && this.Mode == MemberMode.PropertyMember;
        }


        /// <summary>
        /// Whether or not this is a property access on a custom object.
        /// </summary>
        /// <returns></returns>
        public bool IsPropertyAccessOnClass()
        {
            return this.DataType != null && this.Property != null;
        }
    }


    /// <summary>
    /// Member access expressions for "." property or "." method.
    /// </summary>
    public class MemberAccessExpr : MemberExpr
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="variableExp">The variable expression to use instead of passing in name of variable.</param>
        /// <param name="memberName">Name of member, this could be a property or a method name.</param>
        /// <param name="isAssignment">Whether or not this is part of an assigment</param>
        public MemberAccessExpr(Expr variableExp, string memberName, bool isAssignment) : base(variableExp, memberName)
        {
            this.IsAssignment = isAssignment;
        }


        /// <summary>
        /// Whether or not this member access is part of an assignment.
        /// </summary>
        public bool IsAssignment;


        /// <summary>
        /// Either external function or member name.
        /// </summary>
        /// <returns></returns>
        public override object DoEvaluate()
        {
            var memberAccess = MemberHelper.GetMemberAccess(this, this.Ctx, this.VariableExp, this.MemberName);
            if (IsAssignment)
                return memberAccess;

            // NOTES:
            // 1. If property on a built in type && not assignment then just return the value of the property
            // 2. It's done here instead because there is no function/method call on a property.
            if (memberAccess.IsPropertyAccessOnBuiltInType())
            {
                var result = FunctionHelper.CallMemberOnBasicType(this.Ctx, this, memberAccess, null, null);
                return result;
            }
            if (memberAccess.IsPropertyAccessOnClass())
            {
                var result = FunctionHelper.CallMemberOnClass(this.Ctx, this, memberAccess, null, null);
                return result;
            }
            return memberAccess;
        }
    }    
}
