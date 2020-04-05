using System;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.InteractivityNext
{
    // TODO docs
    public abstract class BaseControlState : IDisposable
    {
        public DiscordMessage Message { get; internal set; }

        protected internal BaseControlState()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}