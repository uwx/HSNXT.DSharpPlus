using System;

namespace TestProj47
{
    /// <summary>
    /// Used to retrieve needed multiplier.
    /// </summary>
    internal static class MultiplyManager
    {
        #region Fields

        /// <summary>
        /// Classic multiplier instance.
        /// </summary>
        public static readonly IMultiplier ClassicMultiplier;

        /// <summary>
        /// FHT multiplier instance.
        /// </summary>
        public static readonly IMultiplier AutoFhtMultiplier;

        #endregion Fields

        #region Constructors

        // .cctor
        static MultiplyManager()
        {
            // Create new classic multiplier instance
            IMultiplier classicMultiplier = new ClassicMultiplier();

            // Fill publicity visible multiplier fields
            ClassicMultiplier = classicMultiplier;
            AutoFhtMultiplier = new AutoFhtMultiplier(classicMultiplier);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns multiplier instance for given multiply mode.
        /// </summary>
        /// <param name="mode">Multiply mode.</param>
        /// <returns>Multiplier instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode" /> is out of range.</exception>
        public static IMultiplier GetMultiplier(MultiplyMode mode)
        {
            switch (mode)
            {
                case MultiplyMode.AutoFht:
                    return AutoFhtMultiplier;
                case MultiplyMode.Classic:
                    return ClassicMultiplier;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }
        }

        /// <summary>
        /// Returns current multiplier instance.
        /// </summary>
        /// <returns>Current multiplier instance.</returns>
        public static IMultiplier GetCurrentMultiplier()
        {
            return GetMultiplier(IntX.GlobalSettings.MultiplyMode);
        }

        #endregion Methods
    }
}