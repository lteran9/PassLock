using System;
using PassLock.Core;
using PassLock.EntityFramework;

namespace PassLock.Commands
{
   public class DomainRemoveCommand : ICommand<Task<bool>>
   {
      private readonly int _id;

      private readonly IDatabaseModel<Domain> _domainDatabase;

      public DomainRemoveCommand(IDatabaseModel<Domain> repo, int id = 0)
      {
         _domainDatabase = repo;
         _id = id;
      }

      public async Task<bool> Execute()
      {
         try
         {
            Domain? domain = null;

            if (_id > 0)
            {
               domain = await _domainDatabase.GetByIdAsync(new Domain() { Id = _id });
            }

            if (domain != null)
            {
               await _domainDatabase.RemoveAsync(domain);

               return true;
            }

         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return false;
      }
   }
}