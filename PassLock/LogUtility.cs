using System;

namespace PassLock
{
   /// <summary>
   /// Utility class to format messages displayed to the command line.
   /// </summary>
   public static class LogUtility
   {
      private static string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
      private static string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";

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