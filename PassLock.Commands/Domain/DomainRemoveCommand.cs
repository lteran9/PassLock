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
            Domain? account = null;

            if (_id > 0)
            {
               account = _domainDatabase.GetById(new Domain() { Id = _id });
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