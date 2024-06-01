using System;

namespace PassLock.Commands
{
   /// <summary>
   /// Generic class to execute commands.
   /// </summary>
   /// <typeparam name="TCommand"></typeparam>
   public static class CommandDispatch
   {
      public static bool Execute<TCommand>(TCommand command) where TCommand : ICommand<bool>
      {
         return command.Execute();
      }
   }
}