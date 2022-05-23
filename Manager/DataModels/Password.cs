using System;

namespace PassLock.Manager.DataModels
{
   public struct Password
   {
      public string Title { get; set; }
      public string Salt { get; set; }

      public byte[] Encrypted { get; set; }

      public Hash HashType { get; set; }

      public Password()
      {
         Title = string.Empty;
         Salt = string.Empty;
         Encrypted = new byte[0];
         HashType = Hash.SHA256;
      }
   }
}