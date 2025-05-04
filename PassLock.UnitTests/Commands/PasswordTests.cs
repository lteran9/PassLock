using System;
using AutoFixture.Xunit2;
using Moq;
using PassLock.Commands;
using PassLock.Core;
using PassLock.EntityFramework;
using PassLock.Queries;

namespace PassLock.UnitTests.Commands
{
   public class PasswordTests
   {
      [Theory]
      [AutoMoq]
      public async Task GivenValidPassword_WhenAdded_ResponseIsTrue(
         [Frozen] Mock<IDatabaseModel<Password>> repo)
      {
         // Arrange
         var passwordAddCmd = new PasswordAddCommand(repo.Object, new Password() { Key = "abcd1234", Value = "A Random Password_" });
         // Act
         var response = await passwordAddCmd.Execute();
         // Assert
         Assert.True(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenInvalidPassword_WhenAdded_ResponseIsFalse(
         [Frozen] Mock<IDatabaseModel<Password>> repo)
      {
         // Arrange
         var passwordAddCmd = new PasswordAddCommand(repo.Object, new Password());
         // Act
         var response = await passwordAddCmd.Execute();
         // Assert
         Assert.False(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenPassword_WhenRetrieved_ResponseIsNotNull(
         [Frozen] Mock<IDatabaseModel<Password>> repo)
      {
         // Arrange
         repo.Setup(x => x.GetByIdAsync(It.IsAny<Password>())).ReturnsAsync(new Password());
         var passwordGetCmd = new PasswordGetCommand(repo.Object, 1);
         // Act
         var response = await passwordGetCmd.Execute();
         // Assert
         Assert.NotNull(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenPassword_WhenRetrievedWithBadId_ResponseIsNull(
         [Frozen] Mock<IDatabaseModel<Password>> repo)
      {
         // Arrange
         var passwordGetCmd = new PasswordGetCommand(repo.Object, 0);
         // Act
         var response = await passwordGetCmd.Execute();
         // Assert
         Assert.Null(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenAccountPasswordForDomain_WhenAdded_ResponseIsTrue(
         [Frozen] Mock<IDatabaseModel<AccountPasswordForDomain>> repo)
      {
         // Arrange
         var accountPasswordForDomainAddCmd = new AccountPasswordForDomainAddCommand(repo.Object, new AccountPasswordForDomain() { AccountId = 1, DomainId = 1 });
         // Act
         var response = await accountPasswordForDomainAddCmd.Execute();
         // Assert
         Assert.True(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenAccountPasswordForDomain_WhenRetrieved_ResponseIsNotNull(
         [Frozen] Mock<IDatabaseModel<AccountPasswordForDomain>> repo)
      {
         // Arrange
         repo.Setup(x => x.GetByIdAsync(It.IsAny<AccountPasswordForDomain>())).ReturnsAsync(new AccountPasswordForDomain());
         var accountPasswordForDomainGetCmd = new AccountPasswordForDomainGetCommand(repo.Object, 1, 1);
         // Act
         var response = await accountPasswordForDomainGetCmd.Execute();
         // Assert
         Assert.NotNull(response);
      }
   }
}