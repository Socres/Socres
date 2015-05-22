namespace Socres.Security.Cryptography
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class CryptoProvider : ICryptoProvider
    {
        /// <summary>
        /// Encrypts the specified plain text.
        /// </summary>
        /// <param name="value">The plain text.</param>
        /// <param name="secret">The secret.</param>
        /// <param name="saltKey">The salt key.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">value
        /// or
        /// secret</exception>
        public string Encrypt(string value, string secret, string saltKey)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException("secret");
            }
            if (string.IsNullOrEmpty(saltKey))
            {
                throw new ArgumentNullException("saltKey");
            }

            string outStr;
            RijndaelManaged aesAlg = null;
            try
            {
                var key = new Rfc2898DeriveBytes(secret, Encoding.ASCII.GetBytes(saltKey));

                // Create a RijndaelManaged object
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decryptor to perform the stream transform.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    // prepend the IV
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(value);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                {
                    aesAlg.Clear();
                }
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }

        /// <summary>
        /// Decrypts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="secret">The secret.</param>
        /// <param name="saltKey">The salt key.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">value
        /// or
        /// secret</exception>
        public string Decrypt(string value, string secret, string saltKey)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException("secret");
            }
            if (string.IsNullOrEmpty(saltKey))
            {
                throw new ArgumentNullException("saltKey");
            }

            RijndaelManaged aesAlg = null;
            string plaintext;
            try
            {
                // generate the key from the shared secret and the salt
                var key = new Rfc2898DeriveBytes(secret, Encoding.ASCII.GetBytes(saltKey));

                // Create the streams used for decryption.                
                var bytes = Convert.FromBase64String(value);
                using (var msDecrypt = new MemoryStream(bytes))
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Create a decrytor to perform the stream transform.
                    var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                {
                    aesAlg.Clear();
                }
            }

            return plaintext;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            var rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}
