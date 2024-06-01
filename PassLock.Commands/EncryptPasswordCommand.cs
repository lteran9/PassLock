using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class EncryptPasswordCommand : ICommand<Password?>
   {
      private readonly string Password;

      public EncryptPasswordCommand(string decrypted)
      {
         Password = decrypted;
      }

      public Password? Execute()
      {
         try
         {
            var encryptedPassword = Encryptor.Encrypt(Password, out string key, out string iv);

            return
               new Password()
               {
                  Value = encryptedPassword,
                  Key = key,
                  InitializationVector = iv
               };
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return null;
      }
   }
}