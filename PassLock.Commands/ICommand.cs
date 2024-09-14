using System;

namespace PassLock.Commands
{
   /// <summary>
   /// Base interface for all commands. 
   /// </summary>
   /// <typeparam name="TResponse">Covariant type parameter can only be used as return type.</typeparam>
   public interface ICommand<out TResponse>
   {
      TResponse Execute();
   }
}