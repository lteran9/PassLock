using System;
using Microsoft.EntityFrameworkCore;
using PassLock.Core;

namespace PassLock.EntityFramework
{
   public class PasswordRepository : IDatabaseModel<Password>
   {
      public async Task<List<Password>> GetAllAsync()
      {
         using (var db = new PDatabaseContext())
         {
            return await db.Passwords.ToListAsync();
         }
      }

      public async Task<Password> GetByIdAsync(Password model)
      {
         using (var db = new PDatabaseContext())
         {
            return await db.Passwords.Where(x => x.Id == model.Id).FirstOrDefaultAsync() ?? new Password();
         }
      }

      public async Task InsertAsync(Password model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Passwords.Add(model);
            await db.SaveChangesAsync();
         }
      }

      public async Task RemoveAsync(Password model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Passwords.Remove(model);
            await db.SaveChangesAsync();
         }
      }

      public async Task UpdateAsync(Password model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Passwords.Update(model);
            await db.SaveChangesAsync();
         }
      }
   }
}