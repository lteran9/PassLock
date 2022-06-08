using System;
using System.IO;
using System.Threading.Tasks;

namespace PassLock.Manager.Utils
{
   public class FileManager
   {
      static readonly string FilePath = "Manager/Encrypted/passwords.json";

      /// <summary>
      /// This method will return the file contents at [FilePath].
      /// </summary>
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

      /// <summary> 
      /// This method will overwrite the contents at [FilePath]. If the file does not exist it will be automatically created.
      /// </summary>
      public static async void SaveContentToFile(string content)
      {
         try
         {
            using (var stream = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
               // Discard file content and overwrite with in-memory content
               stream.SetLength(0);

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