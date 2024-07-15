using System;

namespace PassLock.Core
{
   public class AccountPasswordForDomain
   {
      public int AccountId { get; set; }
      public int DomainId { get; set; }
      public int PasswordId { get; set; }

      public DateTime CreatedAt { get; set; }
      public DateTime UpdatedAt { get; set; }

      public AccountPasswordForDomain()
      {
         CreatedAt = DateTime.Now;
         UpdatedAt = DateTime.Now;
      }
   }
}