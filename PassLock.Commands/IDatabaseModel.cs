using System;

namespace PassLock.Commands
{
   public interface IDatabaseModel<T> where T : class
   {
      public void Insert(T model);
      public void Update(T model);
      public void Remove(T model);
      public T GetById(T model);
      public List<T> GetAll();
   }
}