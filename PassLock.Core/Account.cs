using System;

namespace PassLock.Core
{
   public class Account
   {
      public int Id { get; set; }

      public string? Email { get; set; }
      public string? UserName { get; set; }

      public DateTime CreatedAt { get; set; }
      public DateTime UpdatedAt { get; set; }

      public Account()
      {
         CreatedAt = DateTime.Now;
         UpdatedAt = DateTime.Now;
      }
   }
}