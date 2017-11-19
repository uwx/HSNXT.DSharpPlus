﻿using System;
using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Parsing;
using HSNXT.ComLib.Lang.Types;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Helpers
{
    /// <summary>
    /// Helper class for calling functions in the script.
    /// </summary>
    public class AssignHelper
    {
        /// <summary>
        /// Sets a value on a member of a basic type.
        /// </summary>
        /// <param name="ctx">The context of the runtime</param>
        /// <param name="node">The assignment ast node</param>
        /// <param name="isDeclaration">Whether or not this is a declaration</param>
        /// <param name="varExp">The expression representing the index of the instance to set</param>
        /// <param name="valExp">The expression representing the value to set</param>
        public static void SetVariableValue(Context ctx, AstNode node, bool isDeclaration, Expr varExp, Expr valExp)
        {
            var varname = ((VariableExpr)varExp).Name;

            // Case 1: var result;
            if (valExp == null)
            {
                ctx.Memory.SetValue(varname, LObjects.Null, isDeclaration);
            }
            // Case 2: var result = <expression>;
            else
            {
                var result = valExp.Evaluate();
                
                // CHECK_LIMIT:
                ctx.Limits.CheckStringLength(node, result);
                ctx.Memory.SetValue(varname, result, isDeclaration);
            }

            // LIMIT CHECK
            ctx.Limits.CheckScopeCount(varExp);
            ctx.Limits.CheckScopeStringLength(varExp);
        }


        /// <summary>
        /// Sets a value on a member of a basic type.
        /// </summary>
        /// <param name="ctx">The context of the runtime</param>
        /// <param name="varExp">The expression representing the index of the instance to set</param>
        /// <param name="valExp">The expression representing the value to set</param>
        /// <param name="node">The assignment ast node</param>
        public static void SetMemberValue(Context ctx, AstNode node, Expr varExp, Expr valExp)
        {
            // 1. Get the value that is being assigned.
            var val = valExp.Evaluate() as LObject;

            // 2. Check the limit if string.
            ctx.Limits.CheckStringLength(node, val);

            // 3. Evaluate expression to get index info.
            var memAccess = varExp.Evaluate() as MemberAccess;
            if (memAccess == null)
                throw ExceptionHelper.BuildRunTimeException(node, "Value to assign is null");

            // Case 1: Set member on basic type
            if (memAccess.Type != null)
            {
                // Get methods associated with type.
                var methods = ctx.Methods.Get(memAccess.Type);

                // Case 1: users['total'] = 20
                if (memAccess.Type == LTypes.Map)
                {
                    var target = memAccess.Instance as LObject;
                    methods.SetByStringMember(target, memAccess.MemberName, val);
                }
            }
            // Case 2: Set member on custom c# class
            else if(memAccess.DataType != null)
            {
                if(memAccess.Property != null)
                {
                    var prop = memAccess.Property;
                    prop.SetValue(memAccess.Instance, val.GetValue(), null);
                }
            }
        }


        /// <summary>
        /// Sets a value on a member of a basic type.
        /// </summary>
        /// <param name="ctx">The context of the runtime</param>
        /// <param name="varExp">The expression representing the index of the instance to set</param>
        /// <param name="valExp">The expression representing the value to set</param>
        /// <param name="node">The assignment ast node</param>
        public static void SetIndexValue( Context ctx, AstNode node, Expr varExp, Expr valExp)
        {
            // 1. Get the value that is being assigned.
            var val = valExp.Evaluate() as LObject;

            // 2. Check the limit if string.
            ctx.Limits.CheckStringLength(node, val);

            // 3. Evaluate expression to get index info.
            var indexExp = varExp.Evaluate() as IndexAccess;
            if (indexExp == null)
                throw ExceptionHelper.BuildRunTimeException(node, "Value to assign is null");

            // 4. Get the target of the index access and the name / number to set.
            var target = indexExp.Instance;
            var memberNameOrIndex = indexExp.MemberName;
            
            // Get methods associated with type.
            var methods = ctx.Methods.Get(target.Type);
            
            // Case 1: users[0] = 'kishore'
            if(target.Type == LTypes.Array)
            {
                var index = Convert.ToInt32(((LNumber) memberNameOrIndex).Value);
                methods.SetByNumericIndex(target, index, val);
            }
            // Case 2: users['total'] = 20
            else if (target.Type == LTypes.Map)
            {
                var name = ((LString) memberNameOrIndex).Value;
                methods.SetByStringMember(target, name, val);
            }
        }
    }
}
