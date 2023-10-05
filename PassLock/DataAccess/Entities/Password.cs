namespace PassLock.DataAccess.Entities
{
   public class Password
   {
      public int Id { get; set; }
      public int AccountId { get; set; }

      public string Value { get; set; }
      public string Salt { get; set; }
      public string InitializationVector { get; set; }

      public HashMethod HashMethod { get; set; }

      public DateTime CreatedAt { get; set; }
      public DateTime UpdatedAt { get; set; }

      public Password()
      {
         Value = string.Empty;
         Salt = string.Empty;
         InitializationVector = string.Empty;
         CreatedAt = DateTime.MinValue;
         UpdatedAt = DateTime.MinValue;
      }
   }
}