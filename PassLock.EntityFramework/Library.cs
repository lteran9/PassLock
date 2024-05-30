using System;
using System.Collections.Generic;
using PassLock.Core;

namespace PassLock.EntityFramework
{
   /// <summary>
   /// This class handles all CRUD operations for database entities.
   /// </summary>
   public class Library
   {
      #region Accounts 

      public void AddAccount(Account acc)
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

      public void RemoveAccount(int id = 0, string email = "", string username = "")
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

      public List<Account>? GetAccounts(int id = 0, string email = "", string username = "")
      {
         using (var db = new PDatabaseContext())
         {
            if (id > 0)
            {
               return db.Accounts.Where(x => x.Id == id).ToList();
            }
            else if (!string.IsNullOrEmpty(email))
            {
               return db.Accounts.Where(x => x.Email == email).ToList();
            }
            else if (!string.IsNullOrEmpty(username))
            {
               return db.Accounts.Where(x => x.UserName == username).ToList();
            }

            return db.Accounts.ToList();
         }
      }

      #endregion

      #region Domains 

      public void AddDomain(Domain dom)
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

      public void RemoveDomain(int id = 0)
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

      public List<Domain>? GetDomains(int id = 0)
      {
         using (var db = new PDatabaseContext())
         {
            if (id > 0)
            {
               return db.Domains.Where(x => x.Id == id).ToList();
            }

            return db.Domains.ToList();
         }
      }

      #endregion

      #region Passwords 

      public int AddPassword(Password pwd)
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

      public Password? GetPassword(int accountId, int domainId)
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
      public void AddAccountPassword(int accountId, int domainId, int passwordId)
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
                     // Account = null,
                     // Password = null,
                     // Domain = null
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