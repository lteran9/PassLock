using System;

namespace PassLock.Commands
{
   /// <summary>
   /// Abstract class to handle command implementation.
   /// </summary>
   /// <typeparam name="TCommand"></typeparam>
   /// <typeparam name="TResponse"></typeparam>
   public abstract class BaseCommandHandler<TCommand, TResponse> : ICommand<TResponse> where TCommand : ICommand<TResponse>
   {
      public TResponse? Execute(TCommand command)
      {
         try
         {
            return ExecuteCommand(command);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return default(TResponse);
      }

      internal abstract TResponse ExecuteCommand(TCommand command);
   }
}