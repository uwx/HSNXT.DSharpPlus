using System;
using DSharpPlus.EventArgs;

namespace DSharpPlus.Extended.Module
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
        /// Gets the name of the event that threw the exception.
        /// </summary>
        public string EventName { get; internal set; }

        /// <summary>
        /// Gets the DspExtended object that fucked up
        /// </summary>
        public DspExtended Extension { get; set; }
        
        internal ExtensionErrorEventArgs(DiscordClient client, DspExtended dspExtended)
            : base(client)
        {
            Extension = dspExtended;
        }
    }
}