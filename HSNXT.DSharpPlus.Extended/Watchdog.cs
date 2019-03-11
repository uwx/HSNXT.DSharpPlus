using System;
using System.Threading;

namespace HSNXT.DSharpPlus.Extended
{
    public class Watchdog : IDisposable
    {
        private readonly struct WatchdogHandle : IDisposable
        {
            private readonly Watchdog _watchdog;

            public WatchdogHandle(Watchdog watchdog) => _watchdog = watchdog;

            public void Dispose() => _watchdog._hasFinishedOperation.Value.StartTime = 0;
        }
        
        private class RefOpStart
        {
            public long StartTime;
        }
        
        private readonly ThreadLocal<RefOpStart> _hasFinishedOperation 
            = new ThreadLocal<RefOpStart>(() => new RefOpStart(), true);
        private readonly Action<TimeSpan> _deadlockHandler;
        private readonly Thread _watchdogThread;
        private readonly int _watchdogTimeout;

        public Watchdog(Action<TimeSpan> deadlockHandler, TimeSpan? watchdogTimeout = null)
        {
            _deadlockHandler = deadlockHandler;

            _watchdogThread = new Thread(WatchdogDaemon) {IsBackground = true};
            _watchdogThread.Start();

            _watchdogTimeout = watchdogTimeout.HasValue ? (int) watchdogTimeout.Value.TotalMilliseconds : 10_000;
        }

        public IDisposable AcquireHandle()
        {
            _hasFinishedOperation.Value.StartTime = UnixTime();
            return new WatchdogHandle(this);
        }

        public void WatchdogDaemon()
        {
            while (true)
            {
                var now = UnixTime();
                foreach (var status in _hasFinishedOperation.Values)
                {
                    if (status.StartTime == 0) continue;
                    if (now <= status.StartTime + _watchdogTimeout) continue;

                    try
                    {
                        _deadlockHandler(TimeSpan.FromMilliseconds(now - (status.StartTime + _watchdogTimeout)));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"(Critical) An {e} occurred in the D#+ Extended Watchdog deadlock handler.");
                    }
                }

                try
                {
                    Thread.Sleep(_watchdogTimeout * 2);
                }
                catch (ThreadInterruptedException)
                {
                    return;
                }
                catch (ThreadAbortException)
                {
                    return;
                }
            }
        }

        public void Dispose()
        {
            _watchdogThread.Interrupt();
            _watchdogThread.Abort();
            _hasFinishedOperation?.Dispose();
        }
        
        private static long UnixTime() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}