using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended
{
    /// <summary>
    /// Utility methods to reflectively access private members in DSharpPlus. <b>Warning:</b> all of this class' members
    /// are likely to break at some point or another, and may be changed without warning or any deprecation procedure.
    /// </summary>
    public class ReflectionUtils
    {
        private static readonly Lazy<Func<SnowflakeObject, BaseDiscordClient>> GetClientExpr 
            = new Lazy<Func<SnowflakeObject, BaseDiscordClient>>(
                () => Expressions.GetMember<SnowflakeObject, BaseDiscordClient>("Discord"));

        private static readonly Lazy<Func<DiscordRole, ulong>> GetRoleGuildIdExpr 
            = new Lazy<Func<DiscordRole, ulong>>(
                () => Expressions.GetMember<DiscordRole, ulong>("_guild_id"));

//        private static readonly Lazy<Func<BaseDiscordClient, DiscordConfiguration>> GetConfigurationExpr 
//            = new Lazy<Func<BaseDiscordClient, DiscordConfiguration>>(
//                () => Expressions.GetMember<BaseDiscordClient, DiscordConfiguration>("Configuration"));

        private static readonly Lazy<Func<DiscordMember>> NewDiscordMemberExpr 
            = new Lazy<Func<DiscordMember>>(Expressions.Constructor0<DiscordMember>);
        
        private static readonly Lazy<Func<DiscordRole>> NewDiscordRoleExpr 
            = new Lazy<Func<DiscordRole>>(Expressions.Constructor0<DiscordRole>);

        private static readonly Lazy<Action<SnowflakeObject, ulong>> SetIdExpr
            = new Lazy<Action<SnowflakeObject, ulong>>(
                () => Expressions.SetMember<SnowflakeObject, ulong>("Id"));

        private static readonly Lazy<Func<DiscordShardedClient, Task<int>>> InitializeShardsAsyncExpr
            = new Lazy<Func<DiscordShardedClient, Task<int>>>(
                () => Expressions.CallInstanceMethod0<DiscordShardedClient, Task<int>>("InitializeShardsAsync"));

        
        public static BaseDiscordClient GetClient(SnowflakeObject obj)
        {
            return GetClientExpr.Value(obj);
        }
 
//        public static DiscordConfiguration GetConfiguration(BaseDiscordClient obj)
//        {
//            return GetConfigurationExpr.Value(obj);
//        }

        public static DiscordMember CreateSkeletonMember(ulong id)
        {
            var role = NewDiscordMemberExpr.Value();
            SetIdExpr.Value(role, id);
            return role;
        }
        
        public static DiscordRole CreateSkeletonRole(ulong id)
        {
            var role = NewDiscordRoleExpr.Value();
            SetIdExpr.Value(role, id);
            return role;
        }

        public static Task<int> InitializeShardsAsync(DiscordShardedClient client)
        {
            return InitializeShardsAsyncExpr.Value(client);
        }

        public static Func<DiscordClient, TModule> NewModule<TModule>() where TModule : ClientModule
        {
            return Expressions.Constructor1<TModule, DiscordClient>();
        }

        public static Func<DiscordClient, TConfiguration, TModule> NewModule<TModule, TConfiguration>()
            where TModule : ClientModule<TConfiguration>
        {
            return Expressions.Constructor2<TModule, DiscordClient, TConfiguration>();
        }

        public static ulong GetGuildId(DiscordRole role)
        {
            return GetRoleGuildIdExpr.Value(role);
        }
    }
}