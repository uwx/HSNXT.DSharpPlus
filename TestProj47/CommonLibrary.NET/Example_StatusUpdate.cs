using System;
using HSNXT.ComLib.Application;
using HSNXT.ComLib.StatusUpdater;

namespace HSNXT.ComLib.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_StatusUpdate : App
    {

        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            StatusUpdates.Update("startup", "starting", "application startup", DateTime.Now, DateTime.Now);
            StatusUpdates.Update("startup", "completed", "application startup", DateTime.Now, DateTime.Now);
            StatusUpdates.Update("executing", "executing", "app execution", DateTime.Now, DateTime.Now);

            return BoolMessageItem.True;
        }
    }
}
