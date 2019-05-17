using System;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace HSNXT.DSharpPlus.Extended
{
    /// <summary>
    /// Represents arguments for <see cref="E:DSharpPlus.DiscordClient.ExtensionErrored" /> event.
    /// </summary>
    public class ExtensionErrorEventArgs : DiscordEventArgs
    {
        /// <summary>
        /// Gets the exception thrown by the client.
        /// </summary>
        public Exception Exception { get; internal set; }

        /// <summary>
        /// Gets the message of the exception thrown by the client, if available.
        /// </summary>
        public string Message => Exception.Message;

        /// <summary>
        /// Gets the name of the event that threw the exception.
        /// </summary>
        public string EventName { get; internal set; }

        /// <summary>
        /// Gets the DspExtended instance which the event belongs to.
        /// </summary>
        public DspExtended Extension { get; set; }
        
        /// <summary>
        /// Gets the timestamp of the exception being caught.
        /// </summary>
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;

        internal ExtensionErrorEventArgs(DspExtended dspExtended) : base(dspExtended.Client)
        {
            Extension = dspExtended;
        }
        
        public override string ToString()
        {
            return $"[{Timestamp:yyyy-MM-dd HH:mm:ss zzz}] [{EventName}] {Message}:\n{Exception}";
        }
    }
}