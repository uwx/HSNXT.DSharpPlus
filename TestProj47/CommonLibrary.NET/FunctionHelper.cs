﻿using System;
using System.Collections.Generic;
using System.Reflection;
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
    public class FunctionHelper
    {   
        
        /// <summary>
        /// Calls an internal function or external function.
        /// </summary>
        /// <param name="ctx">The context of the runtime.</param>
        /// <param name="fexpr">The function call expression</param>
        /// <param name="functionName">The name of the function. if not supplied, gets from the fexpr</param>
        /// <param name="pushCallStack"></param>
        /// <returns></returns>
        public static object CallFunction(Context ctx, FunctionCallExpr fexpr, string functionName, bool pushCallStack)
        {
            if(string.IsNullOrEmpty(functionName))
                functionName = fexpr.NameExp.ToQualifiedName();

            if (!IsInternalOrExternalFunction(ctx, functionName, null))
                throw ExceptionHelper.BuildRunTimeException(fexpr, "Function does not exist : '" + functionName + "'");

            // 1. Push the name of the function on teh call stack
            if(pushCallStack)
                ctx.State.Stack.Push(functionName, fexpr);

            // 2. Call the function.
            object result = null;
            // Case 1: Custom C# function blog.create blog.*
            if (ctx.ExternalFunctions.Contains(functionName))
                result = ctx.ExternalFunctions.Call(functionName, fexpr);

            // Case 2: Script functions "createUser('john');" 
            else 
                result = CallFunctionInScript(ctx, functionName, fexpr.ParamListExpressions, fexpr.ParamList, true);

            // 3. Finnaly pop the call stact.
            if(pushCallStack)
                ctx.State.Stack.Pop();

            result = CheckConvert(result);
            return result;
        }


        /// <summary>
        /// Calls a property get
        /// </summary>
        /// <param name="ctx">The context of the runtime</param>
        /// <param name="memberAccess">Object to hold all the relevant information required for the member call.</param>
        /// <param name="paramListExpressions">The collection of parameters as expressions</param>
        /// <param name="paramList">The collection of parameter values after they have been evaluated</param>
        /// <returns></returns>
        public static object CallMemberOnBasicType(Context ctx, AstNode node, MemberAccess memberAccess, List<Expr> paramListExpressions, List<object> paramList)
        {
            object result = null;

            // 1. Get methods
            var methods = ctx.Methods.Get(memberAccess.Type);
            
            // 2. Get object on which method/property is being called on.
            var lobj = (LObject)memberAccess.Instance;

            // 3. Property ?
            if (memberAccess.Mode == MemberMode.PropertyMember)
            {
                result = methods.GetProperty(lobj, memberAccess.MemberName);
            }
            // 4. Method
            else if (memberAccess.Mode == MemberMode.MethodMember)
            {
                object[] args = null; 
                if(paramListExpressions != null && paramListExpressions.Count > 0)
                {
                    ParamHelper.ResolveNonNamedParameters(paramListExpressions, paramList);
                    args = paramList.ToArray();
                }
                result = methods.ExecuteMethod(lobj, memberAccess.MemberName, args);
            }
            result = CheckConvert(result);
            return result;
        }


        /// <summary>
        /// Execute a member call.
        /// </summary>
        /// <param name="ctx">The context of the script</param>
        /// <param name="memberAccess">Object to hold all the relevant information required for the member call.</param>
        /// <param name="paramListExpressions">The expressions to resolve as parameters</param>
        /// <param name="paramList">The list of parameters.</param>
        /// <returns></returns>
        public static object CallMemberOnClass(Context ctx, AstNode node, MemberAccess memberAccess, List<Expr> paramListExpressions, List<object> paramList)
        {
            object result = LObjects.Null;
            var obj = memberAccess.Instance;
            var type = memberAccess.DataType;
            
            // Case 1: Property access
            if (memberAccess.Property != null)
            {
                var prop = type.GetProperty(memberAccess.MemberName);
                if (prop != null)
                    result = prop.GetValue(obj, null);
            }
            // Case 2: Method call.
            else if( memberAccess.Method != null)
            {
                result = MethodCall(ctx, obj, type, memberAccess.Method, paramListExpressions, paramList, true);
            }
            result = CheckConvert(result);
            return result;
        }


        /// <summary>
        /// Call a function by passing in all the values.
        /// </summary>
        /// <param name="ctx">The context of the runtime</param>
        /// <param name="functionName">The name of the function to call.</param>
        /// <param name="paramListExpressions">List of parameters as expressions to evaluate first to actual values</param>
        /// <param name="paramVals">List to store the resolved paramter expressions. ( these will be resolved if paramListExpressions is supplied and resolveParams is true. If 
        /// resolveParams is false, the list is assumed to have the values for the paramters to the function.</param>
        /// <param name="resolveParams">Whether or not to resolve the list of parameter expression objects</param>
        /// <returns></returns>
        public static object CallFunctionInScript(Context ctx, string functionName, List<Expr> paramListExpressions, List<object> paramVals, bool resolveParams)
        {

            // 1. Get the function definition
            var function = ctx.Functions.GetByName(functionName);

            // 2. Determine if any parameters provided.
            var hasParams = paramListExpressions != null && paramListExpressions.Count > 0;

            // 3. Resolve parameters if necessary
            if (resolveParams && function != null && (function.HasArguments || hasParams))
                ParamHelper.ResolveParametersForScriptFunction(function.Meta, paramListExpressions, paramVals);

            // 4. Assign the argument values to the function and evaluate.
            function.ArgumentValues = paramVals;
            function.Evaluate();

            object result = null;
            if (function.HasReturnValue)
                result = function.ReturnValue;
            else
                result = LObjects.Null;
            return result;
        }


        /// <summary>
        /// Whether or not the name/member combination supplied is a script level function or an external C# function
        /// </summary>
        /// <param name="ctx">Context of script</param>
        /// <param name="name">Object name "Log"</param>
        /// <param name="member">Member name "Info" as in "Log.Info"</param>
        /// <returns></returns>
        public static bool IsInternalOrExternalFunction(Context ctx, string name, string member)
        {
            var fullName = name;
            if (!string.IsNullOrEmpty(member))
                fullName += "." + member;

            // Case 1: getuser() script function
            if (ctx.Functions.Contains(fullName) || ctx.ExternalFunctions.Contains(fullName))
                return true;

            return false;
        }


        /// <summary>
        /// Whether or not this variable + member name maps to an external function call.
        /// Note: In fluentscript you can setup "Log.*" and allow all method calls to "Log" to map to that external call.
        /// </summary>
        /// <param name="funcs">The collection of external functions.</param>
        /// <param name="varName">The name of the external object e.g. "Log" as in "Log.Error"</param>
        /// <param name="memberName">The name of the method e.g. "Error" as in "Log.Error"</param>
        /// <returns></returns>
        public static bool IsExternalFunction(ExternalFunctions funcs, string varName, string memberName)
        {
            var funcName = varName + "." + memberName;
            if (funcs.Contains(funcName))
                return true;
            return false;
        }


        /// <summary>
        /// Dynamically invokes a method call.
        /// </summary>
        /// <param name="ctx">Context of the script</param>
        /// <param name="obj">Instance of the object for which the method call is being applied.</param>
        /// <param name="datatype">The datatype of the object.</param>
        /// <param name="methodInfo">The method to call.</param>
        /// <param name="paramListExpressions">List of expressions representing parameters for the method call</param>
        /// <param name="paramList">The list of values(evaluated from expressions) to call.</param>
        /// <param name="resolveParams">Whether or not to resolve the parameters from expressions to values.</param>
        /// <returns></returns>
        private static object MethodCall(Context ctx, object obj, Type datatype, MethodInfo methodInfo, List<Expr> paramListExpressions, List<object> paramList, bool resolveParams = true)
        {
            // 1. Convert language expressions to values.
            if (resolveParams) 
                ParamHelper.ResolveParametersForMethodCall(methodInfo, paramListExpressions, paramList);

            // 2. Convert internal language types to c# code method types.
            var args = LangTypeHelper.ConvertArgs(paramList, methodInfo);

            // 3. Handle  params object[];
            if (methodInfo.GetParameters().Length == 1)
            {
                if (methodInfo.GetParameters()[0].ParameterType == typeof(object[]))
                    args = new object[] { args };
            }
            var result = methodInfo.Invoke(obj, args);
            return result;
        }


        private static object CheckConvert(object result)
        {
            // Finally, convert to fluentscript types.
            // Case 1: Aleady an LObject
            if (result is LObject)
                return result;

            // Case 2: C# type so wrap inside of fluentscript type.
            return LangTypeHelper.ConvertToLangValue(result);
        }
    }
}
