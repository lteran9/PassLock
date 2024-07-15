using System;
using PassLock.EntityFramework;
using PassLock.Commands;
using PassLock.Core;
using PassLock.InputReader;

namespace PassLock
{
   /// <summary>
   /// Entry point of the command line tool.
   /// </summary>
   public class Program
   {
      #region CRUD

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
      /// <summary>
      /// CRUD operations for the <c>Password</c> model.
      /// </summary> 
      private static readonly PasswordDatabaseModel dbPassword = new PasswordDatabaseModel();
      /// <summary>
      /// CRUD operations for <c>AccountPasswordForDomain</c> model.
      /// </summary>
      /// <returns></returns>
      private static readonly AccountPasswordForDomainDatabaseModel dbAccountPasswordForDomain = new AccountPasswordForDomainDatabaseModel();

      #endregion

      private static void Main(string[] args)
      {
         try
         {
            ProcessInput(args);
         }
         catch (Exception ex)
         {
            LogUtility.Error(ex);
         }
      }

      private static void ProcessInput(string[] args)
      {
         if (args.Length == 0)
         {
            // Display --help information
            InputReaderCommands.Help();
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
                        var password = InputReaderCommands.ReadPassword.Execute();
                        var encryptedPassword = CommandDispatch.Execute(new EncryptPasswordCommand(password));
                        if (encryptedPassword != null)
                        {
                           CommandDispatch.Execute(new PasswordAddCommand(dbPassword, encryptedPassword));
                           if (encryptedPassword.Id > 0)
                           {
                              var dbModel =
                                 new AccountPasswordForDomain()
                                 {
                                    AccountId = accountId,
                                    DomainId = domainId,
                                    PasswordId = encryptedPassword.Id
                                 };
                              CommandDispatch.Execute(new AccountPasswordForDomainAddCommand(dbAccountPasswordForDomain, dbModel));
                           }
                           else
                           {
                              LogUtility.Error("Unable to save password to database.");
                           }
                        }
                     }
                     else
                     {
                        LogUtility.Error("Invalid account or domain id.");
                     }
                  }
                  else
                  {
                     LogUtility.Error("Missing arguments.");
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
                        var accountPassword = CommandDispatch.Execute(new AccountPasswordForDomainGetCommand(dbAccountPasswordForDomain, accountId, domainId));
                        var passwordId = accountPassword?.PasswordId ?? 0;
                        var password = CommandDispatch.Execute(new PasswordGetCommand(dbPassword, passwordId));
                        var decryptedPassword = CommandDispatch.Execute(new PasswordDecryptCommand(password));

                        if (!string.IsNullOrEmpty(decryptedPassword))
                        {
                           OsxClipboard.SetText(decryptedPassword);
                           LogUtility.Info("Password value copied to clipboard.");
                        }
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
                           LogUtility.Error("Please enter an domain identifier.");
                        }
                        break;
                     default:
                        LogUtility.Error("Unable to determine `domain` operation.");
                        break;
                  }
                  break;
               case "account": // add, list, remove operations
                  switch (args[1])
                  {
                     case "add":
                        string email = InputReaderCommands.ReadEmail.Execute();
                        string userName = InputReaderCommands.ReadUserName.Execute();

                        CommandDispatch.Execute(new AccountAddCommand(dbAccount, email, userName));
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
                           LogUtility.Error("Please enter an account identifier.");
                        }
                        break;
                     default:
                        LogUtility.Error("Unable to determine `account` operation.");
                        break;
                  }
                  break;
               default:
                  LogUtility.Error("Unable to identify operation.");
                  break;
            }
         }
      }
   }
}