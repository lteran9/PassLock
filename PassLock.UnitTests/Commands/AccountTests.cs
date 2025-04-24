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
      public async Task AccountAdd_Test01(
         [Frozen] Mock<IDatabaseModel<Account>> repo)
      {
         var accountAddCmd = new AccountAddCommand(repo.Object, "test@test.com", "testUser");

         Assert.True(await accountAddCmd.Execute());
      }

      [Theory]
      [AutoMoq]
      public async Task AccountList_Test01(
         [Frozen] Mock<IDatabaseModel<Account>> repo)
      {
         var accountListCmd = new AccountListCommand(repo.Object);

         Assert.True(await accountListCmd.Execute());
      }

      [Theory]
      [AutoMoq]
      public async Task AccountRemove_Test01(
         [Frozen] Mock<IDatabaseModel<Account>> repo)
      {
         var accountRemoveCmd = new AccountRemoveCommand(repo.Object);

         // If no account descriptors passed in command should return false
         Assert.False(await accountRemoveCmd.Execute());
      }

      [Theory]
      [AutoMoq]
      public async Task AccountRemove_Test02(
         [Frozen] Mock<IDatabaseModel<Account>> repo)
      {
         // Arrange
         repo.Setup(x => x.GetByIdAsync(It.IsAny<Account>())).Returns(Task.FromResult(new Account()));

         // Act
         var accountRemoveCmd = new AccountRemoveCommand(repo.Object, 1);

         // Assert
         Assert.True(await accountRemoveCmd.Execute());
      }
   }
}