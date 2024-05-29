using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PassLock.Config;
using PassLock.DataAccess;
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
         }
         catch (Exception ex)
         {
            Log.Error(ex);
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
                  if (args.Length >= 3)
                  {
                     // Get account and domain
                     int.TryParse(args[1], out int accountId);
                     int.TryParse(args[2], out int domainId);

                     if (accountId > 0 && domainId > 0)
                     {
                        Console.Write("Enter password: ");
                        var password = Console.ReadLine() ?? string.Empty;
                        var encryptedPassword = Encryptor.Encrypt(password, out string key, out string iv);
                        var pwdId = _lib?.AddPassword(
                           new Password()
                           {
                              Key = key,
                              Value = encryptedPassword,
                              InitializationVector = iv
                           }
                        ) ?? 0;

                        if (pwdId > 0)
                        {
                           _lib?.AddAccountPassword(accountId, domainId, pwdId);
                        }
                        else
                        {
                           throw new Exception("Unable to save password to database.");
                        }
                     }
                     else
                     {
                        Log.Error("Invalid account or domain id.");
                     }
                  }
                  else
                  {
                     Log.Error("Missing arguments.");
                  }
                  break;
               case "decrypt": // Run decrypt operation
                  if (args.Length >= 3)
                  {
                     // Get account and domain
                     int.TryParse(args[1], out int accountId);
                     int.TryParse(args[2], out int domainId);

                     if (accountId > 0 && domainId > 0)
                     {
                        var pwd = _lib?.GetPassword(accountId, domainId);
                        if (!string.IsNullOrEmpty(pwd?.Value))
                        {
                           OsxClipboard.SetText(Encryptor.Decrypt(pwd.Value, pwd.Key, pwd.InitializationVector));
                           Log.Info("Value copied to clipboard.");
                        }
                        else
                        {
                           Log.Error("Password value is empty.");
                        }
                     }
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
                        Log.Error("Unable to determine `domain` operation.");
                        break;
                  }
                  break;
               case "account": // add, list, remove
                  // 
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
                        if (args.Length >= 3)
                        {
                           if (int.TryParse(args[2], out int id))
                           {
                              _lib?.RemoveAccount(id: id);
                           }
                           else if (args[2]?.Contains("@") == true)
                           {
                              _lib?.RemoveAccount(email: args[2]);
                           }
                           else
                           {
                              _lib?.RemoveAccount(username: args[2]);
                           }
                        }
                        else
                        {
                           Log.Error("Please enter an account identifier.");
                        }
                        break;
                     default:
                        Log.Error("Unable to determine `account` operation.");
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