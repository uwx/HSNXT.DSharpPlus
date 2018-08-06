using System;
using DSharpPlus.Entities;

namespace HSNXT.DSharpPlus.ModernEmbedBuilder
{
    public struct DuckColor
    {
        /// <summary>
        /// Implicitly converts RGB tuple to a color.
        /// </summary>
        public static implicit operator DuckColor((byte r, byte g, byte b) rgb) => new DuckColor(rgb.r, rgb.g, rgb.b);

        /// <summary>
        /// Implicitly converts RGB float tuple to a color.
        /// </summary>
        public static implicit operator DuckColor((float r, float g, float b) rgb) =>
            new DuckColor(rgb.r, rgb.g, rgb.b);

        /// <summary>
        /// Implicitly converts RGB array to a color.
        /// </summary>
        public static implicit operator DuckColor(byte[] rgb) => new DuckColor(rgb[0], rgb[1], rgb[2]);

        /// <summary>
        /// Implicitly converts RGB float array to a color.
        /// </summary>
        public static implicit operator DuckColor(float[] rgb) => new DuckColor(rgb[0], rgb[1], rgb[2]);

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
        public byte R => (byte) ((Value >> 16) & 0xFF);

        /// <summary>
        /// Gets the green component of this color as an 8-bit integer.
        /// </summary>
        public byte G => (byte) ((Value >> 8) & 0xFF);

        /// <summary>
        /// Gets the blue component of this color as an 8-bit integer.
        /// </summary>
        public byte B => (byte) (Value & 0xFF);

        /// <summary>
        /// Gets an array containing the three color components of this color, each as an 8-bit integer.
        /// </summary>
        public byte[] Components => new[] {R, G, B};

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
                throw new ArgumentOutOfRangeException(nameof(r) + "/" + nameof(g) + "/" + nameof(b),
                    "Each component must be between 0.0 and 1.0 inclusive.");

            var rb = (byte) (r * 255);
            var gb = (byte) (g * 255);
            var bb = (byte) (b * 255);

            Value = rb << 16 | gb << 8 | bb;
        }

        /// <summary>
        /// Returns a new DiscordColor from an HSL value.
        /// </summary>
        /// <param name="h">Hue, from 0 to 360.</param>
        /// <param name="s">Saturation, from 0 to 100.</param>
        /// <param name="l">Lightness, from 0 to 100.</param>
        /// <returns>The equivalent DiscordColor for the <paramref name="h"></paramref>, <paramref name="s"></paramref>
        /// and <paramref name="l"></paramref> values.</returns>
        public static DiscordColor FromHSL(float h, float s, float l)
        {
            var (r, g, b) = HslToRGB(h, s, l);
            return new DiscordColor(r, g, b);
        }

        /// <summary>
        /// Returns a new DiscordColor from an HSB value.
        /// </summary>
        /// <param name="h">Hue, from 0 to 360.</param>
        /// <param name="s">Saturation, from 0 to 100.</param>
        /// <param name="br">Brightness/Value, from 0 to 100.</param>
        /// <returns>The equivalent DiscordColor for the <paramref name="h"></paramref>, <paramref name="s"></paramref>
        /// and <paramref name="br"></paramref> values.</returns>
        public static DiscordColor FromHSB(float h, float s, float br)
        {
            // mind the order of return tuple entries
            // HsvToHls expects saturation and brightness in 0-1 and returns (0-360, 0-1, 0-1)
            var (h1, l1, s1) = HsvToHls(h, s / 100f, br / 100f);
            // HslToRGB expects saturation and lightness in 0-100
            var (r, g, b) = HslToRGB((float) h1, (float) s1 * 100f, (float) l1 * 100f);
            return new DiscordColor(r, g, b);
        }

        /// <summary>
        /// Returns a new DiscordColor from an HSL value.
        /// </summary>
        /// <param name="h">Hue, from 0 to 1.</param>
        /// <param name="s">Saturation, from 0 to 1.</param>
        /// <param name="l">Lightness, from 0 to 1.</param>
        /// <returns>The equivalent DiscordColor for the <paramref name="h"></paramref>, <paramref name="s"></paramref>
        /// and <paramref name="l"></paramref> values.</returns>
        public static DiscordColor FromSmallHSL(float h, float s, float l) => FromHSL(h * 360f, s * 100f, l * 100f);
        
