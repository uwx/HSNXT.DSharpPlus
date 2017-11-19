using System;

namespace HSNXT.ComLib.Lang.Helpers
{
    /// <summary>
    /// Helps to get argument values ( from array )
    /// </summary>
    public class ArgsFetcher
    {
        private readonly object[] _args;


        /// <summary>
        /// Initialize with arguments.
        /// </summary>
        /// <param name="args"></param>
        public ArgsFetcher(object[] args)
        {
            _args = args;
        }


        /// <summary>
        /// Arguments
        /// </summary>
        public object[] Args => _args;


        /// <summary>
        /// Get value of type T at index position supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndx"></param>
        /// <returns></returns>
        public T Get<T>(int ndx)
        {
            var val = _args[ndx];
            val = Convert.ChangeType(val, typeof(T), null);
            return (T)val;
        }


        /// <summary>
        /// Get the default value if ndx outof bounds
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ndx"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public T Get<T>(int ndx, T defaultVal)
        {
            if (ndx < 0 || ndx >= _args.Length)
                return defaultVal;

            var val = _args[ndx];
            val = Convert.ChangeType(val, typeof(T), null);
            return (T)val;
        }
    }
}
