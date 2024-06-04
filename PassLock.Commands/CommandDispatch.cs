using System;
using PassLock.Core;

namespace PassLock.Commands
{
   /// <summary>
   /// Generic class to execute commands.
   /// </summary>
   /// <typeparam name="TResponse"></typeparam> 
   public static class CommandDispatch
   {
      public static TResponse Execute<TResponse>(ICommand<TResponse> command)
      {
         return command.Execute();
      }
   }
}