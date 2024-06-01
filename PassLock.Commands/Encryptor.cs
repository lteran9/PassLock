using System.Text;
using System.Security.Cryptography;

namespace PassLock.Commands
{
   public static class Encryptor
   {
      /// <summary>
      /// Encrypt a string using the built-in Advanced Encryption Standard (AES).
      /// </summary>
      /// <param name="plaintext"></param>
      /// <returns></returns>
      internal static string Encrypt(string data, out string key, out string iv)
      {
         using (Aes aes = Aes.Create())
         {
            var symmetricEncryptor = aes.CreateEncryptor();

            iv = Convert.ToBase64String(aes.IV);
            key = Convert.ToBase64String(aes.Key);

            using (var memoryStream = new MemoryStream())
            {
               using (var cryptoStream = new CryptoStream(memoryStream, symmetricEncryptor, CryptoStreamMode.Write))
               {
                  using (var streamWriter = new StreamWriter(cryptoStream))
                  {
                     streamWriter.Write(data);
                  }

                  return Convert.ToBase64String(memoryStream.ToArray());
               }
            }
         }
      }

      /// <summary>
      /// Decrypt a string using the built-in Advanced Encryption Standard (AES).
      /// </summary>
      /// <param name="plaintext"></param>
      /// <returns></returns>
      internal static string Decrypt(string cipherText, string key, string iv)
      {
         byte[] buffer = Convert.FromBase64String(cipherText);

         using (Aes aes = Aes.Create())
         {
            var decryptor = aes.CreateDecryptor(Convert.FromBase64String(key), Convert.FromBase64String(iv));

            using (var memoryStream = new MemoryStream(buffer))
            {
               using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
               {
                  using (var streamReader = new StreamReader(cryptoStream))
                  {
                     return streamReader.ReadToEnd();
                  }
               }
            }
         }
      }
   }
}