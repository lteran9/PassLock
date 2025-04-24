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
      public async Task PasswordAdd_Test01(
         [Frozen] Mock<IDatabaseModel<Password>> repo)
      {
         var passwordAddCmd = new PasswordAddCommand(repo.Object, new Password());

         Assert.True(await passwordAddCmd.Execute());
      }

      [Theory]
      [AutoMoq]
      public async Task PasswordGet_Test01(
         [Frozen] Mock<IDatabaseModel<Password>> repo)
      {
         repo.Setup(x => x.GetByIdAsync(It.IsAny<Password>())).Returns(Task.FromResult(new Password()));
         var passwordGetCmd = new PasswordGetCommand(repo.Object, 1);
         // Validate we do not get a null value for password with Id
         Assert.NotNull(await passwordGetCmd.Execute());
      }

      [Theory]
      [AutoMoq]
      public async Task PasswordGet_Test02(
         [Frozen] Mock<IDatabaseModel<Password>> repo)
      {
         var passwordGetCmd = new PasswordGetCommand(repo.Object, 1);
         var response = await passwordGetCmd.Execute();
         // Returns an empty object
         Assert.NotNull(response);
         Assert.Equal(0, response.Id);
      }

      [Theory]
      [AutoMoq]
      public async Task AccountPasswordForDomainAdd_Test01(
         [Frozen] Mock<IDatabaseModel<AccountPasswordForDomain>> repo)
      {
         var accountPasswordForDomainAddCmd = new AccountPasswordForDomainAddCommand(repo.Object, new AccountPasswordForDomain());

         Assert.True(await accountPasswordForDomainAddCmd.Execute());
      }

      [Theory]
      [AutoMoq]
      public async Task AccountPasswordForDomainGet_Test01(
         [Frozen] Mock<IDatabaseModel<AccountPasswordForDomain>> repo)
      {
         // Arrange
         repo.Setup(x => x.GetByIdAsync(It.IsAny<AccountPasswordForDomain>())).ReturnsAsync(new AccountPasswordForDomain());
         // Act
         var accountPasswordForDomainGetCmd = new AccountPasswordForDomainGetCommand(repo.Object, 1, 1);
         // Assert
         Assert.NotNull(await accountPasswordForDomainGetCmd.Execute());
      }
   }
}