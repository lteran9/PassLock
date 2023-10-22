using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using PassLock.DataAccess.Entities;

namespace PassLock
{
   /// <summary>
   /// Keep track of which password belongs to which account + domain combination.
   /// </summary>
   [PrimaryKey(nameof(AccountId), nameof(DomainId))]
   public class AccountPasswordForDomain
   {
      [ForeignKey("Account")]
      public int AccountId { get; set; }
      [ForeignKey("Domain")]
      public int DomainId { get; set; }
      [ForeignKey("Password")]
      public int PasswordId { get; set; }

      public virtual Account Account { get; set; }
      public virtual Domain Domain { get; set; }
      public virtual Password Password { get; set; }

      public DateTime CreatedAt { get; set; }
      public DateTime UpdatedAt { get; set; }

      public AccountPasswordForDomain()
      {
         Account = new Account();
         Domain = new Domain();
         Password = new Password();
         CreatedAt = DateTime.Now;
         UpdatedAt = DateTime.Now;
      }
   }
}