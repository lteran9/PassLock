namespace PassLock
{
   public class Domain
   {
      public int Id { get; set; }

      public string Url { get; set; }

      public DateTime CreatedAt { get; set; }
      public DateTime UpdatedAt { get; set; }

      public Domain()
      {
         Url = string.Empty;
         CreatedAt = DateTime.Now;
         UpdatedAt = DateTime.Now;
      }

   }
}