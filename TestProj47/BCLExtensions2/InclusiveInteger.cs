namespace HSNXT
{
    /// <summary>
    /// Represents a semantically inclusive integer.
    /// </summary>
    public struct InclusiveInteger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InclusiveInteger"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public InclusiveInteger(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; }
    }
}