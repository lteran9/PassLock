using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class DomainListCommand : ICommand<Task<bool>>
   {
      private readonly IDatabaseModel<Domain> _domainDatabase;

      public DomainListCommand(IDatabaseModel<Domain> repo)
      {
         _domainDatabase = repo;
      }

      public async Task<bool> Execute()
      {
         try
         {
            var domains = await _domainDatabase.GetAllAsync();

            if (domains?.Any() == true)
            {
               Console.WriteLine("Domains:");
               foreach (var dom in domains)
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