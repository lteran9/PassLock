using System;
using PassLock.DataAccess;
using PassLock.DataAccess.Entities;

namespace PassLock
{
   public class Library
   {
      private readonly PDatabaseContext _db;

      public Library()
      {
         _db = new PDatabaseContext();
      }

      #region Accounts 

      internal void AddAccount(Account acc)
      {
         // Verify we have an email or a username
         if (!string.IsNullOrEmpty(acc?.Email) || !string.IsNullOrEmpty(acc?.UserName))
         {
            _db.Accounts.Add(acc);
            _db.SaveChanges();
         }
      }

      internal void RemoveAccount(int id = 0, string email = "", string username = "")
      {
         Account? account = null;

         if (id > 0)
         {
            account = new Account() { Id = id };
         }
         else if (!string.IsNullOrEmpty(email))
         {
            account = new Account() { Email = email };
         }
         else if (!string.IsNullOrEmpty(username))
         {
            account = new Account() { UserName = username };
         }

         if (account != null)
         {
            _db.Accounts.Attach(account);
            _db.Accounts.Remove(account);
            _db.SaveChanges();
         }
      }

      internal IEnumerable<Account>? GetAccounts(int id = 0, string email = "", string username = "")
      {
         if (id > 0)
         {
            return _db.Accounts.Where(x => x.Id == id);
         }
         else if (!string.IsNullOrEmpty(email))
         {
            return _db.Accounts.Where(x => x.Email == email);
         }
         else if (!string.IsNullOrEmpty(username))
         {
            return _db.Accounts.Where(x => x.UserName == username);
         }

         return _db.Accounts;
      }

      #endregion 
   }
}