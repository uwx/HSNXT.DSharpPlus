using System;
using System.Linq.Expressions;
using DSharpPlus;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended
{
    public class ReflectionUtils
    {
//        private static readonly Lazy<Func<SnowflakeObject, BaseDiscordClient>> GetClientExpr 
//            = new Lazy<Func<SnowflakeObject, BaseDiscordClient>>(
//                () => GetMemberExpr<SnowflakeObject, BaseDiscordClient>("Discord"));
//        
//        private static readonly Lazy<Func<BaseDiscordClient, DiscordConfiguration>> GetConfigurationExpr 
//            = new Lazy<Func<BaseDiscordClient, DiscordConfiguration>>(
//                () => GetMemberExpr<BaseDiscordClient, DiscordConfiguration>("Configuration"));

        private static readonly Lazy<Func<DiscordMember>> NewDiscordMemberExpr 
            = new Lazy<Func<DiscordMember>>(ParameterlessConstructorExpr<DiscordMember>);
        
        private static readonly Lazy<Func<DiscordRole>> NewDiscordRoleExpr 
            = new Lazy<Func<DiscordRole>>(ParameterlessConstructorExpr<DiscordRole>);

        private static readonly Lazy<Action<SnowflakeObject, ulong>> SetIdExpr
            = new Lazy<Action<SnowflakeObject, ulong>>(
                () => SetMemberExpr<SnowflakeObject, ulong>("Id"));

        private static ParameterExpression ParameterInstance<T>() => Expression.Parameter(typeof(T), "instance");

//        private static Func<T, TResult> GetMemberExpr<T, TResult>(string name)
//        {
//            var instance = ParameterInstance<T>();
//            
//            return Expression.Lambda<Func<T, TResult>>(
//                Expression.PropertyOrField(instance, name) // return instance.name
//                , instance // (instance) => ...
//            ).Compile();
//        }

        private static Func<T> ParameterlessConstructorExpr<T>()
        {
            return Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
        }
        
        private static Action<T, TValue> SetMemberExpr<T, TValue>(string name) where T : class
        {
            var instance = ParameterInstance<T>();
            var value = Expression.Parameter(typeof(TValue), "value");

            return Expression.Lambda<Action<T, TValue>>(
                Expression.Assign(Expression.PropertyOrField(instance, name), value) // instance.name = value
                , instance, value).Compile(); // (ref instance, value) => ...
        }
        
//        public static BaseDiscordClient GetClient(SnowflakeObject obj)
//        {
//            return GetClientExpr.Value(obj);
//        }
//        
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
    }
}