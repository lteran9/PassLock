using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PassLock.Config;
using PassLock.DataAccess.Entities;
using PassLock.InputReader;

namespace PassLock
{
   /// <summary>
   /// Entry point of the command line tool.
   /// </summary>
   public class Program
   {
      private static IConfiguration? _config;
      private static Library? _lib;

      static void Main(string[] args)
      {
         // Add appsettings.json file to Program
         var builder =
            new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true);

         try
         {
            // Create database context
            _lib = new Library();
            // Get configuration settings
            _config = builder.Build();

            ProcessInput(args);

            OsxClipboard.SetText("Hello, world!");
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

                  // Select account and domain
                  if (!string.IsNullOrEmpty(args[1]))
                  {
                     var encryptedPassword = Encryptor.Encrypt(args[1], out string key, out string iv);

                     // _db?.Passwords?.Add(
                     //    new Password()
                     //    {
                     //       Value = encryptedPassword,
                     //       Salt = key,
                     //       InitializationVector = iv,
                     //       CreatedAt = DateTime.Now,
                     //       UpdatedAt = DateTime.Now
                     //    }
                     // );
                     // _db?.SaveChanges();
                  }
                  break;
               case "decrypt": // Run decrypt operation
                  if (!string.IsNullOrEmpty(args[1]) && !string.IsNullOrEmpty(args[2]) && !string.IsNullOrEmpty(args[3]))
                  {
                     Console.WriteLine(Encryptor.Decrypt(args[1], args[2], args[3]));
                  }
                  break;
               case "domain": //  add, list, remove
                  switch (args[1])
                  {
                     case "add":
                        var domain = new Domain();
                        Console.Write("Enter domain: ");
                        domain.Url = Console.ReadLine() ?? string.Empty;
                        if (string.IsNullOrEmpty(domain.Url))
                        {
                           throw new InvalidOperationException("Please enter a valid domain.");
                        }

                        _lib?.AddDomain(domain);

                        break;
                     case "list":
                        if (_lib != null)
                        {
                           var domains = _lib.GetDomains() ?? new List<Domain>();
                           if (domains?.Count() > 0)
                           {
                              foreach (var dom in domains)
                              {
                                 Console.Write($"{dom.Id}. {dom.Url}\n");
                              }
                           }
                           else
                           {
                              Console.WriteLine("No domains found in database.");
                           }
                        }
                        break;
                     case "remove":
                        Console.Write("Please enter the domain id: ");
                        var input = Console.ReadLine();
                        int.TryParse(input, out int domainId);

                        _lib?.RemoveDomain(domainId);

                        break;
                     default:
                        break;
                  }
                  break;
               case "account": // add, list, remove
                  switch (args[1])
                  {
                     case "add":
                        var account = new Account();

                        Console.Write("Enter email: ");
                        account.Email = Console.ReadLine();
                        if (string.IsNullOrEmpty(account.Email))
                        {
                           Console.Write("Enter username: ");
                           account.UserName = Console.ReadLine();
                        }

                        _lib?.AddAccount(account);

                        break;
                     case "list":
                        if (_lib != null)
                        {
                           var accounts = _lib.GetAccounts() ?? new List<Account>();
                           if (accounts?.Count() > 0)
                           {
                              foreach (var acct in accounts)
                              {
                                 Console.Write($"{acct.Id}. {acct.Email}\n");
                              }
                           }
                           else
                           {
                              Console.WriteLine("No accounts found in database.");
                           }
                        }
                        break;
                     case "remove":
                        break;
                     default:
                        break;
                  }
                  break;
               default:
                  Log.Error("Unable to identify operation.");
                  break;
            }
         }
      }
   }
}