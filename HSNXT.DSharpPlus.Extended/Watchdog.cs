using System;
using System.Collections.Concurrent;
using System.Threading;

namespace HSNXT.DSharpPlus.Extended
{
    /// <summary>
    /// A watchdog timer to detect deadlocks in threads.
    /// </summary>
    public class Watchdog : IDisposable // TODO testing
    {
        public delegate void DeadlockHandler(object handleSrc, string reason, Thread source, TimeSpan timeSinceStart);

        private readonly struct WatchdogHandleData
        {
            public readonly long StartTime;
            public readonly object HandleSource;
            public readonly string Reason;

            public WatchdogHandleData(long startTime, object handleSource, string reason)
            {
                HandleSource = handleSource;
                Reason = reason;
                StartTime = startTime;
            }

            public void Deconstruct(out long startTime, out object handleSource, out string reason)
            {
                startTime = StartTime;
                handleSource = HandleSource;
                reason = Reason;
            }
        }
        
        private readonly struct WatchdogHandle : IDisposable
        {
            // we reference the dictionary here and not our parent Watchdog to avoid a cyclical reference
            private readonly ConcurrentDictionary<Thread, WatchdogHandleData> _valuesByThread;

            public WatchdogHandle(ConcurrentDictionary<Thread, WatchdogHandleData> valuesByThread) 
                => _valuesByThread = valuesByThread;

            public void Dispose() 
                => _valuesByThread.TryRemove(Thread.CurrentThread, out _);
        }
        
        private readonly ConcurrentDictionary<Thread, WatchdogHandleData> _valuesByThread 
            = new ConcurrentDictionary<Thread, WatchdogHandleData>();

        private readonly DeadlockHandler _deadlockHandler;
        private readonly Action<string> _log;
        private readonly Thread _watchdogThread;
        private readonly int _watchdogTimeout;
        private bool _disposed;

        /// <summary>
        /// Creates a watchdog and begins its thread. The watchdog will periodically scan for handles to it that have
        /// taken too long to be disposed, and invokes the handler for any that are found. You can usually assume that
        /// there is either a deadlock in that thread or a long-running operation that perhaps should be moved to a new
        /// thread.
        /// </summary>
        /// <param name="deadlockHandler">
        /// Delegate to be called when a thread spends longer than <paramref name="watchdogTimeout"/> while holding a
        /// handle acquired with <see cref="AcquireHandle"/>
        /// </param>
        /// <param name="watchdogTimeout">
        /// Timeout until a thread is considered to be stuck. Note that the actual time until the watchdog thread
        /// catches up may be up to twice the length of time set in this parameter. Defaults to 10 seconds.
        /// </param>
        /// <param name="log">
        /// An optional function to be called whenever the watchdog emits debug information. This usually does not need
        /// to be specified unless you are testing the watchdog itself.
        /// </param>
        /// <remarks>
        /// To help diagnose a deadlock, try using the "break all" feature in your debugger.
        /// </remarks>
        public Watchdog(DeadlockHandler deadlockHandler, TimeSpan? watchdogTimeout = null, Action<string> log = null)
        {
            _deadlockHandler = deadlockHandler;
            _log = log;

            _watchdogThread = new Thread(WatchdogDaemon) {IsBackground = true};
            _watchdogThread.Start();

            _watchdogTimeout = watchdogTimeout.HasValue ? (int) watchdogTimeout.Value.TotalMilliseconds : 10_000;
        }

        /// <summary>
        /// Gives the current thread a handle to this watchdog. No more than a single handle may be kept per thread.
        /// </summary>
        /// <param name="handleSrc">
        /// An object to attach to the handle that can be retrieved in case of a deadlock.
        /// </param>
        /// <param name="reason">
        /// A short reason string to attach to the handle that can be retrieved in case of a deadlock. It should by
        /// convention describe the application that is requesting the handle, but any string can be used. 
        /// </param>
        /// <returns>A disposable object that releases the handle when disposed</returns>
        /// <example>
        /// <code>
        /// using (watchdog.AcquireHandle())
        /// {
        ///     // perform operations
        /// }
        /// </code>
        /// </example>
        public IDisposable AcquireHandle(object handleSrc, string reason)
        {
            var thread = Thread.CurrentThread;
            
            if (_valuesByThread.ContainsKey(thread))
                throw new InvalidOperationException("The current thread already has a handle in use.");

            _valuesByThread[thread] = new WatchdogHandleData(UnixTime(), handleSrc, reason);
            return new WatchdogHandle(_valuesByThread);
        }

        private void WatchdogDaemon()
        {
            _log?.Invoke("Begin process");
            while (true)
            {
                _log?.Invoke("Begin check");
                var now = UnixTime();
                foreach (var (thread, (startTime, handleSource, reason)) in _valuesByThread)
                {
                    if (startTime == 0) continue;
                    if (now <= startTime + _watchdogTimeout) continue;

                    try
                    {
                        _log?.Invoke($"Deadlocked thread {thread.Name}#{thread.ManagedThreadId} (timed out after at least {_watchdogTimeout}ms)");
                        _valuesByThread.TryRemove(thread, out _);
                        _deadlockHandler(
                            handleSource,
                            reason,
                            thread,
                            TimeSpan.FromMilliseconds(now - (startTime + _watchdogTimeout))
                        );
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"(Critical) An {e} occurred in the D#+ Extended Watchdog deadlock handler.");
                    }
                }

                try
                {
                    _log?.Invoke($"Waiting for {_watchdogTimeout * 2}ms");
                    Thread.Sleep(_watchdogTimeout * 2);
                }
                catch (ThreadInterruptedException)
                {
                    _log?.Invoke("Thread was interrupted (goodbye)");
                    return;
                }
                catch (ThreadAbortException)
                {
                    _log?.Invoke("Thread was aborted (goodbye)");
                    return;
                }
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            
            _watchdogThread.Interrupt();
            _watchdogThread.Abort();
        }
        
        private static long UnixTime() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}