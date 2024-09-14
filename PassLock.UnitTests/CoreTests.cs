using System;
using PassLock.Core;

namespace PassLock.UnitTests
{
   public class CoreTests
   {
      private readonly string userName = "user";
      private readonly string email = "test@test.com";
      private readonly string domainUrl = "hotmail.com";

      [Fact]
      public void Account_Test01()
      {
         var acct = new Account();

         Assert.Equal(0, acct.Id);
         // Validate nullable column for Entity Framework
         Assert.Null(acct.Email);
         Assert.Null(acct.UserName);

         acct.Email = email;
         acct.UserName = userName;

         // Validate Account UserName and Email
         Assert.Equal(userName, acct.UserName);
         Assert.Equal(email, acct.Email);
         // Account CreatedAt and UpdatedAt should always be set to DateTime.Now by the constructor
         Assert.NotEqual(DateTime.MinValue, acct.CreatedAt);
         Assert.NotEqual(DateTime.MinValue, acct.UpdatedAt);
      }

      [Fact]
      public void Domain_Test01()
      {
         var domain = new Domain();

         Assert.Equal(0, domain.Id);
         // Validate column not nullable for Entity Framework
         Assert.NotNull(domain.Url);

         domain.Url = domainUrl;

         // Validate Domain URL reference
         Assert.Equal(domainUrl, domain.Url);
         // Domain CreatedAt and UpdatedAt should always be set to DateTime.Now by the constructor
         Assert.NotEqual(DateTime.MinValue, domain.CreatedAt);
         Assert.NotEqual(DateTime.MinValue, domain.UpdatedAt);
      }

      [Fact]
      public void Password_Test01()
      {
         var password = new Password();

         Assert.Equal(0, password.Id);
         // Validate columns not null for Entity Framework
         Assert.NotNull(password.Key);
         Assert.NotNull(password.Value);
         Assert.NotNull(password.InitializationVector);

         // Password CreatedAt and UpdatedAt should always be set to DateTime.Now by the constructor
         Assert.NotEqual(DateTime.MinValue, password.CreatedAt);
         Assert.NotEqual(DateTime.MinValue, password.UpdatedAt);
      }
   }
}