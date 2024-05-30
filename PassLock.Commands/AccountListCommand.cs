using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class AccountListCommand : ICommand<bool>
   {
      public List<Account> Accounts { get; private set; }

      public AccountListCommand(List<Account> accounts)
      {
         Accounts = accounts;
      }
   }

   public class AccountListHandler : BaseCommandHandler<AccountListCommand, bool>
   {
      internal override bool ExecuteCommand(AccountListCommand command)
      {
         if (command.Accounts?.Any() == true)
         {
            Console.WriteLine("Accounts:");
            foreach (var acct in command.Accounts)
            {
               Console.WriteLine($"\t{acct.Id}. {acct.Email}");
            }
         }

         return false;
      }
   }
}