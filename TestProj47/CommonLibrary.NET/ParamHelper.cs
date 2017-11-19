﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Types;

namespace HSNXT.ComLib.Lang.Helpers
{
    /// <summary>
    /// Helper class for function parameters.
    /// </summary>
    public class ParamHelper
    {
        /// <summary>
        /// Whether or not the parametlist of expressions contains a named parameter with the name supplied.
        /// </summary>
        /// <param name="paramListExpressions">List of parameter list expressions.</param>
        /// <param name="paramName">Name of the parameter to search for</param>
        /// <returns></returns>
        public static bool HasNamedParameter(List<Expr> paramListExpressions, string paramName)
        {
            if (paramListExpressions == null || paramListExpressions.Count == 0)
                return false;

            foreach (var paramExpr in paramListExpressions)
                if (paramExpr is NamedParamExpr)
                    if (((NamedParamExpr)paramExpr).Name == paramName)
                        return true;
            return false;
        }


        /// <summary>
        /// Resolve all the non-named parameter expressions and puts the values into the param list supplied.
        /// </summary>
        public static void ResolveNonNamedParameters(List<Expr> paramListExpressions, List<object> paramList)
        {
            if (paramListExpressions == null || paramListExpressions.Count == 0)
                return;

            paramList.Clear();
            foreach (var exp in paramListExpressions)
            {
                var val = exp.Evaluate();
                paramList.Add(val);
            }
        }


        /// <summary>
        /// Resolve the parameters in the function call.
        /// </summary>
        public static void ResolveParametersToHostLangValues(List<Expr> paramListExpressions, List<object> paramList)
        {
            if (paramListExpressions == null || paramListExpressions.Count == 0)
                return;

            paramList.Clear();
            foreach (var exp in paramListExpressions)
            {
                var val = exp.Evaluate();
                if(val is LObject)
                {
                    var converted = ((LObject)val).GetValue();
                    paramList.Add(converted);
                }
                else
                    paramList.Add(val);
            }
        }
        

        /// <summary>
        /// Resolves the parameter expressions to actual values.
        /// </summary>
        /// <param name="totalParams"></param>
        /// <param name="paramListExpressions"></param>
        /// <param name="paramList"></param>
        /// <param name="indexLookup"></param>
        public static void ResolveParameters(int totalParams, List<Expr> paramListExpressions, List<object> paramList, Func<NamedParamExpr, int> indexLookup)
        {
            if (paramListExpressions == null || paramListExpressions.Count == 0)
                return;

            // 1. Determine if named params exist.
            var hasNamedParams = HasNamedParameters(paramListExpressions);

            // 2. If no named parameters, simply eval parameters and return.
            if (!hasNamedParams)
            {
                ResolveNonNamedParameters(paramListExpressions, paramList);
                return;
            }
            
            // Start of named parameter evaluation.
            // 1. Clear existing list of value.
            paramList.Clear();

            // 2. Set all args to null. [null, null, null, null, null]
            for (var ndx = 0; ndx < totalParams; ndx++)
                paramList.Add(LObjects.Null);

            // 3. Now go through each argument and replace the nulls with actual argument values.
            // Each null should be replaced at the correct index.
            // [true, 20.68, new Date(2012, 8, 10), null, 'fluentscript']
            for (var ndx = 0; ndx < paramListExpressions.Count; ndx++)
            {
                var exp = paramListExpressions[ndx];

                // 4. Named arg? Evaluate and put its value into the appropriate index of the args list.           
                if (exp is NamedParamExpr)
                {
                    var namedParam = exp as NamedParamExpr;
                    var val = namedParam.Evaluate();
                    var argIndex = indexLookup(namedParam);
                    paramList[argIndex] = val;
                }
                else
                {
                    // 5. Expect the position of non-named args should be valid.
                    // TODO: Semantic analysis is required here once Lint check feature is added.
                    var val = exp.Evaluate();
                    paramList[ndx] = val;
                }
            }
        }


        /// <summary>
        /// Resolve the parameters in the function call.
        /// </summary>
        public static void ResolveParametersForScriptFunction(FunctionMetaData meta, List<Expr> paramListExpressions, List<object> paramList)
        {
            var totalParams = meta.Arguments == null ? 0 : meta.Arguments.Count;
            ResolveParameters(totalParams, paramListExpressions, paramList,
                namedParam => meta.ArgumentsLookup[namedParam.Name].Index);
        }


        /// <summary>
        /// Resolve the parameters in the function call.
        /// </summary>
        public static void ResolveParametersForMethodCall(MethodInfo method, List<Expr> paramListExpressions, List<object> paramList)
        {
            var parameters = method.GetParameters();
            if (parameters == null || parameters.Length == 0) return;

            // 1. Convert parameters to map to know what index position in argument list a param is.
            var map = Enumerable.ToDictionary(parameters, p => p.Name);
            
            // 2. Resolve all the parameters to fluentscript values. LObject, LString etc.
            ResolveParameters(parameters.Length, paramListExpressions, paramList,
                namedParam => map[namedParam.Name].Position);
        }


        /// <summary>
        /// Whether or not there are named parameters here.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool HasNamedParameters(List<Expr> parameters)
        {
            if (parameters == null || parameters.Count == 0) return false;

            var hasNamedParams = false;
            foreach (var param in parameters)
            {
                if (param is NamedParamExpr)
                {
                    hasNamedParams = true;
                    break;
                }
            }
            return hasNamedParams;
        }
    }
}
