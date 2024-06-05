using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class AccountRemoveCommand : ICommand<bool>
   {
      private readonly int _id;
      private readonly string _email;
      private readonly string _userName;

      private readonly IDatabaseModel<Account> _accountDatabase;

      public AccountRemoveCommand(IDatabaseModel<Account> repo, int id = 0, string email = "", string username = "")
      {
         _accountDatabase = repo;
         _id = id;
         _email = email;
         _userName = username;
      }

      public bool Execute()
      {
         try
         {
            Account? account = null;

            if (_id > 0)
            {
               account = _accountDatabase.GetById(new Account() { Id = _id });
            }
            else if (!string.IsNullOrEmpty(_email))
            {
               // Need to find a better way to get the account by email
               var allAccounts = _accountDatabase.GetAll();
               account = allAccounts.FirstOrDefault(a => a.Email == _email);
            }
            else if (!string.IsNullOrEmpty(_userName))
            {
               // Need to find a better way to get the account by username
               var allAccounts = _accountDatabase.GetAll();
               account = allAccounts.FirstOrDefault(a => a.UserName == _userName);
            }

            if (account != null)
            {
               _accountDatabase.Remove(account);
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