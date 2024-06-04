using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class DecryptPassword : ICommand<string>
   {
      private readonly Password _password;

      public DecryptPassword(Password encrypted)
      {
         _password = encrypted;
      }

      public string Execute()
      {
         try
         {
            if (_password.Id > 0)
            {
               return Encryptor.Decrypt(_password.Value, _password.Key, _password.InitializationVector);
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