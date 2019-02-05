using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace HSNXT.DSharpPlus.ModernEmbedBuilder
{
    /// <summary>
    /// Tiny wrapper for Uri to conform with DiscordUri.
    /// </summary>
    internal readonly struct MebUri
    {
        private readonly object _value;

        /// <summary>
        /// The type of this URI.
        /// </summary>
        public MebUriType Type { get; }

        internal MebUri(Uri value)
        {
            this._value = value ?? throw new ArgumentNullException(nameof(value));
            this.Type = MebUriType.Standard;
        }

        internal MebUri(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (IsStandard(value))
            {
                this._value = new Uri(value);
                this.Type = MebUriType.Standard;
            }
            else
            {
                this._value = value;
                this.Type = MebUriType.NonStandard;
            }
        }

        // can be changed in future
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsStandard(string value) => !value.StartsWith("attachment://");

        /// <summary>
        /// Returns a string representation of this DiscordUri.
        /// </summary>
        /// <returns>This DiscordUri, as a string.</returns>
        public override string ToString() => this._value.ToString();

        /// <summary>
        /// Converts this DiscordUri into a canonical representation of a <see cref="Uri"/> if it can be represented as
        /// such, throwing an exception otherwise.
        /// </summary>
        /// <returns>A canonical representation of this DiscordUri.</returns>
        /// <exception cref="UriFormatException">If <see cref="Type"/> is not <see cref="DiscordUriType.Standard"/>, as
        /// that would mean creating an invalid Uri, which would result in loss of data.</exception>
        public Uri ToUri()
            => this.Type == MebUriType.Standard
                ? this._value as Uri
                : throw new UriFormatException(
                    $@"MebUri ""{this._value}"" would be invalid as a regular URI, please the {nameof(this.Type)} property first.");
    }

    internal enum MebUriType : byte
    {
        /// <summary>
        /// Represents a URI that conforms to RFC 3986, meaning it's stored internally as a <see cref="Uri"/> and will
        /// contain a trailing slash after the domain name.
        /// </summary>
        Standard,

        /// <summary>
        /// Represents a URI that does not conform to RFC 3986, meaning it's stored internally as a plain string and
        /// should be treated as one.
        /// </summary>
        NonStandard
    }
}
