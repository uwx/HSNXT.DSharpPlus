using System;
using DSharpPlus;

#if !IS_LITE_VERSION
using DSharpPlus.CommandsNext;
#endif

namespace HSNXT.DSharpPlus.Extended
{
    /// <summary>
    /// A wrapper for <c>CommandsNextExtension</c> that exists in both the lite and full versions but only serves a
    /// purpose in the full version. It exists to avoid having loads of #ifdef lines initializing little things in the
    /// main <see cref="DspExtended"/> class.
    /// </summary>
    internal class CommandsNextWrapper
    {
#if !IS_LITE_VERSION
        private readonly DiscordClient _client;
        private CommandsNextExtension _cnext;
        
        public CommandsNextExtension Value
        {
            get
            {
                if (_cnext == null && (_cnext = _client.GetCommandsNext()) == null)
                {
                    throw new InvalidOperationException(
                        "You are trying to use a CommandsNext feature without CommandsNext initialized. " +
                        "Either you did not place your call to UseDspExtended after UseCommandsNext, or you have not " +
                        "initialized CommandsNext at all. For a version of the extension library that does not " +
                        "depend on CNext, use HSNXT.DSharpPlus.Extended.Lite.");
                }

                return _cnext;
            }
        }
#endif

        // ReSharper disable once UnusedParameter.Local
        public CommandsNextWrapper(DiscordClient client)
        {
#if !IS_LITE_VERSION
            _client = client;
#endif
        }
    }
}