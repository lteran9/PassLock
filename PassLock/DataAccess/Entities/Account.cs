namespace PassLock.DataAccess.Entities
{
   public class Account
   {
      public int Id { get; set; }
      public int DomainId { get; set; }

      public string? Email { get; set; }
      public string? UserName { get; set; }
   }
}