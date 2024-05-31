using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class AccountRemoveCommand : ICommand<bool>
   {
      public int Id { get; private set; }
      public string Email { get; private set; }
      public string UserName { get; private set; }

      public IDatabaseModel<Account> AccountDatabase { get; private set; }

      public AccountRemoveCommand(IDatabaseModel<Account> repo, int id = 0, string email = "", string username = "")
      {
         AccountDatabase = repo;
         Id = id;
         Email = email;
         UserName = username;
      }
   }

   public class AccountRemoveHandler : BaseCommandHandler<AccountRemoveCommand, bool>
   {

      internal override bool ExecuteCommand(AccountRemoveCommand command)
      {
         try
         {
            Account? account = null;

            if (command.Id > 0)
            {
               account = command.AccountDatabase.GetById(command.Id);
            }
            else if (!string.IsNullOrEmpty(command.Email))
            {
               // Need to find a better way to get the account by email
               var allAccounts = command.AccountDatabase.GetAll();
               account = allAccounts.FirstOrDefault(a => a.Email == command.Email);
            }
            else if (!string.IsNullOrEmpty(command.UserName))
            {
               // Need to find a better way to get the account by username
               var allAccounts = command.AccountDatabase.GetAll();
               account = allAccounts.FirstOrDefault(a => a.UserName == command.UserName);
            }

            if (account != null)
            {
               command.AccountDatabase.Remove(account);
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