using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace samsung_api.Extensions
{
    public static class StringExtensions
    {
        public static string Encrypt(this string plainText, string key, string iv)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key can not be empty", nameof(key));

            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentException("Text can not be empty", nameof(plainText));

            if (string.IsNullOrWhiteSpace(iv))
                throw new ArgumentException("IV can not be empty", nameof(iv));

            using (var rijCrypto = new RijndaelManaged())
            using (var sha = SHA256.Create())
            {
                rijCrypto.Padding = PaddingMode.PKCS7;
                using (var output = new MemoryStream())
                {
                    var encryptor = rijCrypto.CreateEncryptor(sha.ComputeHash(Encoding.UTF8.GetBytes(key)),
                        Encoding.UTF8.GetBytes(iv));

                    using (var cryptoStream = new CryptoStream(output, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(output.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(this string encryptedText, string key, string iv)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key can not be empty", nameof(key));

            if (string.IsNullOrWhiteSpace(encryptedText))
                throw new ArgumentException("Text can not be empty", nameof(encryptedText));

            if (string.IsNullOrWhiteSpace(iv))
                throw new ArgumentException("IV can not be empty", nameof(iv));

            var bytes = Convert.FromBase64String(encryptedText);
            using (var rijCrypto = new RijndaelManaged())
            using (var sha = SHA256.Create())
            {
                rijCrypto.Padding = PaddingMode.PKCS7;
                using (var output = new MemoryStream())
                {
                    var decryptor = rijCrypto.CreateDecryptor(sha.ComputeHash(Encoding.UTF8.GetBytes(key)),
                        Encoding.UTF8.GetBytes(iv));

                    using (var cryptoStream = new CryptoStream(output, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return Encoding.UTF8.GetString(output.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Parses a path entry to an int.
        /// </summary>
        /// <param name="index">a path entry</param>
        /// <returns>a number</returns>
        /// <exception cref="InvalidArgumentException">In case the path entry is not a number</exception>
        public static int ParseToIndex(this string index)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException(nameof(index));

            if (!int.TryParse(index, out var result))
                throw new ArgumentException($"Could not convert string '{index}' to index");
            if (result <= 0)
                throw new ArgumentOutOfRangeException(nameof(index), index);
            return result - 1;
        }

        /// <summary>
        ///     Parses a path entry to an int.
        /// </summary>
        /// <param name="index">a path entry</param>
        /// <returns>a number</returns>
        /// <exception cref="InvalidArgumentException">In case the path entry is not a number</exception>
        public static int ParseIndex(this string index)
        {
            try
            {
                return int.Parse(index) - 1;
            }
            catch (FormatException)
            {
                throw new ArgumentException($"Invalid index: {index}");
            }
        }

        /// <summary>
        /// Try the deserialize json.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <param name="response">The response.</param>
        /// <param name="error">The error.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <typeparam name="T"></typeparam>
        public static bool TryDeserializeJson<T>(this string str, out T response)
            where T : new()
        {
            response = default;
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            try
            {
                response = JsonConvert.DeserializeObject<T>(
                    str,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                );
                return true;
            }
            catch (JsonSerializationException)
            {
                // These exceptions occur if the json is incorrectly formated.
                // These exceptions also occur when serialization settings are broken.
                // This exception can be used to return more specific information to the implementing side. Which makes it easier to debug.

                return false;
            }
            catch (JsonException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}