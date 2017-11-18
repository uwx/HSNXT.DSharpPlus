using System;

namespace HSNXT
{
    /// <summary>
    /// Used to retrieve needed divider.
    /// </summary>
    internal static class DivideManager
    {
        #region Fields

        /// <summary>
        /// Classic divider instance.
        /// </summary>
        public static readonly IDivider ClassicDivider;

        /// <summary>
        /// Newton divider instance.
        /// </summary>
        public static readonly IDivider AutoNewtonDivider;

        #endregion Fields

        #region Constructors

        // .cctor
        static DivideManager()
        {
            // Create new classic divider instance
            IDivider classicDivider = new ClassicDivider();

            // Fill publicity visible divider fields
            ClassicDivider = classicDivider;
            AutoNewtonDivider = new AutoNewtonDivider(classicDivider);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns divider instance for given divide mode.
        /// </summary>
        /// <param name="mode">Divide mode.</param>
        /// <returns>Divider instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode" /> is out of range.</exception>
        public static IDivider GetDivider(DivideMode mode)
        {
            switch (mode)
            {
                case DivideMode.AutoNewton:
                    return AutoNewtonDivider;
                case DivideMode.Classic:
                    return ClassicDivider;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }
        }

        /// <summary>
        /// Returns current divider instance.
        /// </summary>
        /// <returns>Current divider instance.</returns>
        public static IDivider GetCurrentDivider()
        {
            return GetDivider(IntX.GlobalSettings.DivideMode);
        }

        #endregion Methods
    }
}