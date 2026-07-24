using Helicopter.Core;

internal class Program
{
    private static Game1 _game;

    private static void Main(string[] args)
    {
        try
        {
            System.Console.WriteLine("DEBUG: Program.Main starting...");
            _game = new Game1();
            System.Console.WriteLine("DEBUG: Game1 instance created successfully");
            _game.Run();
            System.Console.WriteLine("DEBUG: Game1.Run() returned control to Main");
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("CRASH IN MAIN: " + ex.ToString());
            throw;
        }
    }
}

