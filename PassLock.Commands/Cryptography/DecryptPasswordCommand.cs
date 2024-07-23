using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class DecryptPassword : ICommand<string>
   {
      private readonly Password _password;

      public DecryptPassword(Password encrypted)
      {
         _password = encrypted;
      }

      public string Execute()
      {
         if (string.IsNullOrEmpty(_password.Value))
         {
            throw new ArgumentNullException($"{nameof(Password.Value)} property was not provided.");
         }

         if (string.IsNullOrEmpty(_password.Key))
         {
            throw new ArgumentNullException($"{nameof(Password.Key)} property was not provided.");
         }

         if (string.IsNullOrEmpty(_password.InitializationVector))
         {
            throw new ArgumentNullException($"{nameof(Password.InitializationVector)} property was not provided.");
         }

         try
         {

            return Encryptor.Decrypt(_password.Value, _password.Key, _password.InitializationVector);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return string.Empty;
      }
   }
}