namespace HSNXT
{
    /// <summary>
    /// <see cref="IntX" /> instance settings.
    /// </summary>
    public struct IntXSettings
    {
        #region Private fields

        #endregion Private fields

        /// <summary>
        /// Creates new <see cref="IntXSettings" /> instance.
        /// </summary>
        /// <param name="globalSettings">IntX global settings to copy.</param>
        internal IntXSettings(IntXGlobalSettings globalSettings)
        {
            // Copy local settings from global ones
            AutoNormalize = globalSettings.AutoNormalize;
            ToStringMode = globalSettings.ToStringMode;
        }

        #region Public properties

        /// <summary>
        /// To string conversion mode used in this <see cref="IntX" /> instance.
        /// Set to value from <see cref="IntX.GlobalSettings" /> by default.
        /// </summary>
        public ToStringMode ToStringMode { get; set; }

        /// <summary>
        /// If true then each operation is ended with big integer normalization.
        /// Set to value from <see cref="IntX.GlobalSettings" /> by default.
        /// </summary>
        public bool AutoNormalize { get; set; }

        #endregion Public properties
    }
}