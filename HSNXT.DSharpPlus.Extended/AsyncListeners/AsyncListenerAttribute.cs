using System;

namespace HSNXT.DSharpPlus.Extended.AsyncListeners
{
    /// <inheritdoc />
    /// <summary>
    /// Marks a method as an async D#+ event listener. The method must take a DiscordClient parameter and the relevant
    /// EventArgs parameter (or none if there is none)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AsyncListenerAttribute : Attribute
    {
        public EventTypes Target { get; }

        public AsyncListenerAttribute(EventTypes targetType)
        {
            Target = targetType;
        }
    }
}