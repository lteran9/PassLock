using System;
using Moq;
using PassLock.Commands;
using PassLock.Core;

namespace PassLock.UnitTests.Commands
{
   public class AccountTests
   {
      [Fact]
      public void AccountAdd_Test01()
      {
         var repo = new Mock<IDatabaseModel<Account>>();
         var accountAddCmd = new AccountAddCommand(repo.Object, "test@test.com", "testUser");

         Assert.True(accountAddCmd.Execute());
      }

      [Fact]
      public void AccountList_Test01()
      {
         var repo = new Mock<IDatabaseModel<Account>>();
         var accountListCmd = new AccountListCommand(repo.Object);

         Assert.True(accountListCmd.Execute());
      }

      [Fact]
      public void AccountRemove_Test01()
      {
         var repo = new Mock<IDatabaseModel<Account>>();
         var accountRemoveCmd = new AccountRemoveCommand(repo.Object);
         // If no account descriptors passed in command should return false
         Assert.False(accountRemoveCmd.Execute());
      }

      [Fact]
      public void AccountRemove_Test02()
      {
         var repo = new Mock<IDatabaseModel<Account>>();
         repo.Setup(x => x.GetById(It.IsAny<Account>())).Returns(new Account());
         var accountRemoveCmd = new AccountRemoveCommand(repo.Object, 1);
         // Command should return true because we are remove account id 1
         Assert.True(accountRemoveCmd.Execute());
      }
   }
}