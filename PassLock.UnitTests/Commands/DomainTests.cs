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
      public async Task DomainAdd_Test01(
         [Frozen] Mock<IDatabaseModel<Domain>> repo)
      {
         var domainAddCmd = new DomainAddCommand(repo.Object, "hotmail.com");

         Assert.True(await domainAddCmd.Execute());
      }

      [Theory]
      [AutoMoq]
      public async Task DomainList_Test01(
         [Frozen] Mock<IDatabaseModel<Domain>> repo)
      {
         // Arrange
         repo.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(new List<Domain>() { }));
         // Act
         var domainListCmd = new DomainListCommand(repo.Object);
         // Assert
         var response = await domainListCmd.Execute();
         Assert.True(response);
      }

      [Theory]
      [AutoMoq]
      public async Task DomainRemove_Test01(
         [Frozen] Mock<IDatabaseModel<Domain>> repo)
      {
         var domainRemoveCmd = new DomainRemoveCommand(repo.Object);
         // If no domain descriptors passed in command should return false
         Assert.False(await domainRemoveCmd.Execute());
      }

      [Theory]
      [AutoMoq]
      public async Task DomainRemove_Test02(
         [Frozen] Mock<IDatabaseModel<Domain>> repo)
      {
         // Arange
         repo.Setup(x => x.GetByIdAsync(It.IsAny<Domain>())).Returns(Task.FromResult(new Domain()));
         // Act
         var domainRemoveCmd = new DomainRemoveCommand(repo.Object, 1);
         // Assert
         var response = await domainRemoveCmd.Execute();
         Assert.True(response);
      }
   }
}