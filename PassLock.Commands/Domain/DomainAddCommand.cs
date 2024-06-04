using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class DomainAddCommand : ICommand<bool>
   {
      private readonly IDatabaseModel<Domain> _domainDatabase;

      public DomainAddCommand(IDatabaseModel<Domain> repo)
      {
         _domainDatabase = repo;
      }

      public bool Execute()
      {
         try
         {
            // Get input from user
            Console.Write("Please enter a domain: ");
            var domain = Console.ReadLine();
            if (!string.IsNullOrEmpty(domain))
            {
               // Insert to database
               _domainDatabase.Insert(new Domain() { Url = domain });

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