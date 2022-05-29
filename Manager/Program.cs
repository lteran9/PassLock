using System;
using System.Text;
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
         bool displayUsageInformation = true;

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

                  encryptionManager.Add(args[1], args[2], password);
                  encryptionManager.Save();

                  break;
               case "delete":

                  encryptionManager.Remove(args[1]);
                  encryptionManager.Save();

                  break;
               case "list":
                  var sb = new StringBuilder();

                  if (encryptionManager.EncryptedPasswords.Count > 0)
                  {

                     foreach (var entry in encryptionManager.EncryptedPasswords)
                     {
                        sb.Append("\nKey:\t" + entry.Title);
                     }
                  }
                  else
                  {
                     sb.Append("No passwords to display.");
                  }

                  Console.WriteLine(sb.ToString());

                  break;
               case "decrypt":

                  var plainTextPassword = encryptionManager.Get(args[1]);
                  if (!string.IsNullOrEmpty(plainTextPassword))
                  {
                     Console.WriteLine(plainTextPassword);
                  }
                  else
                  {
                     Console.WriteLine("\nCould not find password in encrypted passwords file.");
                  }

                  break;
               default:
                  DisplayUsageInformation();
                  break;
            }

            displayUsageInformation = false;
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            displayUsageInformation = false;
         }

         if (displayUsageInformation)
         {
            DisplayUsageInformation();
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
         Console.WriteLine("--add <title> <key>\t\t");
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
