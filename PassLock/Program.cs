using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using PassLock.Config;
using PassLock.InputReader;

namespace PassLock
{
   /// <summary>
   /// Entry point of the command line tool.
   /// </summary>
   public class Program
   {
      static void Main(string[] args)
      {
         // Add appsettings.json file to Program
         var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
         // Get configuration settings
         var config = builder.Build();

         // Console.WriteLine(config["AppSettings:Version"]);

         try
         {
            ProcessInput(args);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }

      static void ProcessInput(string[] args)
      {
         if (args.Length == 0)
         {
            // Display --help information
            Commands.Help();
         }
         else
         {
            // Read command and handle operation
            switch (args[0])
            {
               case "encrypt": // Run encrypt operation
                  if (!string.IsNullOrEmpty(args[1]))
                  {
                     var textToEncrypt = args[1];

                     Console.WriteLine(Encryptor.Encrypt(args[1], "abc123"));
                  }

                  break;
               default:
                  break;
            }
         }
      }
   }
}