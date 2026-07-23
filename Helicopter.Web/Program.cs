using Helicopter.Core;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            var game = new Game1();
            game.Run();
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("CRASH: " + ex.ToString());
            throw;
        }
    }
}
