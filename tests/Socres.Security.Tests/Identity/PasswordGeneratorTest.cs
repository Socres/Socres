namespace Socres.Security.Tests.Identity
{
    using Socres.FakingEasy.AutoFakeItEasy;
    using Socres.Security.Identity;
    using Xunit;

    public class PasswordGeneratorTest
    {
        [Theory]
        [InlineAutoFakeItEasyData(8, 15, 1, 1, 1, 1)]
        public void PasswordGenerator_Generate_Succeeds(
            int minimumLengthPassword,
            int maximumLengthPassword,
            int minimumLowerCaseChars,
            int minimumUpperCaseChars,
            int minimumNumericChars,
            int minimumSpecialChars)
        {
            // Arrange
            var passwordGenerator = new PasswordGenerator();

            // Act
            var actual = passwordGenerator.Generate(
                minimumLengthPassword,
                maximumLengthPassword,
                minimumLowerCaseChars,
                minimumUpperCaseChars,
                minimumNumericChars,
                minimumSpecialChars);

            // Assert
            Assert.NotEmpty(actual);
            Assert.True(actual.Length >= minimumLengthPassword);
            Assert.True(actual.Length <= maximumLengthPassword);
        }
    }
}
