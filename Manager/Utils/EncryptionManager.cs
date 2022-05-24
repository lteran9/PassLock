using Newtonsoft.Json;
using PassLock.Manager.DataModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassLock.Manager.Utils
{
   public class EncryptionManager
   {
      public List<Password> EncryptedPasswords { get; set; } = new List<Password>();

      /// <summary>
      /// Encryption manager class that handles all of the encryption and serialization for passwords.
      /// </summary>
      public EncryptionManager() { }

      /// <summary>
      /// This method will add a password to the list.
      /// </summary>
      public bool Add(string key, string salt, Hash hashingMethod, string value)
      {
         if (!string.IsNullOrEmpty(key) || !string.IsNullOrEmpty(salt) || !string.IsNullOrEmpty(value))
         {
            var newPassword = new Password();
            byte[] hashValue = new byte[0];

            if (hashingMethod == Hash.MD5)
            {
               hashValue = Algorithm.md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
            else if (hashingMethod == Hash.SHA256)
            {
               hashValue = Algorithm.sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
            else if (hashingMethod == Hash.SHA512)
            {
               hashValue = Algorithm.sha512.ComputeHash(Encoding.UTF8.GetBytes(value));
            }

            // Check that the key does not exist in the list.

            newPassword =
               new Password()
               {
                  Title = key,
                  Salt = salt,
                  HashType = hashingMethod,
                  Encrypted = hashValue
               };

            EncryptedPasswords.Add(newPassword);

            return true;
         }

         return false;
      }

      /// <summary>
      /// Loads the encrypted passwords found in the encryption file.
      /// </summary>
      public async Task<bool> Load()
      {
         var content = await FileManager.GetFileContent();

         EncryptedPasswords = JsonConvert.DeserializeObject<List<Password>>(content);

         return false;
      }

      public bool Save()
      {
         Console.WriteLine(Serialize());

         FileManager.SaveContentToFile(Serialize());

         return true;
      }

      private string Serialize()
      {
         return JsonConvert.SerializeObject(EncryptedPasswords);
      }

      private Password Deserialize(string encryptedPassword)
      {
         return new Password();
      }
   }
}