using System;
using PassLock.Core;
using PassLock.EntityFramework;

namespace PassLock.Commands
{
   public class DomainAddCommand : ICommand<Task<bool>>
   {
      private readonly string? _domain;
      private readonly IDatabaseModel<Domain> _domainDatabase;

      public DomainAddCommand(IDatabaseModel<Domain> repo, string? domain = null)
      {
         _domain = domain;
         _domainDatabase = repo;
      }

      public async Task<bool> Execute()
      {
         try
         {
            // Get input from user
            Console.Write("Please enter a domain: ");
            var domain = _domain ?? Console.ReadLine();
            if (!string.IsNullOrEmpty(domain))
            {
               // Insert to database
               await _domainDatabase.InsertAsync(new Domain() { Url = domain });

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