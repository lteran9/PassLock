using System;
using PassLock.Core;
using PassLock.EntityFramework;

namespace PassLock.Commands
{
   public class AccountPasswordForDomainAddCommand : ICommand<Task<bool>>
   {
      private readonly IDatabaseModel<AccountPasswordForDomain> _accountPasswordForDomainDatabase;
      private readonly AccountPasswordForDomain _model;

      public AccountPasswordForDomainAddCommand(IDatabaseModel<AccountPasswordForDomain> repo, AccountPasswordForDomain model)
      {
         _accountPasswordForDomainDatabase = repo;
         _model = model;
      }

      public async Task<bool> Execute()
      {
         try
         {
            if (_model.AccountId > 0 && _model.DomainId > 0)
            {
               await _accountPasswordForDomainDatabase.InsertAsync(_model);

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