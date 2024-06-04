using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class AccountPasswordForDomainAddCommand : ICommand<bool>
   {
      private readonly IDatabaseModel<AccountPasswordForDomain> _accountPasswordForDomainDatabase;
      private readonly AccountPasswordForDomain _model;

      public AccountPasswordForDomainAddCommand(IDatabaseModel<AccountPasswordForDomain> repo, AccountPasswordForDomain model)
      {
         _accountPasswordForDomainDatabase = repo;
         _model = model;
      }

      public bool Execute()
      {
         try
         {
            _accountPasswordForDomainDatabase.Insert(_model);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return false;
      }
   }
}