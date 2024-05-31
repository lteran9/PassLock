using System;

namespace PassLock.Core
{
   public class AccountPasswordForDomain
   {
      public int AccountId { get; set; }
      public int DomainId { get; set; }
      public int PasswordId { get; set; }

      // public virtual Account Account { get; set; }
      // public virtual Domain Domain { get; set; }
      // public virtual Password Password { get; set; }

      public DateTime CreatedAt { get; set; }
      public DateTime UpdatedAt { get; set; }

      public AccountPasswordForDomain()
      {
         // Account = new Account();
         // Domain = new Domain();
         // Password = new Password();
         CreatedAt = DateTime.Now;
         UpdatedAt = DateTime.Now;
      }
   }
}