using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class PasswordAddCommand : ICommand<bool>
   {
      public readonly string EncryptedPassword;

      public PasswordAddCommand(IDatabaseModel<Account> repo, string encryptedPassword)
      {
         EncryptedPassword = encryptedPassword;
      }

      public bool Execute()
      {
         return false;
      }
   }
}