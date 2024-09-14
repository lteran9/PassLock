using System;
using PassLock.Core;

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
            await _accountPasswordForDomainDatabase.InsertAsync(_model);

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