using System;
using Moq;
using AutoFixture.Xunit2;
using PassLock.Core;
using PassLock.Commands;
using PassLock.EntityFramework;
using PassLock.Queries;

namespace PassLock.UnitTests.Commands
{
   public class AccountTests
   {
      [Theory]
      [AutoMoq]
      public async Task GivenValidAccount_WhenAdded_ResponseIsTrue(
         [Frozen] Mock<IDatabaseModel<Account>> repo)
      {
         // Arrange
         var accountAddCmd = new AccountAddCommand(repo.Object, "test@test.com", "testUser");
         // Act
         var response = await accountAddCmd.Execute();
         // Assert
         Assert.True(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenInvalidAccount_WhenAdded_ResponseIsFalse(
         [Frozen] Mock<IDatabaseModel<Account>> repo)
      {
         // Arrange
         var accountAddCmd = new AccountAddCommand(repo.Object, string.Empty, string.Empty);
         // Act
         var response = await accountAddCmd.Execute();
         // Assert
         Assert.False(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenAccounts_WhenList_ResponseIsTrue(
         [Frozen] Mock<IDatabaseModel<Account>> repo)
      {
         // Arrange
         var accountListCmd = new AccountListCommand(repo.Object);
         // Act
         var response = await accountListCmd.Execute();
         // Assert
         Assert.True(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenAccountRemoveCommandWithMissingArguments_WhenExecuted_ResponseIsFalse(
         [Frozen] Mock<IDatabaseModel<Account>> repo)
      {
         // Arrange
         var accountRemoveCmd = new AccountRemoveCommand(repo.Object);
         // Act
         var response = await accountRemoveCmd.Execute();
         // Assert
         Assert.False(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenAccountRemoveCommand_WhenExecutedWithId_ResponseIsTrue(
         [Frozen] Mock<IDatabaseModel<Account>> repo)
      {
         // Arrange
         repo.Setup(x => x.GetByIdAsync(It.IsAny<Account>())).ReturnsAsync(new Account());
         var accountRemoveCmd = new AccountRemoveCommand(repo.Object, 1);
         // Act
         var response = await accountRemoveCmd.Execute();
         // Assert
         Assert.True(response);
      }

      [Theory]
      [AutoMoq]
      [InlineAutoData("test@test.com")]
      public async Task GivenAccountRemoveCommand_WhenExecutedWithEmail_ResponseIsTrue(
         string email,
         [Frozen] Mock<IDatabaseModel<Account>> repo)
      {
         // Arrange
         repo.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Account>() { new Account() { Email = email } });
         var accountRemoveCmd = new AccountRemoveCommand(repo.Object, email: email);
         // Act
         var response = await accountRemoveCmd.Execute();
         // Assert
         Assert.True(response);
      }

      [Theory]
      [AutoMoq]
      [InlineAutoData("sampleUser")]
      public async Task GivenAccountRemoveCommand_WhenExecutedWithUsername_ResponseIsTrue(
         string username,
         [Frozen] Mock<IDatabaseModel<Account>> repo)
      {
         // Arrange
         repo.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Account>() { new Account() { UserName = username } });
         var accountRemoveCmd = new AccountRemoveCommand(repo.Object, username: username);
         // Act
         var response = await accountRemoveCmd.Execute();
         // Assert
         Assert.True(response);
      }
   }
}