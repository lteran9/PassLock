using System;
using System.Security.Cryptography;

namespace PassLock.Manager.Utils
{
   public class Algorithm
   {
      public static MD5 md5 = MD5.Create();
      public static SHA256 sha256 = SHA256.Create();
      public static SHA512 sha512 = SHA512.Create();

   }

}