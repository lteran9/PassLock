using Newtonsoft.Json;
using PassLock.Manager.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassLock.Manager.Utils
{
   public class EncryptionManager
   {
      public List<Password> EncryptedPasswords { get; set; }

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
      public bool Add(string key, string salt, Hash hashingMethod, string value)
      {
         if (!string.IsNullOrEmpty(key) || !string.IsNullOrEmpty(salt) || !string.IsNullOrEmpty(value))
         {
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
            if (!DoesKeyExist(key))
            {
               EncryptedPasswords.Add(
                  new Password()
                  {
                     Title = key,
                     Salt = salt,
                     HashType = hashingMethod,
                     Encrypted = hashValue
                  });
            }
            else
            {
               // Overwrite existing password
               foreach (var password in EncryptedPasswords)
               {
                  if (password.Title.ToLower() == key.ToLower())
                  {
                     password.Salt = salt;
                     password.HashType = hashingMethod;
                     password.Encrypted = hashValue;
                  }
               }
            }

            return true;
         }

         return false;
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

      /// <summary>
      /// Loads the encrypted passwords found in the encryption file.
      /// </summary>
      public async Task<bool> Load()
      {
         var content = await FileManager.GetFileContent();

         EncryptedPasswords = Deserialize(content);

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

      private List<Password> Deserialize(string json)
      {
         return JsonConvert.DeserializeObject<List<Password>>(json) ?? new List<Password>();
      }

      private bool DoesKeyExist(string key)
      {
         foreach (var password in EncryptedPasswords)
         {
            if (password.Title.ToLower() == key.ToLower())
            {
               return true;
            }
         }

         return false;
      }
   }
}