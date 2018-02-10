using System;
using System.Security.Cryptography;
using System.IO;

namespace GlobalInfoProtocol.Crypto
{
    public class CryptoService
    {
        private static byte[] bytes = null;
        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="originalString">The original string.</param>
        /// <returns>The encrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown when the original string is null or empty.</exception>
        //public static string Encrypt(string originalString)
        public string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            MemoryStream memoryStream = new MemoryStream();

            try
            {
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(cryptoStream);

                writer.Write(originalString);
                writer.Flush();

                cryptoStream.FlushFinalBlock();
                writer.Flush();
            }
            catch (Exception ex)
            {
                //Logger("EncryptErr", ex.Message);
            }
            String convertedString = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            //Logger("convertedString", convertedString);
            return convertedString;
        }

        /// <summary>
        /// Decrypt a crypted string.
        /// </summary>
        /// <param name="cryptedString">The crypted string.</param>
        /// <returns>The decrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown when the crypted string is null or empty.</exception>
        //public static string Decrypt(string cryptedString)
        public string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);

            return reader.ReadToEnd();
        }
    }
}