using System;
using System.Runtime.InteropServices;
using System.Security;

using Shouldly;

using Xunit;

namespace HSNXT.Tests
{
    public sealed class PasswordGeneratorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Zero_or_negative_password_length_throws_exception(int length)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => PasswordGenerator.Generate(length));

            Should.Throw<ArgumentOutOfRangeException>(() => PasswordGenerator.GenerateSecure(length));
        }

        [Fact]
        public void Generated_password_lengths_equals_input_length()
        {
            for (var i = 1; i <= 50; i++)
            {
                var password = PasswordGenerator.Generate(i);
                password.Length.ShouldBe(i);

                var securePassword = PasswordGenerator.GenerateSecure(i);
                securePassword.Length.ShouldBe(i);
            }
        }

        [Theory]
        [InlineData(PasswordCharacters.Numbers, "13579")]
        [InlineData(PasswordCharacters.AllLetters, "JjMmRrEe")]
        [InlineData(PasswordCharacters.AlphaNumeric, "OlIS015")]
        public void Excluded_characters_are_excluded(PasswordCharacters allowedCharacters, string excludeCharacters)
        {
            for (var i = 0; i < 50; i++)
            {
                var password = PasswordGenerator.Generate(40, allowedCharacters, excludeCharacters);
                foreach (var excludeChar in excludeCharacters)
                    password.ShouldNotContain(excludeChar);

                var securePassword = PasswordGenerator.GenerateSecure(40, allowedCharacters, excludeCharacters);
                foreach (var excludeChar in excludeCharacters)
                    SecureStringToString(securePassword).ShouldNotContain(excludeChar);
            }
        }

        // Converts a SecureString to a string
        private static string SecureStringToString(SecureString secureString)
        {
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(valuePtr);
            } finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
