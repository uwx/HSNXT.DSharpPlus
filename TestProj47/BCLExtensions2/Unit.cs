using System;

namespace HSNXT.BCLExtensions2
{
    /// <inheritdoc cref="IEquatable{T}" />
    /// <summary>
    /// Represents a void where a type is required.
    /// </summary>
    public struct Unit : IEquatable<Unit>
    {
        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Unit other)
        {
            return true;
        }

        /// <summary>
        /// Gets a default Unit.
        /// </summary>
        public static Unit Default => default;
    }
}