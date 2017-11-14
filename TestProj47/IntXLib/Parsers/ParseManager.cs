using System;

namespace TestProj47
{
    /// <summary>
    /// Used to retrieve needed parser.
    /// </summary>
    internal static class ParseManager
    {
        #region Fields

        /// <summary>
        /// Classic parser instance.
        /// </summary>
        public static readonly IParser ClassicParser;

        /// <summary>
        /// Fast parser instance.
        /// </summary>
        public static readonly IParser FastParser;

        #endregion Fields

        #region Constructors

        // .cctor
        static ParseManager()
        {
            // Create new pow2 parser instance
            IParser pow2Parser = new Pow2Parser();

            // Create new classic parser instance
            IParser classicParser = new ClassicParser(pow2Parser);

            // Fill publicity visible parser fields
            ClassicParser = classicParser;
            FastParser = new FastParser(pow2Parser, classicParser);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns parser instance for given parse mode.
        /// </summary>
        /// <param name="mode">Parse mode.</param>
        /// <returns>Parser instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode" /> is out of range.</exception>
        public static IParser GetParser(ParseMode mode)
        {
            switch (mode)
            {
                case ParseMode.Fast:
                    return FastParser;
                case ParseMode.Classic:
                    return ClassicParser;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }
        }

        /// <summary>
        /// Returns current parser instance.
        /// </summary>
        /// <returns>Current parser instance.</returns>
        public static IParser GetCurrentParser()
        {
            return GetParser(IntX.GlobalSettings.ParseMode);
        }

        #endregion Methods
    }
}