using System;
using System.Security;
using System.Threading.Tasks;
using PassLock.Manager.Utils;
using PassLock.Manager.DataModels;

namespace Manager
{
   class Program
   {
      static EncryptionManager encryptionManager = new EncryptionManager();

      // Run the program
      static async Task Main(string[] args)
      {
         try
         {
            // Find passwords.json file and load all passwords into memory
            await encryptionManager.Load();

            switch (args[0].ToLower())
            {
               case "add":
                  Console.WriteLine("Please type in your password.");
                  var securePassword = GetPassword();
                  Console.WriteLine(string.Empty);

                  // Get the value of the secure string for encryption
                  string password = new System.Net.NetworkCredential(string.Empty, securePassword).Password;

                  encryptionManager.Add(args[1], args[2], Hash.SHA256, password);
                  encryptionManager.Save();

                  break;
               case "delete":
                  var key = args[1];

                  encryptionManager.Remove(key);
                  encryptionManager.Save();

                  break;
               case "list":
                  break;
               default:
                  DisplayUsageInformation();
                  break;
            }

            return;
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         DisplayUsageInformation();
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
         Console.WriteLine("--info\t\t\tDisplay PassLock information help.");
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
