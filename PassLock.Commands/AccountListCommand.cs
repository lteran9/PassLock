using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class AccountListCommand : ICommand<bool>
   {
      private readonly IDatabaseModel<Account> AccountDatabase;

      public AccountListCommand(IDatabaseModel<Account> repo)
      {
         AccountDatabase = repo;
      }

      public bool Execute()
      {
         try
         {
            var accounts = AccountDatabase.GetAll();

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