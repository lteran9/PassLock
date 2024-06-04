using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class DomainListCommand : ICommand<bool>
   {
      private readonly IDatabaseModel<Domain> _domainDatabase;

      public DomainListCommand(IDatabaseModel<Domain> repo)
      {
         _domainDatabase = repo;
      }

      public bool Execute()
      {
         try
         {
            var Domains = _domainDatabase.GetAll();

            if (Domains?.Any() == true)
            {
               Console.WriteLine("Domains:");
               foreach (var dom in Domains)
               {
                  Console.WriteLine($"\t{dom.Id}. {dom.Url}");
               }
            }
            else
            {
               Console.WriteLine("No Domains found in database.");
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