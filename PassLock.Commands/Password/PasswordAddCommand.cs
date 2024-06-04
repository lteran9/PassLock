using System;
using System.Text;
using PassLock.Core;

namespace PassLock.Commands
{
   public class PasswordAddCommand : ICommand<bool>
   {
      private readonly IDatabaseModel<Password> _passwordDatabase;
      private readonly Password _encryptedPassword;


      public PasswordAddCommand(IDatabaseModel<Password> repo, Password encryptedPassword)
      {
         _passwordDatabase = repo;
         _encryptedPassword = encryptedPassword;
      }

      public bool Execute()
      {
         try
         {
            _passwordDatabase.Insert(_encryptedPassword);

            return true;
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return false;
      }
   }
}