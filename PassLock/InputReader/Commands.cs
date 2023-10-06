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

      public static void List()
      {

      }

      public static void Add()
      {

      }

      public static void Remove()
      {

      }
   }
}