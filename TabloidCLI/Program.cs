using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine(@"
 __          ________ _      _____ ____  __  __ ______     _______ ____  
 \ \        / /  ____| |    / ____/ __ \|  \/  |  ____|   |__   __/ __ \ 
  \ \  /\  / /| |__  | |   | |   | |  | | \  / | |__         | | | |  | |
   \ \/  \/ / |  __| | |   | |   | |  | | |\/| |  __|        | | | |  | |
    \  /\  /  | |____| |___| |___| |__| | |  | | |____       | | | |__| |
     \/  \/   |______|______\_____\____/|_|  |_|______|      |_|  \____/ 
                                                                         
                                                                          
                                                                         

  _______    _     _       _     _       _____ _      _____ 
 |__   __|  | |   | |     (_)   | |     / ____| |    |_   _|
    | | __ _| |__ | | ___  _  __| |    | |    | |      | |  
    | |/ _` | '_ \| |/ _ \| |/ _` |    | |    | |      | |  
    | | (_| | |_) | | (_) | | (_| |    | |____| |____ _| |_ 
    |_|\__,_|_.__/|_|\___/|_|\__,_|     \_____|______|_____|
                                                            
                                                            ");
            Console.Write("Press any key to continue");
            Console.ReadKey();

            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }
        }
    }
}
