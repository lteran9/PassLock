using System;

namespace PassLock.Manager.DataModels
{
   public struct Password
   {
      public string Title { get; set; }
      public string Salt { get; set; }

      public byte[] Encrypted { get; set; }

      public Hash HashType { get; set; }
   }
}