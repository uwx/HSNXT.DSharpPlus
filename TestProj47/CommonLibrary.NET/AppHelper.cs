using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using HSNXT.ComLib.Arguments;
using HSNXT.ComLib.Authentication;
using HSNXT.ComLib.Environments;
using HSNXT.ComLib.Logging;

namespace HSNXT.ComLib.Application
{
    /// <summary>
    /// Utility class with methods that provide application information.
    /// </summary>
    public class AppHelper
    {
        /// <summary>
        /// <para>
        /// Handle possible "meta data" options of the application.
        /// 1. -help
        /// 2. -version
        /// 3. -pause
        /// </para>
        /// </summary>
        /// <param name="app"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static BoolMessageItem<Args> HandleOptions(IApp app, Args args)
        {
            var result = new BoolMessageItem<Args>(args, true, string.Empty);

            // Pause the execution of application to allow attaching of debugger.
            if (args.IsPause)
            {
                Console.WriteLine("Paused for debugging ....");
                Console.ReadKey();
                result = new BoolMessageItem<Args>(args, true, string.Empty); 
            }
            else if (args.IsVersion || args.IsInfo)
            {
                ShowAppInfo(app);
                result = new BoolMessageItem<Args>(args, false, "Displaying description/version.");
            }
            else if (args.IsHelp)
            {
                var helpText = GetAppInfo(app) + Environment.NewLine + Environment.NewLine;

                // -help or ?
                if (args.Schema.IsEmpty)
                    helpText += "Argument definitions are not present.";
                else
                {
                    // 1. Get the examples
                    var examplesText = app.OptionsExamples;

                    // 2. Now get the options as text.
                    helpText += ArgsUsage.BuildDescriptive(args.Schema.Items, examplesText, args.Prefix, args.Separator);
                }
                // 3. Print
                Console.WriteLine(helpText);
                result = new BoolMessageItem<Args>(args, false, "Displaying usage");
            }
            return result;
        }


        /// <summary>
        /// Show the application info.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static void ShowAppInfo(IApp app)
        {
            Console.WriteLine(GetAppInfo(app));
        }


        /// <summary>
        /// Get application run summary.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="isStart"></param>
        /// <param name="summaryInfo"></param>
        /// <returns></returns>
        public static string GetAppRunSummary(IApp app, bool isStart, IDictionary summaryInfo)
        {
            BuildAppRunSummary(app, isStart, summaryInfo);
            var runSummary = GetAppRunSummaryAsString(isStart, summaryInfo);
            return runSummary;
        }


        /// <summary>
        /// Get application information as string.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static string GetAppInfo(IApp app)
        {
            var buffer = new StringBuilder();
            IDictionary info = new OrderedDictionary();
            info["Company"] = app.Company;
            info["Name"] = app.Name;
            info["Website"] = app.Website;
            info["Version"] = app.Version;
            info["Description"] = app.Description;
            buffer.Append("===============================================================" + Environment.NewLine);
            StringHelper.DoFixedLengthPrinting(info, 4, (key, val) => buffer.Append(key + " : " + val + Environment.NewLine));
            buffer.Append("===============================================================" + Environment.NewLine);
            var summary = buffer.ToString();
            return summary;
        }


        /// <summary>
        /// Creates a runtime summary for the application.
        /// </summary>
        /// <param name="app">Application to get summary for.</param>
        /// <param name="isStart">True if application is starting.</param>
        /// <param name="summaryInfo">Dictionary to use when storing runtime summary information.</param>
        public static void BuildAppRunSummary(IApp app, bool isStart, IDictionary summaryInfo)
        {
            if (summaryInfo == null) summaryInfo = new OrderedDictionary();
            var fileInfo = new FileInfo(Assembly.GetEntryAssembly().Location);
            var envName = Env.Selected == null ? "" : Env.Name;
            var envPath = Env.Selected == null ? "" : Env.RefPath;
            var envType = Env.Selected == null ? "" : Env.EnvType.ToString();

            // Version/Machine/Evnrionment summary.
            summaryInfo["Location"] = fileInfo.Directory.FullName;
            summaryInfo["Version"] = app.Version;
            summaryInfo["Machine"] = Environment.MachineName;
            summaryInfo["User"] = Auth.UserName;
            summaryInfo["StartTime"] = app.StartTime.ToString();
            summaryInfo["EndTime"] = DateTime.Now.ToString();
            summaryInfo["Duration"] = (DateTime.Now - app.StartTime).ToString();
            summaryInfo["Diagnostics"] = "Diagnostics.log";
            summaryInfo["Args"] = Environment.CommandLine;
            summaryInfo["Env Type"] = envType;
            summaryInfo["Env Name"] = envName;
            summaryInfo["Config"] = envPath;

            // Get the log summary.
            var logSummary = Logger.GetLogInfo();
            for (var ndx = 0; ndx < logSummary.Count; ndx++)
                summaryInfo["Log " + ndx + 1] = logSummary[ndx];

            // Get arguments that were applied to the argument reciever.
            if (app.Settings.ArgsReciever != null
                && app.Settings.ArgsRequired && app.Settings.ArgsAppliedToReciever)
                ArgsHelper.GetArgValues(summaryInfo, app.Settings.ArgsReciever);
        }


        /// <summary>
        /// Returns a string with the application summary information..
        /// </summary>
        /// <param name="isStart">True if the application is starting.</param>
        /// <param name="summaryInfo">Dictionary with summary info.</param>
        /// <returns>String with application summary.</returns>
        public static string GetAppRunSummaryAsString(bool isStart, IDictionary summaryInfo)
        {
            var header = isStart ? "Application Start" : "Application End";            
            var buffer = new StringBuilder();
            // Print the summary.
            // - Version          : 1.0.0.0
            // - Machine          : KISHORE1
            // - User             : kishore1\kishore
            buffer.Append("=================================================== " + Environment.NewLine);
            buffer.Append("=========== " + header + " Information ============= " + Environment.NewLine);
            StringHelper.DoFixedLengthPrinting(summaryInfo, 4, (key, val) => buffer.Append(" - " + key + " : " + val + Environment.NewLine));
            buffer.Append("=================================================== " + Environment.NewLine);
            buffer.Append("=================================================== " + Environment.NewLine);
            return buffer.ToString();
        }

    }
}
