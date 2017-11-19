using System;
using System.Collections.Generic;
using System.Threading;
using HSNXT.ComLib.Application;
using HSNXT.ComLib.Queue;
using HSNXT.ComLib.Scheduling;
//<doc:using>
//</doc:using>


namespace HSNXT.ComLib.Samples
{
    /// <summary>
    /// Example for the Scheduling namespace.
    /// </summary>
    public class Example_Scheduler : App
    {
        private readonly Dictionary<string, bool> _tasksCompleted = new Dictionary<string, bool>();
        private readonly AutoResetEvent _resetevent = new AutoResetEvent(false);


        /// <summary>
        /// initialize the completed status of tasks to false.
        /// </summary>
        /// <param name="context"></param>
        public override void Init(object context)
        {
            base.Init(context);
            _tasksCompleted["task1"] = false;
            _tasksCompleted["task2"] = false;
            _tasksCompleted["task3"] = false;
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
			//<doc:example>
            // 1. Run every 2 seconds, with maximum of 3 runs.
            Scheduler.Schedule("task1", new Trigger().Every(2.Seconds()).MaxRuns(3), 
                                        () => Console.WriteLine("Repeating task"), 
                                        task => OnComplete(task));

            // 2. Run every 2 seconds, end in 30 seconds.
            Scheduler.Schedule("task2", new Trigger().Every(3.Seconds()).StopAt(DateTime.Now.AddSeconds(30)), 
                                        () => Console.WriteLine("Repeat with limit"), 
                                        task => OnComplete(task));

            // 3. Combine w/ the Queue processing.
            Queues.AddProcessorFor<string>(items => items.ForEach(item => Console.WriteLine(item)));
            Queues.Enqueue<string>(new List<string> { "a", "b", "c", "d", "e", "f" });
            Scheduler.Schedule("task3", new Trigger().Every(4.Seconds()).MaxRuns(2), 
                                        () => Queues.Process<string>(),
                                        task => OnComplete(task));

            _resetevent.WaitOne();  

			//</doc:example>
            return BoolMessageItem.True;
        }


        private void OnComplete(Task task)
        {
            _tasksCompleted[task.Name] = true;

            if (_tasksCompleted["task1"]
                && _tasksCompleted["task2"]
                && _tasksCompleted["task3"])
            {
                _resetevent.Set();
            }
        }


        /// <summary>
        /// Shutdown dependent services.
        /// </summary>
        public override void ShutDown()
        {
            Scheduler.ShutDown();
        }
    }
}
