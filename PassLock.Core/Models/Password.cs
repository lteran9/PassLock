using System;

namespace PassLock.Core
{
   public class Password
   {
      public int Id { get; set; }

      public string Key { get; set; }
      public string Value { get; set; }
      public string InitializationVector { get; set; }

      public DateTime CreatedAt { get; set; }
      public DateTime UpdatedAt { get; set; }

      public Password()
      {
         Key = string.Empty;
         Value = string.Empty;
         InitializationVector = string.Empty;
         CreatedAt = DateTime.Now;
         UpdatedAt = DateTime.Now;
      }
   }
}