using System.Collections.Generic;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.Extended.ExtensionMethods
{
    /// <summary>
    /// A simple implementation of <see cref="IEqualityComparer{T}"/> for <see cref="SnowflakeObject"/>, that compares
    /// by ID.
    /// </summary>
    public readonly struct SnowflakeEqualityComparer : IEqualityComparer<SnowflakeObject>
    {
        public bool Equals(SnowflakeObject x, SnowflakeObject y) => x.Id == y.Id;

        public int GetHashCode(SnowflakeObject obj) => obj.Id.GetHashCode();
    }
}