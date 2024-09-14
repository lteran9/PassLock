using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class AccountAddCommand : ICommand<Task<bool>>
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

      public async Task<bool> Execute()
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
            await _accountDatabase.InsertAsync(account);

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