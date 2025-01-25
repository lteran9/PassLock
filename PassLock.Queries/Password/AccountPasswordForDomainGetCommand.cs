using System;
using PassLock.Core;
using PassLock.EntityFramework;

namespace PassLock.Queries
{
   public class AccountPasswordForDomainGetCommand : IQuery<Task<AccountPasswordForDomain?>>
   {
      private readonly int _accountId;
      private readonly int _domainId;

      private readonly IDatabaseModel<AccountPasswordForDomain> _dbAccountPasswordForDomain;

      public AccountPasswordForDomainGetCommand(IDatabaseModel<AccountPasswordForDomain> repo, int accountId, int domainId)
      {
         _accountId = accountId;
         _domainId = domainId;
         _dbAccountPasswordForDomain = repo;
      }

      public async Task<AccountPasswordForDomain?> Execute()
      {
         try
         {
            return await _dbAccountPasswordForDomain.GetByIdAsync(
               new AccountPasswordForDomain()
               {
                  AccountId = _accountId,
                  DomainId = _domainId
               });
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return null;
      }
   }
}