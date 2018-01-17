using Xunit;

namespace HSNXT.Tests
{
    public sealed class SaltedHashTests
    {
        [Fact]
        public void Verify_salted_password_is_verified()
        {
            for (var i = 0; i < 50; i++)
            {
                var password = PasswordGenerator.Generate(32);
                var saltedHash = SaltedHash.Compute(password);
                Assert.True(SaltedHash.Verify(password, saltedHash.Hash, saltedHash.Salt));
            }
        }
    }
}