using System;
using PassLock.EntityFramework;
using PassLock.Commands;

namespace PassLock
{
   /// <summary>
   /// Entry point of the command line tool.
   /// </summary>
   public class Program
   {
      /// <summary>
      /// CRUD operations for the <c>Account</c> model.
      /// </summary>
      /// <returns></returns>
      private static readonly AccountDatabaseModel dbAccount = new AccountDatabaseModel();
      /// <summary>
      /// CRUD operations for the <c>Domain</c> model.
      /// </summary>
      /// <returns></returns>
      private static readonly DomainDatabaseModel dbDomain = new DomainDatabaseModel();

      static void Main(string[] args)
      {
         try
         {
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
            InputReader.Commands.Help();
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
                        // var encryptedPassword = Encryptor.Encrypt(password, out string key, out string iv);
                        // var pwdId = _lib?.AddPassword(
                        //    new Password()
                        //    {
                        //       Key = key,
                        //       Value = encryptedPassword,
                        //       InitializationVector = iv
                        //    }
                        // ) ?? 0;

                        // if (pwdId > 0)
                        // {
                        //    _lib?.AddAccountPassword(accountId, domainId, pwdId);
                        // }
                        // else
                        // {
                        //    throw new Exception("Unable to save password to database.");
                        // }
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
                        // var pwd = _lib?.GetPassword(accountId, domainId);
                        // if (!string.IsNullOrEmpty(pwd?.Value))
                        // {
                        //    OsxClipboard.SetText(Encryptor.Decrypt(pwd.Value, pwd.Key, pwd.InitializationVector));
                        //    Log.Info("Value copied to clipboard.");
                        // }
                        // else
                        // {
                        //    Log.Error("Password value is empty.");
                        // }
                     }
                  }
                  break;
               case "domain": //  add, list, remove operations
                  switch (args[1])
                  {
                     case "add":
                        CommandDispatch.Execute(new DomainAddCommand(dbDomain));
                        break;
                     case "list":
                        CommandDispatch.Execute(new DomainListCommand(dbDomain));
                        break;
                     case "remove":
                        if (args.Length >= 3)
                        {
                           if (int.TryParse(args[2], out int id))
                           {
                              CommandDispatch.Execute(new DomainRemoveCommand(dbDomain, id: id));
                           }
                        }
                        else
                        {
                           Log.Error("Please enter an domain identifier.");
                        }
                        break;
                     default:
                        Log.Error("Unable to determine `domain` operation.");
                        break;
                  }
                  break;
               case "account": // add, list, remove operations
                  switch (args[1])
                  {
                     case "add":
                        CommandDispatch.Execute(new AccountAddCommand(dbAccount));
                        break;
                     case "list":
                        CommandDispatch.Execute(new AccountListCommand(dbAccount));
                        break;
                     case "remove":
                        if (args.Length >= 3)
                        {
                           if (int.TryParse(args[2], out int id))
                           {
                              // Remove by id
                              CommandDispatch.Execute(new AccountRemoveCommand(dbAccount, id: id));
                           }
                           else if (args[2]?.Contains("@") == true)
                           {
                              // Remove by email
                              CommandDispatch.Execute(new AccountRemoveCommand(dbAccount, email: args[2]));
                           }
                           else
                           {
                              // Remove by username
                              CommandDispatch.Execute(new AccountRemoveCommand(dbAccount, username: args[2]));
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