using System;
using PassLock.Commands;
using PassLock.Core;

namespace PassLock.EntityFramework
{
   public class AccountDatabaseModel : IDatabaseModel<Account>
   {
      public List<Account> GetAll()
      {
         using (var db = new PDatabaseContext())
         {
            return db.Accounts.ToList();
         }
      }

      public Account GetById(int id)
      {
         using (var db = new PDatabaseContext())
         {
            return db.Accounts.Where(x => x.Id == id).FirstOrDefault() ?? new Account();
         }
      }

      public void Insert(Account model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Accounts.Add(model);
            db.SaveChanges();
         }
      }

      public void Remove(Account model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Accounts.Remove(model);
            db.SaveChanges();
         }
      }

      public void Update(Account model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Accounts.Update(model);
            db.SaveChanges();
         }
      }
   }
}