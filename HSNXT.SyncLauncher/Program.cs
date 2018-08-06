using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HSNXT.SyncLauncher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Pass two commands to be executed");
                return;
            }

            var p1 = new Process
            {
                StartInfo =
                {
                    FileName = args[0]
                }
            };
            var p2 = new Process
            {
                StartInfo =
                {
                    FileName = args[1]
                }
            };
            Task.WaitAny(WaitForExitAsync(p1), WaitForExitAsync(p2));
            if (!p1.HasExited)
            {
                p1.Kill();
            }
            if (!p2.HasExited)
            {
                p2.Kill();
            }

            Environment.Exit(p1.ExitCode | p2.ExitCode);
        }
        
        private static Task WaitForExitAsync(Process proc)
        {
            var tcs = new TaskCompletionSource<bool>();

            proc.Exited += (o, e) => tcs.SetResult(true);

            proc.Start();
            
            return tcs.Task;
        }
    }
}