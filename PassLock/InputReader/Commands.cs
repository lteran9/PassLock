using System;
using System.Text;

namespace PassLock.InputReader
{
   /// <summary>
   /// This class will handle callbacks for various operations cli operations. 
   /// </summary>
   public static class Commands
   {
      /// <summary>
      /// -h, --help
      /// </summary>
      public static void Help()
      {
         var message = new StringBuilder();
         message.Append("Usage: passlock [operation] [arguments]...\n\n");
         message.Append("Manage your passwords directly from the command line.\n\n");
         message.Append("runtime-options:\n\n");

         message.Append("arguments:\n\n");

         Console.WriteLine(message);
      }

      public static string ReadPrivateString()
      {
         var result = new StringBuilder();
         while (true)
         {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
               case ConsoleKey.Enter:
                  return result.ToString();
               case ConsoleKey.Backspace:
                  if (result.Length == 0)
                  {
                     continue;
                  }

                  result.Length--;
                  Console.Write("\b \b");
                  continue;
               default:
                  result.Append(key.KeyChar);
                  Console.Write("*");
                  continue;
            }
         }
      }
   }
}