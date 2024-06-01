using System;
using PassLock.Commands;
using PassLock.Core;

namespace PassLock.EntityFramework
{
   public class DomainDatabaseModel : IDatabaseModel<Domain>
   {
      public List<Domain> GetAll()
      {
         using (var db = new PDatabaseContext())
         {
            return db.Domains.ToList();
         }
      }

      public Domain GetById(int id)
      {
         using (var db = new PDatabaseContext())
         {
            return db.Domains.Where(x => x.Id == id).FirstOrDefault() ?? new Domain();
         }
      }

      public void Insert(Domain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Domains.Add(model);
            db.SaveChanges();
         }
      }

      public void Remove(Domain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Domains.Remove(model);
            db.SaveChanges();
         }
      }

      public void Update(Domain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Domains.Update(model);
            db.SaveChanges();
         }
      }
   }
}