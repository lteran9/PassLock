using System;
using System.Text;
using PassLock.Commands;

namespace PassLock.InputReader
{
   /// <summary>
   /// This class will handle callbacks for various operations cli operations. 
   /// </summary>
   public static class InputReaderCommands
   {
      public static ICommand<string> ReadEmail
      {
         get
         {
            return new ReadInputCommand("Please enter email: ");
         }
      }
      public static ICommand<string> ReadUserName
      {
         get
         {
            return new ReadInputCommand("Please enter user name: ");
         }
      }
      public static ICommand<string> ReadPassword
      {
         get
         {
            return new ReadPrivateInputCommand("Enter password: ");
         }
      }

      /// <summary>
      /// -h, --help
      /// </summary>
      public static void Help()
      {
         var message = new StringBuilder();
         message.Append("Usage: passlock [operation] [arguments]...\n\n");
         message.Append("Manage your passwords directly from the command line.\n\n");
         message.Append("options:\n\n");
         message.Append("\t-h, --help\n\n");
         message.Append("operation:\n\n");
         message.Append("\tencrypt\n");
         message.Append("\tdecrypt\n");
         message.Append("\tdomain\n");
         message.Append("\taccount\n\n");

         Console.WriteLine(message);
      }
   }
}