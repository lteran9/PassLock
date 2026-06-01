using System;
using PassLock.Core;

namespace PassLock.Commands
{
   /// <summary>
   /// Represents a command that decrypts a stored password record.
   /// </summary>
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