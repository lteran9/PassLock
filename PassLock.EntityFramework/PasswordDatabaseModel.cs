using System;
using PassLock.Commands;
using PassLock.Core;

namespace PassLock.EntityFramework
{
   public class PasswordDatabaseModel : IDatabaseModel<Password>
   {
      public List<Password> GetAll()
      {
         using (var db = new PDatabaseContext())
         {
            return db.Passwords.ToList();
         }
      }

      public Password GetById(Password model)
      {
         using (var db = new PDatabaseContext())
         {
            return db.Passwords.Where(x => x.Id == model.Id).FirstOrDefault() ?? new Password();
         }
      }

      public void Insert(Password model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Passwords.Add(model);
            db.SaveChanges();
         }
      }

      public void Remove(Password model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Passwords.Remove(model);
            db.SaveChanges();
         }
      }

      public void Update(Password model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Passwords.Update(model);
            db.SaveChanges();
         }
      }
   }
}