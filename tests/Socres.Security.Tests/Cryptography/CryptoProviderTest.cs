namespace Socres.Security.Tests.Cryptography
{
    using System;
    using Socres.FakingEasy.AutoFakeItEasy;
    using Socres.Security.Cryptography;
    using Xunit;

    public class CryptoProviderTest
    {
        [Theory]
        [InlineAutoFakeItEasyData("", "TestSecret", "TestSaltKey", "value")]
        [InlineAutoFakeItEasyData("TestValue", "", "TestSaltKey", "secret")]
        [InlineAutoFakeItEasyData("TestValue", "TestSecret", "", "saltKey")]
        public void CryptoProvider_Encrypt_WithEmptyArgument_ThrowsArgumentNullException(
            string value,
            string secret,
            string saltKey,
            string paramName)
        {
            // Arrange
            var cryptoProvider = new CryptoProvider();

            // Act
            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                cryptoProvider.Encrypt(value, secret, saltKey);
            });
            Assert.Equal(paramName, exception.ParamName);
        }

        [Theory]
        [InlineAutoFakeItEasyData("", "TestSecret", "TestSaltKey", "value")]
        [InlineAutoFakeItEasyData("TestValue", "", "TestSaltKey", "secret")]
        [InlineAutoFakeItEasyData("TestValue", "TestSecret", "", "saltKey")]
        public void CryptoProvider_Decrypt_WithEmptyArgument_ThrowsArgumentNullException(
            string value,
            string secret,
            string saltKey,
            string paramName)
        {
            // Arrange
            var cryptoProvider = new CryptoProvider();

            // Act
            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                cryptoProvider.Decrypt(value, secret, saltKey);
            });
            Assert.Equal(paramName, exception.ParamName);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void CryptoProvider_Encrypt_Decrypt_Succeeds(
            string value,
            string secret,
            string saltKey)
        {
            // Arrange
            var cryptoProvider = new CryptoProvider();

            // Act
            var encryptedString = cryptoProvider.Encrypt(value, secret, saltKey);
            var decryptedString = cryptoProvider.Decrypt(encryptedString, secret, saltKey);

            // Assert
            Assert.NotEqual(encryptedString, decryptedString);
            Assert.Equal(value, decryptedString);
        }
    }
}
