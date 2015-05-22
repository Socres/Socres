namespace Socres.Security.Cryptography
{
    public interface ICryptoProvider
    {
        /// <summary>
        /// Encrypts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="secret">The secret.</param>
        /// <param name="saltKey">The salt key.</param>
        /// <returns></returns>
        string Encrypt(string value, string secret, string saltKey);

        /// <summary>
        /// Decrypts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="secret">The secret.</param>
        /// <param name="saltKey">The salt key.</param>
        /// <returns></returns>
        string Decrypt(string value, string secret, string saltKey);
    }
}
