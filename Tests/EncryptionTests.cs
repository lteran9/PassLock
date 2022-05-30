using System;
using System.Text;
using PassLock.Manager.Utils;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
   public class EncryptionTests : EncryptionManager
   {
      [Fact]
      public void Test_KeyPadding()
      {
         // Key length 0
         var sampleKey1 = "";
         var paddedSampleKey1 = GenerateKey(sampleKey1);
         // Key length 16
         var sampleKey2 = "abcdefghijklmnop";
         var paddedSampleKey2 = GenerateKey(sampleKey2);
         // Key length 32
         var sampleKey3 = "abcdefghijklmnopqrstuvxyz0123456";
         var paddedSampleKey3 = GenerateKey(sampleKey3);

         Assert.True(GenerateKey().Length == 32);
         Assert.True(paddedSampleKey1.Length == 32);
         Assert.True(paddedSampleKey2.Length == 32);
         Assert.True(paddedSampleKey3.Length == 32);

         Assert.NotEqual(sampleKey1, paddedSampleKey1);
         Assert.NotEqual(sampleKey2, paddedSampleKey2);
         // Key length is already 32 characters so no need to modify string
         Assert.Equal(sampleKey3, paddedSampleKey3);
      }

      [Fact]
      public void Test_Encryption()
      {
         var password = "s6@nLL49";
         var key = Guid.NewGuid().ToString().Replace("-", "");
         var iv = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);

         var encryptedPasswordCipher = Encrypt(password, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

         Assert.True(encryptedPasswordCipher != Encoding.UTF8.GetBytes(password));
         Assert.True(encryptedPasswordCipher is byte[]);
      }

      [Fact]

      public void Test_Decryption()
      {
         var password = "s6@nLL49";
         var key = Guid.NewGuid().ToString().Replace("-", "");
         var iv = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);

         var encryptedPassword = Encrypt(password, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
         // Need to use the same key and iv to decrypt
         var decryptedPassword = Decrypt(encryptedPassword, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

         Assert.True(decryptedPassword == password);
         Assert.True(decryptedPassword is string);

         key = Guid.NewGuid().ToString().Replace("-", "");
         iv = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);

         // Using a different key and iv should not yield the correct password
         Assert.Throws<System.Security.Cryptography.CryptographicException>(() => Decrypt(encryptedPassword, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv)));
      }
   }
}
