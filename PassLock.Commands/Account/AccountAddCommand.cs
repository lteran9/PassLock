using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class AccountAddCommand : ICommand<bool>
   {
      private readonly IDatabaseModel<Account> _accountDatabase;
      private readonly string _email;
      private readonly string _userName;

      public AccountAddCommand(IDatabaseModel<Account> repo, string email, string userName)
      {
         _accountDatabase = repo;
         _email = email;
         _userName = userName;
      }

      public bool Execute()
      {
         try
         {
            var account =
               new Account()
               {
                  Email = _email,
                  UserName = _userName
               };
            // Insert to database
            _accountDatabase.Insert(account);

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