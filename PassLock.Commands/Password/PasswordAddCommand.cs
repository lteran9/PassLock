using System;
using System.Text;
using PassLock.Core;
using PassLock.EntityFramework;

namespace PassLock.Commands
{
   /// <summary>
   /// Represents a command that adds an encrypted password record to the password database.
   /// </summary>
   public class PasswordAddCommand : ICommand<Task<bool>>
   {
      private readonly IDatabaseModel<Password> _passwordDatabase;
      private readonly Password _encryptedPassword;


      public PasswordAddCommand(IDatabaseModel<Password> repo, Password encryptedPassword)
      {
         _passwordDatabase = repo;
         _encryptedPassword = encryptedPassword;
      }

      public async Task<bool> Execute()
      {
         try
         {
            if (!string.IsNullOrEmpty(_encryptedPassword.Key) && !string.IsNullOrEmpty(_encryptedPassword.Value))
            {
               await _passwordDatabase.InsertAsync(_encryptedPassword);

               return true;
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return false;
      }
   }
}