using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.CommandsNext._Introspection
{
    public abstract class BaseIntrospectiveExtension : BaseExtension
    {
        public new ProxiedDiscordClient Client { get; set; }
    }
}