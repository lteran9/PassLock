using System.Text;

namespace PassLock.Commands
{
   public class ReadInputCommand : ICommand<string>
   {
      private readonly string _prompt;

      public ReadInputCommand(string prompt)
      {
         _prompt = prompt;
      }

      public string Execute()
      {
         try
         {
            Console.Write(_prompt);

            return ReadPrivateString();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return string.Empty;
      }

      public string ReadPrivateString()
      {
         var result = new StringBuilder();

         while (true)
         {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
               case ConsoleKey.Enter:
                  return result.ToString();
               case ConsoleKey.Backspace:
                  if (result.Length == 0)
                  {
                     continue;
                  }

                  result.Length--;
                  Console.Write("\b \b");
                  continue;
               default:
                  result.Append(key.KeyChar);
                  Console.Write("*");
                  continue;
            }
         }
      }
   }
}