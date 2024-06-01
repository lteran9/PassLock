using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class DomainRemoveCommand : ICommand<bool>
   {
      public int Id { get; private set; }

      private readonly IDatabaseModel<Domain> DomainDatabase;

      public DomainRemoveCommand(IDatabaseModel<Domain> repo, int id = 0)
      {
         DomainDatabase = repo;
         Id = id;
      }

      public bool Execute()
      {
         try
         {
            Domain? account = null;

            if (Id > 0)
            {
               account = DomainDatabase.GetById(Id);
            }

            if (account != null)
            {
               DomainDatabase.Remove(account);
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