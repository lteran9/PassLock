using System;
using PassLock.DataAccess.Entities;

namespace PassLock.DataAccess
{
   /// <summary>
   /// This class handles all CRUD operations for database entities.
   /// </summary>
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

      #region Domains 

      internal void AddDomain(Domain dom)
      {
         if (!string.IsNullOrEmpty(dom?.Url))
         {
            _db.Domains.Add(dom);
            _db.SaveChanges();
         }
      }

      internal void RemoveDomain(int id = 0)
      {
         if (id > 0)
         {
            var dom = new Domain() { Id = id };
            _db.Domains.Attach(dom);
            _db.Domains.Remove(dom);
            _db.SaveChanges();
         }
      }

      internal IEnumerable<Domain>? GetDomains(int id = 0)
      {
         if (id > 0)
         {
            return _db.Domains.Where(x => x.Id == id);
         }

         return _db.Domains.ToList();
      }

      #endregion

      #region Passwords 

      internal int AddPassword(Password pwd)
      {
         if (pwd != null)
         {
            _db.Passwords.Add(pwd);
            _db.SaveChanges();
            return pwd.Id;
         }

         return -1;
      }

      internal Password? GetPassword(int accountId, int domainId)
      {
         if (accountId > 0 && domainId > 0)
         {
            var accountDomainPwd =
               _db.AccountDomainPasswords.FirstOrDefault(x => x.AccountId == accountId && x.DomainId == domainId);

            if (accountDomainPwd != null)
            {
               // Cannot use transitive object found in accountDomainPwd.Password....
               return _db.Passwords.FirstOrDefault(x => x.Id == accountDomainPwd.PasswordId);
            }
         }

         return null;
      }

#pragma warning disable CS8625
      internal void AddAccountPassword(int accountId, int domainId, int passwordId)
      {
         if (accountId > 0 && domainId > 0 && passwordId > 0)
         {
            _db.AccountDomainPasswords.Add(
               new AccountPasswordForDomain()
               {
                  AccountId = accountId,
                  DomainId = domainId,
                  PasswordId = passwordId,
                  // Explicitly remove default objects to prevent creation of new account, password, and domain
                  Account = null,
                  Password = null,
                  Domain = null
               }
            );
            _db.SaveChanges();
         }
      }
#pragma warning restore CS8625

      #endregion
   }
}