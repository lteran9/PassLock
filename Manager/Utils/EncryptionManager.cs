using Newtonsoft.Json;
using PassLock.Manager.DataModels;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassLock.Manager.Utils
{
   public class EncryptionManager
   {
      private List<Password> EncryptedPasswords { get; set; }

      /// <summary>
      /// Encryption manager class that handles all of the encryption and serialization for passwords.
      /// </summary>
      public EncryptionManager()
      {
         EncryptedPasswords = new List<Password>();
      }

      /// <summary>
      /// This method will add a password to the list.
      /// </summary>
      public bool Add(string title, string key, string value)
      {
         if (!string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(key) || !string.IsNullOrEmpty(value))
         {
            // Get key as a byte array
            byte[] byteKey = Encoding.UTF8.GetBytes(GenerateKey(key));
            // Generate randomly for each password must be 16 bytes (128 bits)
            byte[] byteIV = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16));

            // Check that the key does not exist in the list
            if (!DoesEntryExist(title))
            {
               EncryptedPasswords.Add(
                  new Password()
                  {
                     Title = title,
                     Key = byteKey,
                     IV = byteIV,
                     Cipher = Encrypt(value, byteKey, byteIV)
                  });
            }
            else
            {
               // Overwrite existing password
               foreach (var password in EncryptedPasswords)
               {
                  if (password.Title.ToLower() == title.ToLower())
                  {
                     password.Key = byteKey;
                     password.IV = byteIV;
                     password.Cipher = Encrypt(value, byteKey, byteIV);
                  }
               }
            }

            return true;
         }

         return false;
      }

      /// <summary>
      /// Returns the decrypted password as plain text.
      /// </summary>
      public string Get(string title)
      {
         if (DoesEntryExist(title))
         {
            foreach (var password in EncryptedPasswords)
            {
               if (password.Title.ToLower() == title.ToLower())
               {
                  return Decrypt(password.Cipher, password.Key, password.IV);
               }
            }
         }

         return string.Empty;
      }

      /// <summary>
      /// This method will remove a password entry from the list.
      /// </summary>
      public bool Remove(string key)
      {
         if (!string.IsNullOrEmpty(key))
         {
            int removeAtIndex = -1;
            for (int i = 0; i < EncryptedPasswords.Count; i++)
            {
               if (EncryptedPasswords[i].Title.ToLower() == key.ToLower())
               {
                  removeAtIndex = i;
                  break;
               }
            }

            if (removeAtIndex >= 0)
            {
               EncryptedPasswords.RemoveAt(removeAtIndex);

               return true;
            }
         }

         return false;
      }

      public string List()
      {
         var sb = new StringBuilder();

         if (EncryptedPasswords.Count > 0)
         {
            foreach (var entry in EncryptedPasswords)
            {
               sb.Append("\nKey:\t" + entry.Title);
            }
         }
         else
         {
            sb.Append("No passwords to display.");
         }

         return sb.ToString();
      }

      /// <summary>
      /// Loads the encrypted passwords found in the encryption file.
      /// </summary>
      public async Task<bool> Load()
      {
         var content = await FileManager.GetFileContent();

         EncryptedPasswords = Deserialize(content);

         return false;
      }

      /// <summary>
      /// Writes the encrypted passwords to the encryption file.
      /// </summary>
      public bool Save()
      {
         FileManager.SaveContentToFile(Serialize());

         Console.WriteLine("\nEncrypted passwords saved to file.");

         return true;
      }

      public string GenerateKey(string input = null)
      {
         return PadKey(input);
      }

      private string Serialize()
      {
         return JsonConvert.SerializeObject(EncryptedPasswords);
      }

      private List<Password> Deserialize(string json)
      {
         return JsonConvert.DeserializeObject<List<Password>>(json) ?? new List<Password>();
      }

      /// <summary>
      /// Checks the encrypted passwords list in memory and determines if the title exists.
      /// <summary>
      private bool DoesEntryExist(string title)
      {
         if (!string.IsNullOrEmpty(title))
         {
            foreach (var password in EncryptedPasswords)
            {
               if (password.Title.ToLower() == title.ToLower())
               {
                  return true;
               }
            }
         }

         return false;
      }

      /// <summary>
      /// This method will return a string of length 32 regardless of input.
      /// <summary>
      string PadKey(string key)
      {
         if (string.IsNullOrEmpty(key))
         {
            return Guid.NewGuid().ToString().Replace("-", "");
         }
         else
         {
            if (key.Length < 32)
            {
               var paddedKey = new string(key);
               var newGuid = Guid.NewGuid().ToString().Replace("-", "");

               for (int i = 0; i < 32; i++)
               {
                  if (i >= key.Length)
                  {
                     paddedKey += newGuid[i];
                  }
               }
               // Return padded key
               return paddedKey;
            }
            // Return unmodified key
            return key;
         }
      }

      protected static byte[] Encrypt(string plainText, byte[] key, byte[] iv)
      {
         if (string.IsNullOrEmpty(plainText))
            throw new ArgumentException("String must not be null or empty.");
         if (key == null)
            throw new ArgumentNullException("key");
         if (iv == null)
            throw new ArgumentNullException("iv");

         Algorithm.aes.Key = key;
         Algorithm.aes.IV = iv;

         byte[] encrypted = null;

         ICryptoTransform encryptor = Algorithm.aes.CreateEncryptor(Algorithm.aes.Key, Algorithm.aes.IV);

         using (var msEncrypt = new MemoryStream())
         {
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
               using (var swEncrypt = new StreamWriter(csEncrypt))
               {
                  // Write all data to the stream
                  swEncrypt.Write(plainText);
               }
               encrypted = msEncrypt.ToArray();
            }
         }

         return encrypted;
      }

      protected static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
      {
         if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentException("cipherText must not be null or empty.");
         if (key == null || key.Length <= 0)
            throw new ArgumentException("key must not be null or empty.");
         if (iv == null || iv.Length <= 0)
            throw new ArgumentException("iv must not be null or empty.");

         Algorithm.aes.Key = key;
         Algorithm.aes.IV = iv;

         string plainText = null;

         ICryptoTransform decryptor = Algorithm.aes.CreateDecryptor(Algorithm.aes.Key, Algorithm.aes.IV);

         using (var msDecrypt = new MemoryStream(cipherText))
         {
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
               using (var srDecrypt = new StreamReader(csDecrypt))
               {
                  plainText = srDecrypt.ReadToEnd();
               }
            }
         }

         return plainText;
      }
   }
}