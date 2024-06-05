using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class PasswordGetCommand : ICommand<Password?>
   {
      private readonly int _id;
      private readonly IDatabaseModel<Password> _dbPassword;

      public PasswordGetCommand(IDatabaseModel<Password> repo, int id)
      {
         _id = id;
         _dbPassword = repo;
      }

      public Password? Execute()
      {
         try
         {
            return _dbPassword.GetById(new Password() { Id = _id });
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
         return null;
      }
   }
}