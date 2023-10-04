using System.Text;
using System.Security.Cryptography;

namespace PassLock.Config
{
   public static class Encryptor
   {
      /// <summary>
      /// Encrypt a string using the built-in Advanced Encryption Standard (AES).
      /// </summary>
      /// <param name="plaintext"></param>
      /// <returns></returns>
      internal static string Encrypt(string data, out string key)
      {
         using (Aes aes = Aes.Create())
         {
            var symmetricEncryptor = aes.CreateEncryptor();
            key = Convert.ToHexString(aes.Key);
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
      internal static string Decrypt(string cipherText, string key)
      {
         byte[] buffer = Convert.FromBase64String(cipherText);

         using (Aes aes = Aes.Create())
         {
            aes.Key = Encoding.UTF8.GetBytes(key);
            //aes.IV = _IV;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

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