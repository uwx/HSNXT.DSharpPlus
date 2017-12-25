using System;

namespace HSNXT
{
    public partial class Extensions
    {
        /// <summary>
        /// Takes a reference type, and returns it, or default if the reference type is null.
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        /// <param name="item">The item to check</param>
        /// <param name="defaultValue">The default item</param>
        /// <returns>item if not null; otherwise defaultValue</returns>
        public static T GetValueOrDefault<T>(this T item, T defaultValue) where T : class
        {
            if (defaultValue == null) throw new ArgumentNullException(nameof(defaultValue));
            return item ?? defaultValue;
        }

        /// <summary>
        /// Maps the item through the specified function.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        public static TResult Map<TSource, TResult>(this TSource item, Func<TSource, TResult> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            return func(item);
        }

        /// <summary>
        /// Allows the item to pipe through the command, and also affect other structures as part of it's action (Tees the data flow).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        /// <remarks>
        /// See the Wikipedia page <see cref="!:https://en.wikipedia.org/wiki/Tee_(command)" /> for further information.
        /// </remarks>
        public static T Tee<T>(this T item, Action<T> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            action(item);
            return item;
        }

        /// <summary>
        /// Pipes through T and if the predicate is met, pipes it through the function.
        /// Otherwise the original T is returned.
        /// </summary>
        /// <typeparam name="T">The Type of the item.</typeparam>
        /// <param name="item">The item to process.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="function">The function that runs when the predicate is true.</param>
        /// <returns>The original value if the predicate is false, otherwise the result of the function.</returns>
        public static T When<T>(this T item, Func<T, bool> predicate, Func<T, T> function)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (function == null) throw new ArgumentNullException(nameof(function));
            if (predicate(item))
            {
                item = function(item);
            }
            return item;
        }

        /// <summary>
        /// When the predicate is true, executes the action.
        /// </summary>
        /// <typeparam name="T">The Type of the item.</typeparam>
        /// <param name="item">The item to process.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="action">The action that runs when the predicate is true.</param>
        /// <returns>The original item.</returns>
        public static T When<T>(this T item, Func<T, bool> predicate, Action<T> action)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (predicate(item))
            {
                action(item);
            }
            return item;
        }
    }
}
