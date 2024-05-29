using System;
using PassLock.DataAccess.Entities;

namespace PassLock.DataAccess
{
   /// <summary>
   /// This class handles all CRUD operations for database entities.
   /// </summary>
   public class Library
   {
      #region Accounts 

      internal void AddAccount(Account acc)
      {
         using (var db = new PDatabaseContext())
         {
            // Verify we have an email or a username
            if (!string.IsNullOrEmpty(acc?.Email) || !string.IsNullOrEmpty(acc?.UserName))
            {
               db.Accounts.Add(acc);
               db.SaveChanges();
            }
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
            using (var db = new PDatabaseContext())
            {
               db.Accounts.Attach(account);
               db.Accounts.Remove(account);
               db.SaveChanges();
            }
         }
      }

      internal IEnumerable<Account>? GetAccounts(int id = 0, string email = "", string username = "")
      {
         using (var db = new PDatabaseContext())
         {
            if (id > 0)
            {
               return db.Accounts.Where(x => x.Id == id);
            }
            else if (!string.IsNullOrEmpty(email))
            {
               return db.Accounts.Where(x => x.Email == email);
            }
            else if (!string.IsNullOrEmpty(username))
            {
               return db.Accounts.Where(x => x.UserName == username);
            }

            return db.Accounts;
         }
      }

      #endregion

      #region Domains 

      internal void AddDomain(Domain dom)
      {
         if (!string.IsNullOrEmpty(dom?.Url))
         {
            using (var db = new PDatabaseContext())
            {
               db.Domains.Add(dom);
               db.SaveChanges();
            }
         }
      }

      internal void RemoveDomain(int id = 0)
      {
         if (id > 0)
         {
            using (var db = new PDatabaseContext())
            {
               var dom = new Domain() { Id = id };
               db.Domains.Attach(dom);
               db.Domains.Remove(dom);
               db.SaveChanges();
            }
         }
      }

      internal IEnumerable<Domain>? GetDomains(int id = 0)
      {
         using (var db = new PDatabaseContext())
         {
            if (id > 0)
            {
               return db.Domains.Where(x => x.Id == id);
            }

            return db.Domains.ToList();
         }
      }

      #endregion

      #region Passwords 

      internal int AddPassword(Password pwd)
      {
         if (pwd != null)
         {
            using (var db = new PDatabaseContext())
            {
               db.Passwords.Add(pwd);
               db.SaveChanges();
               return pwd.Id;
            }
         }

         return -1;
      }

      internal Password? GetPassword(int accountId, int domainId)
      {
         if (accountId > 0 && domainId > 0)
         {
            using (var db = new PDatabaseContext())
            {
               var accountDomainPwd =
                  db.AccountDomainPasswords.FirstOrDefault(x => x.AccountId == accountId && x.DomainId == domainId);

               if (accountDomainPwd != null)
               {
                  // Cannot use transitive object found in accountDomainPwd.Password....
                  return db.Passwords.FirstOrDefault(x => x.Id == accountDomainPwd.PasswordId);
               }
            }
         }

         return null;
      }

#pragma warning disable CS8625
      internal void AddAccountPassword(int accountId, int domainId, int passwordId)
      {
         if (accountId > 0 && domainId > 0 && passwordId > 0)
         {
            using (var db = new PDatabaseContext())
            {
               db.AccountDomainPasswords.Add(
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
               db.SaveChanges();
            }
         }
      }
#pragma warning restore CS8625

      #endregion
   }
}