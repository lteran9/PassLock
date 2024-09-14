using System;
using PassLock.Core;
using PassLock.Commands;
using Microsoft.EntityFrameworkCore;

namespace PassLock.EntityFramework
{
   public class AccountPasswordForDomainDatabaseModel : IDatabaseModel<AccountPasswordForDomain>
   {
      public async Task<List<AccountPasswordForDomain>> GetAllAsync()
      {
         using (var db = new PDatabaseContext())
         {
            return await db.AccountDomainPasswords.ToListAsync();
         }
      }

      /// <summary>
      /// Can't use this method because <c>AccountPasswordForDomain</c> uses a composite key.
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public async Task<AccountPasswordForDomain> GetByIdAsync(AccountPasswordForDomain model)
      {
         using (var db = new PDatabaseContext())
         {
            return await db.AccountDomainPasswords
               .FirstOrDefaultAsync(x =>
                  x.AccountId == model.AccountId &&
                  x.DomainId == model.DomainId) ?? new AccountPasswordForDomain();
         }
      }


      public async Task InsertAsync(AccountPasswordForDomain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.AccountDomainPasswords.Add(model);
            await db.SaveChangesAsync();
         }
      }

      public async Task RemoveAsync(AccountPasswordForDomain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.AccountDomainPasswords.Remove(model);
            await db.SaveChangesAsync();
         }
      }

      public async Task UpdateAsync(AccountPasswordForDomain model)
      {
         using (var db = new PDatabaseContext())
         {
            db.AccountDomainPasswords.Update(model);
            await db.SaveChangesAsync();
         }
      }
   }
}