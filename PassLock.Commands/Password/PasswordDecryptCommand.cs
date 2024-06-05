using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class PasswordDecryptCommand : ICommand<string>
   {
      private readonly Password? _password;

      public PasswordDecryptCommand(Password? password)
      {
         _password = password;
      }

      public string Execute()
      {
         if (_password != null)
         {
            return Encryptor.Decrypt(_password.Value, _password.Key, _password.InitializationVector);
         }

         return string.Empty;
      }
   }
}