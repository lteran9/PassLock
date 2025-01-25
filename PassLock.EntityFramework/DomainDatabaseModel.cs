using System;
using Microsoft.EntityFrameworkCore;
using PassLock.Core;

namespace PassLock.EntityFramework
{
   public class DomainDatabaseModel : IDatabaseModel<Domain>
   {
      public async Task<List<Domain>> GetAllAsync()
      {
         using (var db = new PDatabaseContext())
         {
            return await db.Domains.ToListAsync();
         }
      }

      public async Task<Domain> GetByIdAsync(Domain model)
      {
         using (var db = new PDatabaseContext())
         {
            return await db.Domains.Where(x => x.Id == model.Id).FirstOrDefaultAsync() ?? new Domain();
         }
      }

      public async Task InsertAsync(Domain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Domains.Add(model);
            await db.SaveChangesAsync();
         }
      }

      public async Task RemoveAsync(Domain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Domains.Remove(model);
            await db.SaveChangesAsync();
         }
      }

      public async Task UpdateAsync(Domain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Domains.Update(model);
            await db.SaveChangesAsync();
         }
      }
   }
}