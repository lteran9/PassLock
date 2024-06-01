using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class DomainAddCommand : ICommand<bool>
   {
      private readonly IDatabaseModel<Domain> DomainDatabase;

      public DomainAddCommand(IDatabaseModel<Domain> repo)
      {
         DomainDatabase = repo;
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
               DomainDatabase.Insert(new Domain() { Url = domain });

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