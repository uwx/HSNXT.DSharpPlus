﻿using HSNXT.SuccincT.Options;

namespace HSNXT.SuccincT.Parsers
{
    /// <summary>
    /// Defines a set of string extension methods for parsing float, double and decimal
    /// values in an elegant fashion (avoiding exception throwing and out parameters).
    /// </summary>
    public static class FloatParsers
    {
        /// <summary>
        /// Parses the current string for a 32 bit float value and returns an Option{float} with None or the value.
        /// </summary>
        public static Option<float> TryParseFloat(this string source) =>
            float.TryParse(source, out var result) ? Option<float>.Some(result) : Option<float>.None();

        /// <summary>
        /// Parses the current string for a 64 bit float value and returns an Option{double} with None or the value.
        /// </summary>
        public static Option<double> TryParseDouble(this string source) =>
            double.TryParse(source, out var result) ? Option<double>.Some(result) : Option<double>.None();

        /// <summary>
        /// Parses the current string for a 128 bit float value and returns an Option{decimal} with None or the value.
        /// </summary>
        public static Option<decimal> TryParseDecimal(this string source) =>
            decimal.TryParse(source, out var result) ? Option<decimal>.Some(result) : Option<decimal>.None();
    }
}