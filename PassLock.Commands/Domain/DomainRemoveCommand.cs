using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class DomainRemoveCommand : ICommand<bool>
   {
      private readonly int _id;

      private readonly IDatabaseModel<Domain> _domainDatabase;

      public DomainRemoveCommand(IDatabaseModel<Domain> repo, int id = 0)
      {
         _domainDatabase = repo;
         _id = id;
      }

      public bool Execute()
      {
         try
         {
            Domain? domain = null;

            if (_id > 0)
            {
               domain = _domainDatabase.GetById(new Domain() { Id = _id });
            }

            if (domain != null)
            {
               _domainDatabase.Remove(domain);

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