using System;
using System.Security;
using PassLock.Manager.Utils;
using PassLock.Manager.DataModels;

namespace Manager
{
   class Program
   {
      static EncryptionManager encryptionManager = new EncryptionManager();

      // Run the program
      static void Main(string[] args)
      {
         // If no arguments provided
         if (args.Length <= 0)
         {
            DisplayUsageInformation();
         }
         else
         {
            string argument = args[0].ToLower();

            switch (argument)
            {
               case "add":
                  Console.WriteLine("Please type in your password.");
                  var securePassword = GetPassword();
                  Console.WriteLine(string.Empty);

                  encryptionManager.Add(args[1], args[2], Hash.SHA256, securePassword.ToString());
                  encryptionManager.Save();

                  break;
               case "delete":
               case "update":
               case "list":
                  break;
               default:
                  DisplayUsageInformation();
                  break;
            }
         }
      }

      static void DisplayUsageInformation()
      {
         Console.WriteLine("");
         Console.WriteLine("Usage: passlock [options]");
         Console.WriteLine("Usage: passlock [command]");
         Console.WriteLine("");
         Console.WriteLine("Options:");
         Console.WriteLine("-h|--help\tDisplay help.");
         Console.WriteLine("--add <title> <salt>\t\t");
         Console.WriteLine("--info\t\tDisplay PassLock information help.");
         Console.WriteLine("--version\tDisplay PassLock version number.");
      }

      static SecureString GetPassword()
      {
         var pwd = new SecureString();
         while (true)
         {
            ConsoleKeyInfo i = Console.ReadKey(true);
            if (i.Key == ConsoleKey.Enter)
            {
               break;
            }
            else if (i.Key == ConsoleKey.Backspace)
            {
               if (pwd.Length > 0)
               {
                  pwd.RemoveAt(pwd.Length - 1);
                  Console.Write("\b \b");
               }
            }
            else if (i.KeyChar != '\u0000') // KeyChar == '\u0000' if the key pressed does not correspond to a printable character, e.g. F1, Pause-Break, etc
            {
               pwd.AppendChar(i.KeyChar);
               Console.Write("*");
            }
         }
         return pwd;
      }
   }
}
