using System;
using System.Collections.Generic;
using PassLock.Manager.DataModels;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

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
         // Find passwords.json file and load all passwords into memory
         EncryptedPasswords = new List<Password>();
      }

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

            newPassword =
             new Password()
             {
                Title = key,
                Salt = salt,
                HashType = hashingMethod,
                Encrypted = value
             };

            EncryptedPasswords.Add(newPassword);

            return true;
         }

         return false;
      }

      /// <summary>
      /// Loads the encrypted passwords found in the encryption file.
      /// </summary>
      public bool Load()
      {
         return false;
      }

      public bool Save()
      {
         return false;
      }

      private string Serialize()
      {
         string serializedPasswordList = JsonConvert.SerializeObject(EncryptedPasswords);

         return serializedPasswordList;
      }

      private Password Deserialize(string encryptedPassword)
      {
         return new Password() { Title = "Empty", Encrypted = string.Empty, Salt = string.Empty, HashType = Hash.SHA256 };
      }
   }
}