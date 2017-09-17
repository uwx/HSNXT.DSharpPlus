using System;
using DSharpPlus.Entities;

namespace DSharpPlus.ModernEmbedBuilder
{
    public struct DuckColor
    {
        /// <summary>
        /// Implicitly converts RGB tuple to a color.
        /// </summary>
        public static implicit operator DuckColor((byte r, byte g, byte b) rgb) => new DuckColor(rgb.r, rgb.g, rgb.b);
        
        /// <summary>
        /// Implicitly converts packed int to a color.
        /// </summary>
        public static implicit operator DuckColor(int rgb) => new DuckColor(rgb);
        
        /// <summary>
        /// Implicitly converts packed uint to a color.
        /// </summary>
        public static implicit operator DuckColor(uint rgb) => new DuckColor(rgb);
        
        /// <summary>
        /// Implicitly converts DuckColor to a DiscordColor.
        /// </summary>
        public static implicit operator DiscordColor(DuckColor c) => new DiscordColor(c.Value);
        
        /// <summary>
        /// Implicitly converts DiscordColor to a DuckColor.
        /// </summary>
        public static implicit operator DuckColor(DiscordColor c) => new DuckColor(c.Value);
        
        /// <summary>
        /// Gets the integer representation of this color.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Gets the red component of this color as an 8-bit integer.
        /// </summary>
        public byte R => (byte)((Value >> 16) & 0xFF);

        /// <summary>
        /// Gets the green component of this color as an 8-bit integer.
        /// </summary>
        public byte G => (byte)((Value >> 8) & 0xFF);

        /// <summary>
        /// Gets the blue component of this color as an 8-bit integer.
        /// </summary>
        public byte B => (byte)(Value & 0xFF);

        /// <summary>
        /// Creates a new color with specified value.
        /// </summary>
        /// <param name="color">Value of the color.</param>
        public DuckColor(int color)
        {
            Value = color;
        }
        
        /// <summary>
        /// Creates a new color with specified value.
        /// </summary>
        /// <param name="color">Value of the color.</param>
        public DuckColor(uint color)
        {
            Value = (int) color;
        }

        /// <summary>
        /// Creates a new color with specified values for red, green, and blue components.
        /// </summary>
        /// <param name="r">Value of the red component.</param>
        /// <param name="g">Value of the green component.</param>
        /// <param name="b">Value of the blue component.</param>
        public DuckColor(byte r, byte g, byte b)
        {
            Value = r << 16 | g << 8 | b;
        }
        
        /// <summary>
        /// Creates a new color with specified values for red, green, and blue components.
        /// </summary>
        /// <param name="r">Value of the red component.</param>
        /// <param name="g">Value of the green component.</param>
        /// <param name="b">Value of the blue component.</param>
        public DuckColor(float r, float g, float b)
        {
            if (r < 0 || r > 1 || g < 0 || g > 1 || b < 0 || b > 1)
                throw new ArgumentOutOfRangeException("Each component must be between 0.0 and 1.0 inclusive.");

            var rb = (byte)(r * 255);
            var gb = (byte)(g * 255);
            var bb = (byte)(b * 255);

            Value = rb << 16 | gb << 8 | bb;
        }
    }
}