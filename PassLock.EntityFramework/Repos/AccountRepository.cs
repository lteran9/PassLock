using System;
using Microsoft.EntityFrameworkCore;
using PassLock.Core;

namespace PassLock.EntityFramework
{
   public class AccountRepository : IDatabaseModel<Account>
   {
      public async Task<List<Account>> GetAllAsync()
      {
         using (var db = new PDatabaseContext())
         {
            return await db.Accounts.ToListAsync();
         }
      }

      public async Task<Account> GetByIdAsync(Account model)
      {
         using (var db = new PDatabaseContext())
         {
            return await db.Accounts.Where(x => x.Id == model.Id).FirstOrDefaultAsync() ?? new Account();
         }
      }

      public async Task InsertAsync(Account model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Accounts.Add(model);
            await db.SaveChangesAsync();
         }
      }

      public async Task RemoveAsync(Account model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Accounts.Remove(model);
            await db.SaveChangesAsync();
         }
      }

      public async Task UpdateAsync(Account model)
      {
         using (var db = new PDatabaseContext())
         {
            db.Accounts.Update(model);
            await db.SaveChangesAsync();
         }
      }
   }
}