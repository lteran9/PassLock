using System;

namespace PassLock.Commands
{
   /// <summary>
   /// Generic class to execute commands.
   /// </summary>
   /// <typeparam name="TCommand"></typeparam>
   public static class CommandDispatch<TCommand> where TCommand : ICommand<bool>
   {
      public static bool Execute<THandler>(TCommand command) where THandler : BaseCommandHandler<TCommand, bool>, new()
      {
         return new THandler().Execute(command);
      }
   }
}