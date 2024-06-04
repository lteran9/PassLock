using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class DomainRemoveCommand : ICommand<bool>
   {
      public int Id { get; private set; }

      private readonly IDatabaseModel<Domain> _domainDatabase;

      public DomainRemoveCommand(IDatabaseModel<Domain> repo, int id = 0)
      {
         _domainDatabase = repo;
         Id = id;
      }

      public bool Execute()
      {
         try
         {
            Domain? account = null;

            if (Id > 0)
            {
               account = _domainDatabase.GetById(Id);
            }

            if (account != null)
            {
               _domainDatabase.Remove(account);
            }

            return true;
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return false;
      }
   }
}