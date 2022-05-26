using System;
using PassLock.Manager.Utils;

namespace PassLock.Manager.DataModels
{
   public class Password
   {
      public string Title { get; set; }
      public string Salt { get; set; }

      public byte[] Encrypted { get; set; }

      public Algorithm.Hash HashType { get; set; }
   }
}