using System;
using System.Collections.Generic;
using PassLock.Manager.DataModels;
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

      public string Serialize()
      {
         string serializedPasswordList = JsonConvert.SerializeObject(EncryptedPasswords);

         return serializedPasswordList;
      }

      public Password Deserialize(string encryptedPassword)
      {
         return new Password() { Title = "Empty", Encrypted = string.Empty, Salt = string.Empty, HashType = Hash.SHA256 };
      }
   }
}