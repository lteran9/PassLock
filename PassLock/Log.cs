using System;

namespace PassLock
{
   public static class Log
   {
      static string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
      static string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";

      public static void Info(string message)
      {
         Console.Write($"{message}\n");
      }

      public static void Error(string message)
      {
         Console.Write($"{RED}Error:{NORMAL}\n\t-{message}\n");
      }

      public static void Error(Exception ex)
      {
         Error(ex.Message);
         if (!string.IsNullOrEmpty(ex.InnerException?.Message))
         {
            Console.Write($"\t-{ex.InnerException.Message}\n");
         }
      }
   }
}