namespace Socres.Security.Identity
{
    /// <summary>
    /// Generate a random password
    /// </summary>
    public interface IPasswordGenerator
    {
        /// <summary>
        /// Generates this password.
        /// </summary>
        /// <param name="minimumLengthPassword">The minimum length password.</param>
        /// <param name="maximumLengthPassword">The maximum length password.</param>
        /// <param name="minimumLowerCaseChars">The minimum lower case chars.</param>
        /// <param name="minimumUpperCaseChars">The minimum upper case chars.</param>
        /// <param name="minimumNumericChars">The minimum numeric chars.</param>
        /// <param name="minimumSpecialChars">The minimum special chars.</param>
        /// <returns></returns>
        string Generate(int minimumLengthPassword,
            int maximumLengthPassword,
            int minimumLowerCaseChars,
            int minimumUpperCaseChars,
            int minimumNumericChars,
            int minimumSpecialChars);
    }
}