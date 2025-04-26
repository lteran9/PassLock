using System;
using PassLock.Commands;
using PassLock.Core;

namespace PassLock.UnitTests.Commands
{
   public class CryptographyTests
   {
      [Fact]
      public void GivenPassword_WhenEmpty_ThrowsException()
      {
         var password = string.Empty;
         var encryptPassword = new EncryptPasswordCommand(password);

         Assert.Throws<ArgumentNullException>(() => encryptPassword.Execute());

         var emptyEncryptedPassword = new Password();
         var decryptPassword = new DecryptPassword(emptyEncryptedPassword);

         Assert.Throws<ArgumentNullException>(() => decryptPassword.Execute());
      }

      [Fact]
      public void GivenPassword_WhenSimple_NotEmptyOrNull()
      {
         // Arrange
         var password = "abcd1234";
         var encryptCommand = new EncryptPasswordCommand(password);
         // Act
         var encrypted = encryptCommand.Execute();
         // Assert
         Assert.NotNull(encrypted);
         Assert.True(!string.IsNullOrEmpty(encrypted?.Value));
         Assert.True(!string.IsNullOrEmpty(encrypted?.Key));
         Assert.True(!string.IsNullOrEmpty(encrypted?.InitializationVector));
         Assert.True(password != encrypted?.Value);
      }

      [Fact]
      public void GivenSamePassword_WhenRepeatedlyEncrypted_ResultsInDifferentHash()
      {
         // Arrange
         var password = "zywx9874";
         var encryptCommand = new EncryptPasswordCommand(password);

         // Act
         var encryptedRoundOne = encryptCommand.Execute();
         var encryptedRoundTwo = encryptCommand.Execute();
         var encryptedRoundThree = encryptCommand.Execute();

         // Assert
         Assert.True(encryptedRoundOne != null);
         Assert.True(encryptedRoundTwo != null);
         Assert.True(encryptedRoundThree != null);
         // Validate we get a different key each time we encrypt the same string
         Assert.NotEqual(encryptedRoundOne?.Value, encryptedRoundTwo?.Value);
         Assert.NotEqual(encryptedRoundTwo?.Value, encryptedRoundThree?.Value);
      }

      [Fact]
      public void GivenPassword_WhenEncrypted_CanAlsoBeDecrypted()
      {
         // Arrange
         var password = "The quick brown fox jumps over the lazy dog";
         var encryptCommand = new EncryptPasswordCommand(password);
         // Act
         var encrypted = encryptCommand.Execute();
         // Assert
         Assert.NotNull(encrypted);
         Assert.NotEqual(password, encrypted.Value);

         // Act
         var decryptCommand = new DecryptPassword(encrypted);
         // Assert
         Assert.Equal(password, decryptCommand.Execute());
      }

      [Fact]
      public void GivenComplexPassword_WhenEncrypted_IsNotEmptyOrNull()
      {
         // Arrange
         int length = 8192;
         // Test string of indeterminate length (^ controlled by above)
         char[] password = new char[length];
         for (int i = 0; i < length; i++)
         {
            password[i] = (char)((i % 93) + 32);
         }
         string result = new string(password, 0, password.Length);

         Assert.NotEmpty(result);

         var encryptCommand = new EncryptPasswordCommand(result);
         // Act
         var encrypted = encryptCommand.Execute();
         // Assert
         Assert.NotNull(encrypted);
         Assert.NotEqual(result, encrypted.Value);
      }
   }
}