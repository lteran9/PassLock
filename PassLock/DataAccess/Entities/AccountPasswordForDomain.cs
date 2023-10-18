using PassLock.DataAccess.Entities;

namespace PassLock
{
   public class AccountPasswordForDomain
   {
      public int UniqueIdentifier { get; set; }

      public Account Account { get; set; }
      public Domain Domain { get; set; }
      public Password Password { get; set; }

      public AccountPasswordForDomain()
      {
         Account = new Account();
         Domain = new Domain();
         Password = new Password();
      }
   }
}