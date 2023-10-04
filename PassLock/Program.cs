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
      private static IConfiguration? _config;

      static void Main(string[] args)
      {
         string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
         string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";

         // Add appsettings.json file to Program
         var builder =
            new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true);

         try
         {
            // Get configuration settings
            _config = builder.Build();

            ProcessInput(args);
         }
         catch (Exception ex)
         {
            Console.Write($"{RED}Error:{NORMAL}\n\t-{ex.Message}\n");
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
            // Can't really do anything without the configuration settings
            if (_config != null)
            {
               // Read command and handle operation
               switch (args[0])
               {
                  case "encrypt": // Run encrypt operation
                     if (!string.IsNullOrEmpty(args[1]))
                     {
                        Console.WriteLine(Encryptor.Encrypt(args[1], _config["AppSettings:EncryptionKey"] ?? string.Empty));
                     }

                     break;
                  case "decrypt":

                     break;
                  default:
                     break;
               }
            }
         }
      }
   }
}