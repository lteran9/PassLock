using System;

namespace PassLock.Commands
{
   public interface IDatabaseModel<T> where T : class
   {
      public Task InsertAsync(T model);
      public Task UpdateAsync(T model);
      public Task RemoveAsync(T model);
      public Task<T> GetByIdAsync(T model);
      public Task<List<T>> GetAllAsync();
   }
}