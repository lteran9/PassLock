using System;
using PassLock.Core;

namespace PassLock.UnitTests
{
   public class CoreTests
   {
      [Theory]
      [InlineData("", "")]
      [InlineData("randomUser", "random@test.com")]
      [InlineData("x23_Test", "jamal_test@email.com")]
      public void Account_SanityTest01(string userName, string email)
      {
         var acct = new Account();

         Assert.Equal(0, acct.Id);
         // Validate nullable column for Entity Framework
         Assert.Null(acct.UserName);
         Assert.Null(acct.Email);

         acct.Email = email;
         acct.UserName = userName;

         // Validate Account UserName and Email
         Assert.Equal(userName, acct.UserName);
         Assert.Equal(email, acct.Email);
         // Account CreatedAt and UpdatedAt should always be set to DateTime.Now by the constructor
         Assert.NotEqual(DateTime.MinValue, acct.CreatedAt);
         Assert.NotEqual(DateTime.MinValue, acct.UpdatedAt);
      }

      [Theory]
      [InlineData("")]
      [InlineData("hotmail.com")]
      public void Domain_SanityTest01(string domainUrl)
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
      public void Password_SanityTest01()
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