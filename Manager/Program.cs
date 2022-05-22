using System;

namespace Manager
{
   class Program
   {
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
         Console.WriteLine("--info\t\tDisplay PassLock information help.");
         Console.WriteLine("--version\tDisplay PassLock version number.");
      }
   }
}
