using System;
using PassLock.Core;
using PassLock.Commands;

namespace PassLock.Commands
{
   public class AccountAddCommand : ICommand<bool>
   {
      public IDatabaseModel<Account> AccountDatabase { get; private set; }

      public AccountAddCommand(IDatabaseModel<Account> repo)
      {
         AccountDatabase = repo;
      }
   }

   public class AccountAddHandler : BaseCommandHandler<AccountAddCommand, bool>
   {
      internal override bool ExecuteCommand(AccountAddCommand command)
      {
         try
         {
            // Get input from user
            Console.Write("Please enter email: ");
            var email = Console.ReadLine();
            Console.Write("Please enter username: ");
            var username = Console.ReadLine();
            var account = new Account() { Email = email, UserName = username };
            // Insert to database
            command.AccountDatabase.Insert(account);

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