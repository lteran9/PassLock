namespace PassLock.DataAccess.Entities
{
   /// <summary>
   /// An account can be linked to an email or a username (or both).
   /// </summary>
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