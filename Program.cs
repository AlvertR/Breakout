using Breakout;

public class Program
{
    public Program() { }

    public static void Main(string[] args)
    {
        Game game = new Game("Breakout", 60, 800, 600);
        game.LoadGame();
    }
}