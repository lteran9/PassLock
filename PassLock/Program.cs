using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using PassLock.Config;
using PassLock.InputReader;
using PassLock.DataAccess;

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
            Log.Error(ex.Message);
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
                     Console.WriteLine(Encryptor.Encrypt(args[1], out string key));

                     Log.Info(key);
                  }
                  break;
               case "decrypt": // Run decrypt operation

                  break;
               case "domain": //  add, list, remove

                  break;
               case "account": // add, list, remove

                  break;
               default:
                  break;
            }
         }
      }
   }
}