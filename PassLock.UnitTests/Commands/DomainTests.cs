using System;
using Moq;
using PassLock.Commands;
using PassLock.Core;

namespace PassLock.UnitTests.Commands
{
   public class DomainTests
   {
      [Fact]
      public void DomainAdd_Test01()
      {
         var repo = new Mock<IDatabaseModel<Domain>>();
         var domainAddCmd = new DomainAddCommand(repo.Object, "hotmail.com");

         Assert.True(domainAddCmd.Execute());
      }

      [Fact]
      public void DomainList_Test01()
      {
         var repo = new Mock<IDatabaseModel<Domain>>();
         repo.Setup(x => x.GetAll()).Returns(new List<Domain>() { });
         var domainListCmd = new DomainListCommand(repo.Object);

         Assert.True(domainListCmd.Execute());
      }

      [Fact]
      public void DomainRemove_Test01()
      {
         var repo = new Mock<IDatabaseModel<Domain>>();
         var domainRemoveCmd = new DomainRemoveCommand(repo.Object);
         // If no domain descriptors passed in command should return false
         Assert.False(domainRemoveCmd.Execute());
      }

      [Fact]
      public void DomainRemove_Test02()
      {
         var repo = new Mock<IDatabaseModel<Domain>>();
         repo.Setup(x => x.GetById(It.IsAny<Domain>())).Returns(new Domain());
         var domainRemoveCmd = new DomainRemoveCommand(repo.Object, 1);
         // Command should return true because we are remove domain id 1
         Assert.True(domainRemoveCmd.Execute());
      }
   }
}