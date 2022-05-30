using System;
using PassLock.Manager.Utils;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
   public class EncryptionTests
   {
      static EncryptionManager encryptionManager = new EncryptionManager();

      [Fact]
      public void Test_KeyPadding()
      {
         // Key length 0
         var sampleKey1 = "";
         var paddedSampleKey1 = encryptionManager.GenerateKey(sampleKey1);
         // Key length 16
         var sampleKey2 = "abcdefghijklmnop";
         var paddedSampleKey2 = encryptionManager.GenerateKey(sampleKey2);
         // Key length 32
         var sampleKey3 = "abcdefghijklmnopqrstuvxyz0123456";
         var paddedSampleKey3 = encryptionManager.GenerateKey(sampleKey3);

         Assert.True(encryptionManager.GenerateKey().Length == 32);
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

      }

      [Fact]
      public void Test_Decryption()
      {

      }
   }
}
