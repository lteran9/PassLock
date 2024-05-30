using System;

namespace PassLock.Commands
{
   public static class CommandDispatch
   {
      public static bool Execute(AccountListCommand command)
      {
         return new AccountListHandler().Execute(command);
      }
   }
}