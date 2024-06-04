using System;
using PassLock.Core;

namespace PassLock.Commands
{
   public class EncryptPasswordCommand : ICommand<Password?>
   {
      private readonly string _password;

      public EncryptPasswordCommand(string decrypted)
      {
         _password = decrypted;
      }

      public Password? Execute()
      {
         try
         {
            var encryptedPassword = Encryptor.Encrypt(_password, out string key, out string iv);

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