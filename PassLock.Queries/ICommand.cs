using System;

namespace PassLock.Queries
{
   /// <summary>
   /// Generic class to execute commands.
   /// </summary>
   /// <typeparam name="TResponse"></typeparam> 
   public static partial class CommandDispatch
   {
      public static TResponse Execute<TResponse>(IQuery<TResponse> command)
      {
         return command.Execute();
      }
   }
}