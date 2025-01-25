using System;
using Moq;
using PassLock.Core;
using PassLock.Commands;
using PassLock.EntityFramework;
using PassLock.Queries;

namespace PassLock.UnitTests.Commands
{
   public class AccountTests
   {
      [Fact]
      public async Task AccountAdd_Test01()
      {
         var repo = new Mock<IDatabaseModel<Account>>();
         var accountAddCmd = new AccountAddCommand(repo.Object, "test@test.com", "testUser");

         Assert.True(await accountAddCmd.Execute());
      }

      [Fact]
      public async Task AccountList_Test01()
      {
         var repo = new Mock<IDatabaseModel<Account>>();
         var accountListCmd = new AccountListCommand(repo.Object);

         Assert.True(await accountListCmd.Execute());
      }

      [Fact]
      public async Task AccountRemove_Test01()
      {
         var repo = new Mock<IDatabaseModel<Account>>();
         var accountRemoveCmd = new AccountRemoveCommand(repo.Object);
         // If no account descriptors passed in command should return false
         Assert.False(await accountRemoveCmd.Execute());
      }

      [Fact]
      public async Task AccountRemove_Test02()
      {
         var repo = new Mock<IDatabaseModel<Account>>();
         repo.Setup(x => x.GetByIdAsync(It.IsAny<Account>())).Returns(Task.FromResult(new Account()));
         var accountRemoveCmd = new AccountRemoveCommand(repo.Object, 1);
         // Command should return true because we are remove account id 1
         Assert.True(await accountRemoveCmd.Execute());
      }
   }
}