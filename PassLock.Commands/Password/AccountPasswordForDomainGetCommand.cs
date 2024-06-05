using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class AccountPasswordForDomainGetCommand : ICommand<AccountPasswordForDomain?>
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

      public AccountPasswordForDomain? Execute()
      {
         try
         {
            return _dbAccountPasswordForDomain.GetById(new AccountPasswordForDomain() { AccountId = _accountId, DomainId = _domainId });
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return null;
      }
   }
}