using System;

namespace PassLock.Config
{
   internal static class DatabaseSettings
   {
      public static string SqlitePath = "";

      private static readonly string Folder = "/PassLock";
      private static readonly string Database = "/data_warehouse.db3";

      /// <summary>
      /// Use this method to initialize the database. Will only create the SQLite files if not present already.
      /// </summary>
      /// <returns></returns>
      public static bool Init()
      {
         try
         {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            if (CheckIfDirectoryExists(Path.Join(path, Folder)) == false)
            {
               Directory.CreateDirectory(Path.Join(path, Folder));
            }

            if (CheckIfSqliteDbExists(Path.Join(path, Folder, Database)) == false)
            {
               File.Create(Path.Join(path, Folder, Database));
            }

            SqlitePath = Path.Join(path, Folder, Database);

            return true; // Initialized Successfully
         }
         catch (Exception ex)
         {
            // TODO: Handle Exceptions
            Console.Write(ex.Message);
         }

         return false;
      }

      private static bool CheckIfDirectoryExists(string path)
      {
         return Directory.Exists(path);
      }

      private static bool CheckIfSqliteDbExists(string filePath)
      {
         return File.Exists(filePath);
      }
   }
}