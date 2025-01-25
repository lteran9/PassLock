using System;
using PassLock.Core;
using PassLock.EntityFramework;

namespace PassLock.Queries
{
   public class AccountListCommand : IQuery<Task<bool>>
   {
      private readonly IDatabaseModel<Account> _accountDatabase;

      public AccountListCommand(IDatabaseModel<Account> repo)
      {
         _accountDatabase = repo;
      }

      public async Task<bool> Execute()
      {
         try
         {
            var accounts = await _accountDatabase.GetAllAsync();

            if (accounts?.Any() == true)
            {
               Console.WriteLine("Accounts:");
               foreach (var acct in accounts)
               {
                  Console.WriteLine($"\t{acct.Id}. {acct.Email}");
               }
            }
            else
            {
               Console.WriteLine("No Accounts found in database.");
            }

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