using System;
using System.IO;
using System.Threading.Tasks;

namespace PassLock.Manager.Utils
{
   public class FileManager
   {
      static readonly string FilePath = "Encrypted/passwords.json";

      public static async Task<string> GetFileContent()
      {
         try
         {
            using (var stream = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
               using (var reader = new StreamReader(stream))
               {
                  return await reader.ReadToEndAsync();
               }
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return string.Empty;
      }

      public static async void SaveContentToFile(string content)
      {
         try
         {
            using (var stream = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
               using (var writer = new StreamWriter(stream))
               {
                  await writer.WriteAsync(content);
               }
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }
   }
}