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

            return Console.ReadLine() ?? string.Empty;
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }

         return string.Empty;
      }
   }
}