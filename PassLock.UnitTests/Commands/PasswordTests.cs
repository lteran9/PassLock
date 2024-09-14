using System;
using Moq;
using PassLock.Commands;
using PassLock.Core;

namespace PassLock.UnitTests.Commands
{
   public class PasswordTests
   {
      [Fact]
      public void PasswordAdd_Test01()
      {
         var repo = new Mock<IDatabaseModel<Password>>();
         var passwordAddCmd = new PasswordAddCommand(repo.Object, new Password());

         Assert.True(passwordAddCmd.Execute());
      }

      [Fact]
      public void PasswordGet_Test01()
      {
         var repo = new Mock<IDatabaseModel<Password>>();
         repo.Setup(x => x.GetById(It.IsAny<Password>())).Returns(new Password());
         var passwordGetCmd = new PasswordGetCommand(repo.Object, 1);
         // Validate we do not get a null value for password with Id
         Assert.NotNull(passwordGetCmd.Execute());
      }

      [Fact]
      public void PasswordGet_Test02()
      {
         var repo = new Mock<IDatabaseModel<Password>>();
         var passwordGetCmd = new PasswordGetCommand(repo.Object, 1);
         // Validate we get null when no password found for Id
         Assert.Null(passwordGetCmd.Execute());
      }

      [Fact]
      public void AccountPasswordForDomainAdd_Test01()
      {
         var repo = new Mock<IDatabaseModel<AccountPasswordForDomain>>();
         var accountPasswordForDomainAddCmd = new AccountPasswordForDomainAddCommand(repo.Object, new AccountPasswordForDomain());

         Assert.True(accountPasswordForDomainAddCmd.Execute());
      }

      [Fact]
      public void AccountPasswordForDomainGet_Test01()
      {
         var repo = new Mock<IDatabaseModel<AccountPasswordForDomain>>();
         repo.Setup(x => x.GetById(It.IsAny<AccountPasswordForDomain>())).Returns(new AccountPasswordForDomain());
         var accountPasswordForDomainGetCmd = new AccountPasswordForDomainGetCommand(repo.Object, 1, 1);

         Assert.NotNull(accountPasswordForDomainGetCmd.Execute());
      }
   }
}