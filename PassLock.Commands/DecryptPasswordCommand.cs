using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class DecryptPassword : ICommand<string>
   {
      private readonly Password Password;

      public DecryptPassword(Password encrypted)
      {
         Password = encrypted;
      }

      public string Execute()
      {
         try
         {
            if (Password.Id > 0)
            {
               return Encryptor.Decrypt(Password.Value, Password.Key, Password.InitializationVector);
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return string.Empty;
      }
   }
}