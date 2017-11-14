using System;

namespace TestProj47
{
    /// <summary>
    /// Used to retrieve needed ToString converter.
    /// </summary>
    internal static class StringConvertManager
    {
        #region Fields

        /// <summary>
        /// Classic converter instance.
        /// </summary>
        public static readonly IStringConverter ClassicStringConverter;

        /// <summary>
        /// Fast converter instance.
        /// </summary>
        public static readonly IStringConverter FastStringConverter;

        #endregion Fields

        #region Constructors

        // .cctor
        static StringConvertManager()
        {
            // Create new pow2 converter instance
            IStringConverter pow2StringConverter = new Pow2StringConverter();

            // Create new classic converter instance
            IStringConverter classicStringConverter = new ClassicStringConverter(pow2StringConverter);

            // Fill publicity visible converter fields
            ClassicStringConverter = classicStringConverter;
            FastStringConverter = new FastStringConverter(pow2StringConverter, classicStringConverter);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns ToString converter instance for given ToString mode.
        /// </summary>
        /// <param name="mode">ToString mode.</param>
        /// <returns>Converter instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode" /> is out of range.</exception>
        public static IStringConverter GetStringConverter(ToStringMode mode)
        {
            switch (mode)
            {
                case ToStringMode.Fast:
                    return FastStringConverter;
                case ToStringMode.Classic:
                    return ClassicStringConverter;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }
        }

        #endregion Methods
    }
}