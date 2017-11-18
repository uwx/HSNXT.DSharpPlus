namespace HSNXT
{
    /// <summary>
    /// <see cref="IntX" /> global settings.
    /// </summary>
    public sealed class IntXGlobalSettings
    {
        #region Private fields

        private volatile MultiplyMode _multiplyMode = MultiplyMode.AutoFht;
        private volatile DivideMode _divideMode = DivideMode.AutoNewton;
        private volatile ParseMode _parseMode = ParseMode.Fast;
        private volatile ToStringMode _toStringMode = ToStringMode.Fast;
        private volatile bool _autoNormalize;
        private volatile bool _applyFhtValidityCheck = true;

        #endregion Private fields

        // Class can be created only inside this assembly
        internal IntXGlobalSettings()
        {
        }

        #region Public properties

        /// <summary>
        /// Multiply operation mode used in all <see cref="IntX" /> instances.
        /// Set to auto-FHT by default.
        /// </summary>
        public MultiplyMode MultiplyMode
        {
            get { return _multiplyMode; }
            set { _multiplyMode = value; }
        }

        /// <summary>
        /// Divide operation mode used in all <see cref="IntX" /> instances.
        /// Set to auto-Newton by default.
        /// </summary>
        public DivideMode DivideMode
        {
            get { return _divideMode; }
            set { _divideMode = value; }
        }

        /// <summary>
        /// Parse mode used in all <see cref="IntX" /> instances.
        /// Set to Fast by default.
        /// </summary>
        public ParseMode ParseMode
        {
            get { return _parseMode; }
            set { _parseMode = value; }
        }

        /// <summary>
        /// To string conversion mode used in all <see cref="IntX" /> instances.
        /// Set to Fast by default.
        /// </summary>
        public ToStringMode ToStringMode
        {
            get { return _toStringMode; }
            set { _toStringMode = value; }
        }

        /// <summary>
        /// If true then each operation is ended with big integer normalization.
        /// Set to false by default.
        /// </summary>
        public bool AutoNormalize
        {
            get { return _autoNormalize; }
            set { _autoNormalize = value; }
        }

        /// <summary>
        /// If true then FHT multiplication result is always checked for validity
        /// by multiplying integers lower digits using classic algorithm and comparing with FHT result.
        /// Set to true by default.
        /// </summary>
        public bool ApplyFhtValidityCheck
        {
            get { return _applyFhtValidityCheck; }
            set { _applyFhtValidityCheck = value; }
        }

        #endregion Public properties
    }
}