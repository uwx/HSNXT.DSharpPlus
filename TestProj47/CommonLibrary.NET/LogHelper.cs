﻿using System;
using System.Collections.Generic;
using HSNXT.ComLib.Lang.AST;
using HSNXT.ComLib.Lang.Core;
using HSNXT.ComLib.Lang.Types;
// <lang:using>

// </lang:using>

namespace HSNXT.ComLib.Lang.Helpers
{
    /// <summary>
    /// Helper class for calling functions in the script.
    /// </summary>
    public class LogHelper
    {   

        /// <summary>
        /// Prints to the console.
        /// </summary>
        /// /// <param name="settings">Settings for interpreter</param>
        /// <param name="exp">The functiona call expression</param>
        /// <param name="printline">Whether to print with line or no line</param>
        public static string Print(LangSettings settings, FunctionCallExpr exp, bool printline)
        {
            if (!settings.EnablePrinting) return string.Empty;

            var message = BuildMessage(exp.ParamList);
            if (printline) Console.WriteLine(message);
            else Console.Write(message);
            return message;
        }


        /// <summary>
        /// Logs severity to console.
        /// </summary>
        /// <param name="settings">Settings for interpreter</param>
        /// <param name="exp">The functiona call expression</param>
        public static string Log(LangSettings settings, FunctionCallExpr exp)
        {
            if (!settings.EnableLogging) return string.Empty;

            var severity = exp.Name.Substring(exp.Name.IndexOf(".") + 1);
            var message = BuildMessage(exp.ParamList);
            Console.WriteLine(severity.ToUpper() + " : " + message);
            return message;
        }


        /// <summary>
        /// Builds a single message from multiple arguments
        /// If there are 2 or more arguments, the 1st is a format, then rest are the args to the format.
        /// </summary>
        /// <param name="paramList">The list of parameters</param>
        /// <returns></returns>
        public static string BuildMessage(List<object> paramList)
        {
            var val = string.Empty;
            var hasFormat = false;
            var format = string.Empty;
            if (paramList != null && paramList.Count > 0)
            {
                // Check for 2 arguments which reflects formatting the printing.
                hasFormat = paramList.Count > 1;
                if (hasFormat)
                {
                    format = GetVal(paramList[0]);
                    var args = paramList.GetRange(1, paramList.Count - 1);
                    val = string.Format(format, args.ToArray());
                }
                else
                {
                    val = GetVal(paramList[0]);
                }
            }
            return val;
        }


        private static string GetVal(object val)
        {
            var text = "";
            if (val is LObject)
                text = ((LObject)val).GetValue().ToString();
            else
                text = val.ToString();
            return text;
        }
    }
}
