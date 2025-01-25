using System;
using Moq;
using PassLock.Commands;
using PassLock.Core;
using PassLock.EntityFramework;
using PassLock.Queries;

namespace PassLock.UnitTests.Commands
{
   public class PasswordTests
   {
      [Fact]
      public async Task PasswordAdd_Test01()
      {
         var repo = new Mock<IDatabaseModel<Password>>();
         var passwordAddCmd = new PasswordAddCommand(repo.Object, new Password());

         Assert.True(await passwordAddCmd.Execute());
      }

      [Fact]
      public async Task PasswordGet_Test01()
      {
         var repo = new Mock<IDatabaseModel<Password>>();
         repo.Setup(x => x.GetByIdAsync(It.IsAny<Password>())).Returns(Task.FromResult(new Password()));
         var passwordGetCmd = new PasswordGetCommand(repo.Object, 1);
         // Validate we do not get a null value for password with Id
         Assert.NotNull(await passwordGetCmd.Execute());
      }

      [Fact]
      public async Task PasswordGet_Test02()
      {
         var repo = new Mock<IDatabaseModel<Password>>();
         var passwordGetCmd = new PasswordGetCommand(repo.Object, 1);
         // Validate we get null when no password found for Id
         Assert.Null(await passwordGetCmd.Execute());
      }

      [Fact]
      public async Task AccountPasswordForDomainAdd_Test01()
      {
         var repo = new Mock<IDatabaseModel<AccountPasswordForDomain>>();
         var accountPasswordForDomainAddCmd = new AccountPasswordForDomainAddCommand(repo.Object, new AccountPasswordForDomain());

         Assert.True(await accountPasswordForDomainAddCmd.Execute());
      }

      [Fact]
      public async Task AccountPasswordForDomainGet_Test01()
      {
         var repo = new Mock<IDatabaseModel<AccountPasswordForDomain>>();
         repo.Setup(x => x.GetByIdAsync(It.IsAny<AccountPasswordForDomain>())).Returns(Task.FromResult(new AccountPasswordForDomain()));
         var accountPasswordForDomainGetCmd = new AccountPasswordForDomainGetCommand(repo.Object, 1, 1);

         Assert.NotNull(await accountPasswordForDomainGetCmd.Execute());
      }
   }
}