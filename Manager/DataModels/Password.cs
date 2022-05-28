using System;
using PassLock.Manager.Utils;

namespace PassLock.Manager.DataModels
{
   public class Password
   {
      public string Title { get; set; }

      public byte[] Key { get; set; }
      public byte[] IV { get; set; }
      public byte[] Cipher { get; set; }
   }
}