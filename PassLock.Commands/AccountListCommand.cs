using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class AccountListCommand : ICommand<bool>
   {
      public IDatabaseModel<Account> AccountDatabase { get; private set; }

      public AccountListCommand(IDatabaseModel<Account> repo)
      {
         AccountDatabase = repo;
      }
   }

   public class AccountListHandler : BaseCommandHandler<AccountListCommand, bool>
   {
      internal override bool ExecuteCommand(AccountListCommand command)
      {
         try
         {
            var accounts = command.AccountDatabase.GetAll();

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
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return false;
      }
   }
}