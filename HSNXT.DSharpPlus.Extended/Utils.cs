using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HSNXT.DSharpPlus.Extended
{
    internal static class Utils
    {
        internal static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
        {
            key = tuple.Key;
            value = tuple.Value;
        }

        /// <summary>
        /// Shortcut for <see cref="Task{TResult}.ConfigureAwait"/><c>(false)</c>, ensures that the continuation of the
        /// task will not be marshaled back to the original context captured.
        /// </summary>
        /// <param name="task">The task</param>
        /// <typeparam name="T">The task type</typeparam>
        /// <returns>An object used to await the configured task</returns>
        internal static ConfiguredTaskAwaitable<T> NoCapt<T>(this Task<T> task) 
            => task.ConfigureAwait(false);

        /// <summary>
        /// Shortcut for <see cref="Task.ConfigureAwait"/><c>(false)</c>, ensures that the continuation of the
        /// task will not be marshaled back to the original context captured.
        /// </summary>
        /// <param name="task">The task</param>
        /// <returns>An object used to await the configured task</returns>
        internal static ConfiguredTaskAwaitable NoCapt(this Task task) 
            => task.ConfigureAwait(false);
    }
}