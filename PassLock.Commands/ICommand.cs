using System;

namespace PassLock.Commands
{
   /// <summary>
   /// Base interface for all commands.
   /// </summary>
   /// <typeparam name="TResponse"></typeparam>
   public interface ICommand<out TResponse>
   {
      TResponse Execute();
   }
}