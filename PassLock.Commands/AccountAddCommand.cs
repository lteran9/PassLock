using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class AccountAddCommand : ICommand<bool>
   {
      private readonly IDatabaseModel<Account> AccountDatabase;

      public AccountAddCommand(IDatabaseModel<Account> repo)
      {
         AccountDatabase = repo;
      }

      public bool Execute()
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
            AccountDatabase.Insert(account);

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