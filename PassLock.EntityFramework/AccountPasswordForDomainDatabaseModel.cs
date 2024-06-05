using System;
using PassLock.Core;
using PassLock.Commands;

namespace PassLock.EntityFramework
{
   public class AccountPasswordForDomainDatabaseModel : IDatabaseModel<AccountPasswordForDomain>
   {
      public List<AccountPasswordForDomain> GetAll()
      {
         using (var db = new PDatabaseContext())
         {
            return db.AccountDomainPasswords.ToList();
         }
      }

      /// <summary>
      /// Can't use this method because <c>AccountPasswordForDomain</c> uses a composite key.
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public AccountPasswordForDomain GetById(AccountPasswordForDomain model)
      {
         using (var db = new PDatabaseContext())
         {
            return db.AccountDomainPasswords
               .FirstOrDefault(x =>
                  x.AccountId == model.AccountId &&
                  x.DomainId == model.DomainId) ?? new AccountPasswordForDomain();
         }
      }


      public void Insert(AccountPasswordForDomain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.AccountDomainPasswords.Add(model);
            db.SaveChanges();
         }
      }

      public void Remove(AccountPasswordForDomain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.AccountDomainPasswords.Remove(model);
            db.SaveChanges();
         }
      }

      public void Update(AccountPasswordForDomain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.AccountDomainPasswords.Update(model);
            db.SaveChanges();
         }
      }
   }
}