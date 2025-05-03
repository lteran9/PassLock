using System;
using AutoFixture.Xunit2;
using Moq;
using PassLock.Commands;
using PassLock.Core;
using PassLock.EntityFramework;
using PassLock.Queries;

namespace PassLock.UnitTests.Commands
{
   public class DomainTests
   {
      [Theory]
      [AutoMoq]
      public async Task GivenValidDomain_WhenAdded_ResponseIsTrue(
         [Frozen] Mock<IDatabaseModel<Domain>> repo)
      {
         // Arrange
         var domainAddCmd = new DomainAddCommand(repo.Object, "hotmail.com");
         // Act
         var response = await domainAddCmd.Execute();
         // Assert
         Assert.True(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenInvalidDomain_WhenAdded_ResponseIsFalse(
         [Frozen] Mock<IDatabaseModel<Domain>> repo)
      {
         // Arrange
         var domainAddCmd = new DomainAddCommand(repo.Object, string.Empty);
         // Act
         var response = await domainAddCmd.Execute();
         // Assert
         Assert.False(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenDomain_WhenListed_ResponseIsTrue(
         [Frozen] Mock<IDatabaseModel<Domain>> repo)
      {
         // Arrange
         repo.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Domain>() { });
         var domainListCmd = new DomainListCommand(repo.Object);
         // Act
         var response = await domainListCmd.Execute();
         // Assert
         Assert.True(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenDomainRemoveCommand_WhenMissingArguments_ResponseIsFalse(
         [Frozen] Mock<IDatabaseModel<Domain>> repo)
      {
         // Arrange
         var domainRemoveCmd = new DomainRemoveCommand(repo.Object);
         // Act
         var response = await domainRemoveCmd.Execute();
         // Assert
         Assert.False(response);
      }

      [Theory]
      [AutoMoq]
      public async Task GivenDomainRemoveCommand_WhenExecuted_ResponseIsTrue(
         [Frozen] Mock<IDatabaseModel<Domain>> repo)
      {
         // Arange
         repo.Setup(x => x.GetByIdAsync(It.IsAny<Domain>())).ReturnsAsync(new Domain());
         var domainRemoveCmd = new DomainRemoveCommand(repo.Object, 1);
         // Act
         var response = await domainRemoveCmd.Execute();
         // Assert
         Assert.True(response);
      }
   }
}