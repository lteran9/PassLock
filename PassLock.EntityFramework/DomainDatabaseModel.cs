using System;
using PassLock.Commands;
using PassLock.Core;

namespace PassLock.EntityFramework
{
   public class DomainDatabaseModel : IDatabaseModel<Domain>
   {
      public List<Domain> GetAll()
      {
         throw new NotImplementedException();
      }

      public Domain GetById(int id)
      {
         throw new NotImplementedException();
      }

      public void Insert(Domain model)
      {
         throw new NotImplementedException();
      }

      public void Remove(Domain model)
      {
         throw new NotImplementedException();
      }

      public void Update(Domain model)
      {
         throw new NotImplementedException();
      }
   }
}