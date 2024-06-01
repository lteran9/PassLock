using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class AccountRemoveCommand : ICommand<bool>
   {
      public int Id { get; private set; }
      public string Email { get; private set; }
      public string UserName { get; private set; }

      private readonly IDatabaseModel<Account> AccountDatabase;

      public AccountRemoveCommand(IDatabaseModel<Account> repo, int id = 0, string email = "", string username = "")
      {
         AccountDatabase = repo;
         Id = id;
         Email = email;
         UserName = username;
      }

      public bool Execute()
      {
         try
         {
            Account? account = null;

            if (Id > 0)
            {
               account = AccountDatabase.GetById(Id);
            }
            else if (!string.IsNullOrEmpty(Email))
            {
               // Need to find a better way to get the account by email
               var allAccounts = AccountDatabase.GetAll();
               account = allAccounts.FirstOrDefault(a => a.Email == Email);
            }
            else if (!string.IsNullOrEmpty(UserName))
            {
               // Need to find a better way to get the account by username
               var allAccounts = AccountDatabase.GetAll();
               account = allAccounts.FirstOrDefault(a => a.UserName == UserName);
            }

            if (account != null)
            {
               AccountDatabase.Remove(account);
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