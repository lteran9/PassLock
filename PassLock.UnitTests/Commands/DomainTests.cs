using System;
using Moq;
using PassLock.Commands;
using PassLock.Core;

namespace PassLock.UnitTests.Commands
{
   public class DomainTests
   {
      [Fact]
      public async Task DomainAdd_Test01()
      {
         var repo = new Mock<IDatabaseModel<Domain>>();
         var domainAddCmd = new DomainAddCommand(repo.Object, "hotmail.com");

         Assert.True(await domainAddCmd.Execute());
      }

      [Fact]
      public async Task DomainList_Test01()
      {
         var repo = new Mock<IDatabaseModel<Domain>>();
         repo.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(new List<Domain>() { }));
         var domainListCmd = new DomainListCommand(repo.Object);

         Assert.True(await domainListCmd.Execute());
      }

      [Fact]
      public async Task DomainRemove_Test01()
      {
         var repo = new Mock<IDatabaseModel<Domain>>();
         var domainRemoveCmd = new DomainRemoveCommand(repo.Object);
         // If no domain descriptors passed in command should return false
         Assert.False(await domainRemoveCmd.Execute());
      }

      [Fact]
      public async Task DomainRemove_Test02()
      {
         var repo = new Mock<IDatabaseModel<Domain>>();
         repo.Setup(x => x.GetByIdAsync(It.IsAny<Domain>())).Returns(Task.FromResult(new Domain()));
         var domainRemoveCmd = new DomainRemoveCommand(repo.Object, 1);
         // Command should return true because we are remove domain id 1
         Assert.True(await domainRemoveCmd.Execute());
      }
   }
}