        /// <summary>
        /// Returns a new DiscordColor from an HSB value.
        /// </summary>
        /// <param name="h">Hue, from 0 to 1.</param>
        /// <param name="s">Saturation, from 0 to 1.</param>
        /// <param name="br">Brightness/Value, from 0 to 1.</param>
        /// <returns>The equivalent DiscordColor for the <paramref name="h"></paramref>, <paramref name="s"></paramref>
        /// and <paramref name="br"></paramref> values.</returns>
        public static DiscordColor FromSmallHSB(float h, float s, float br) => FromHSB(h * 360f, s * 100f, br * 100f);

        // https://gist.github.com/peteroupc/4085710
        // Peter O., 2012. http://upokecenter.dreamhosters.com 
        // Public domain dedication: http://creativecommons.org/publicdomain/zero/1.0/
        // Assumes hsv[0] is from 0-360, and hsv[1] and hsv[2] are from 0-1
        private static (double hue, double luminance, double saturation) HsvToHls(double h, double s, double v)
        {
            var luminance = v * (1.0 - (s / 2.0));
            double saturation;
            if (luminance <= 0.5)
            {
                saturation = (Math.Abs(luminance) < 0.005) ? 0 : s * v / (luminance * 2);
            }
            else
            {
                var lumFactor = (0.5 - (luminance - 0.5));
                saturation = (Math.Abs(lumFactor) < 0.005) ? 1.0f : s * v / (lumFactor * 2);
            }

            return (h, luminance, saturation);
        }

        // We assume no responsibility for the code. You are free to use and/or modify and/or distribute any or all code
        // posted on the Java Tips Weblog without restriction. A credit in the code comments would be nice, but not in
        // any way mandatory.
        //
        // http://www.camick.com/java/source/HSLColor.java
        // https://tips4java.wordpress.com/2009/07/05/hsl-color/
        private static (byte r, byte g, byte b) HslToRGB(float h, float s, float l)
        {
            if (s < 0.0f || s > 100.0f)
            {
                throw new ArgumentException("Color parameter outside of expected range - Saturation", nameof(s));
            }

            if (l < 0.0f || l > 100.0f)
            {
                throw new ArgumentException("Color parameter outside of expected range - Luminance", nameof(l));
            }

            //  Formula needs all values between 0 - 1.

            h = h % 360.0f;
            h /= 360f;
            s /= 100f;
            l /= 100f;

            float q;

            if (l < 0.5)
                q = l * (1 + s);
            else
                q = (l + s) - (s * l);

            var p = 2 * l - q;

            var r = Math.Max(0, HueToRGB(p, q, h + (1.0f / 3.0f)));
            var g = Math.Max(0, HueToRGB(p, q, h));
            var b = Math.Max(0, HueToRGB(p, q, h - (1.0f / 3.0f)));

            r = Math.Min(r, 1.0f);
            g = Math.Min(g, 1.0f);
            b = Math.Min(b, 1.0f);

            return ((byte) Math.Round(r * 255), (byte) Math.Round(g * 255), (byte) Math.Round(b * 255));
        }

        private static float HueToRGB(float p, float q, float h)
        {
            if (h < 0) h += 1;

            if (h > 1) h -= 1;

            if (6 * h < 1)
            {
                return p + ((q - p) * 6 * h);
            }

            if (2 * h < 1)
            {
                return q;
            }

            if (3 * h < 2)
            {
                return p + ((q - p) * 6 * ((2.0f / 3.0f) - h));
            }

            return p;
        }

        /*
        // https://stackoverflow.com/a/9493060
        private static (byte r, byte g, byte b) HslToRGB(float h, float s, float l)
        {
            float r, g, b;

            if (Math.Abs(s) < 0.0005)
            {
                r = g = b = l; // achromatic
            }
            else
            {
                var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                var p = 2 * l - q;
                r = Hue2RGB(p, q, h + 1f / 3);
                g = Hue2RGB(p, q, h);
                b = Hue2RGB(p, q, h - 1f / 3);
            }

            return ((byte) Math.Round(r * 255), (byte) Math.Round(g * 255), (byte) Math.Round(b * 255));
        }

        private static float Hue2RGB(float ap, float aq, float at)
        {
            if (at < 0) at += 1;
            if (at > 1) at -= 1;
            if (at < 1f / 6) return ap + (aq - ap) * 6 * at;
            if (at < 1f / 2) return aq;
            if (at < 2f / 3) return ap + (aq - ap) * (2f / 3 - at) * 6;
            return ap;
        }*/
    }
}