using System;
using PassLock.Core;
using PassLock.EntityFramework;

namespace PassLock.Queries
{
   public class PasswordGetCommand : IQuery<Task<Password?>>
   {
      private readonly int _id;
      private readonly IDatabaseModel<Password> _dbPassword;

      public PasswordGetCommand(IDatabaseModel<Password> repo, int id)
      {
         _id = id;
         _dbPassword = repo;
      }

      public async Task<Password?> Execute()
      {
         try
         {
            if (_id > 0)
            {
               return await _dbPassword.GetByIdAsync(new Password() { Id = _id });
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return null;
      }
   }
}