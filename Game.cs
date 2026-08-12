namespace snake;

public enum GameLevel
{
    Easy = 40,
    Normal = 20,
    Hard = 8,
    Impossible = 4
}
public class Game
{
    public static GameLevel Level {get; private set;} = GameLevel.Easy;
    private const int delayLimit = 10;
    public static int Score {get; set;} = 0;
    private Snake _snake;
    private Screen _screen;

    public Game()
    {
        _screen = new(); //first we create the screen
        _snake = new();
    }
    
    private bool Menu()
    {
        Console.WriteLine($"1 - Start Game ({Level} Mode)");
        Console.WriteLine("2 - Change Level");

        int option = Helper.ReadOption(2);

        if (option == 1)
        {
            Console.Clear();
            return false;
        }
        else if (option == 2)
        {
            Level = ChangeGameLevel();
            Console.Clear();
            return true;
        }
        else return false;
    }

    private GameLevel ChangeGameLevel()
    {
        Console.Clear();
        
        // Ordena do maior delay (Easy = 60) para o menor (Impossible = 12)
        var levels = Enum.GetValues<GameLevel>()
                        .OrderByDescending(l => (int)l)
                        .ToArray();

        for (int i = 0; i < levels.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {levels[i]}");
        }

        int option = Helper.ReadOption(levels.Length);
        return levels[option - 1];
    }

    public static void Delay() => Thread.Sleep(delayLimit);

    public static void Pause()
    {
        Console.SetCursorPosition(Helper.width/2 - 4, Helper.height + 1);
        Console.Write("PAUSE");
        do
        {
            //do nothing, just pause untill Spacebar is clicked
        } while(Console.ReadKey(true).Key != ConsoleKey.Spacebar);
        Console.SetCursorPosition(Helper.width/2 - 4, Helper.height + 1);
        Console.Write("     ");
    }

    public void Start()
    {
        while(Menu());

        _screen.Print();
        _snake.Print(_screen);
        Helper.GenerateFood(_screen);

        Console.SetCursorPosition(0, Helper.height + 1);
        Console.WriteLine($"{Level} Mode");
        Console.Write($"Score: {Score}"); //também poderia ser : 0
        ConsoleKey key = _snake.Move(_screen, ConsoleKey.A, (ConsoleKey.RightArrow, ConsoleKey.D), 0, -1);  // A -> MOVE OF LEFT
        
        do
        {
            if(key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
                key = _snake.Move(_screen, key, (ConsoleKey.RightArrow, ConsoleKey.D), 0, -1);                

            if(key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                key = _snake.Move(_screen,key, (ConsoleKey.DownArrow, ConsoleKey.S), -1, 0);
            
            if(key == ConsoleKey.D || key == ConsoleKey.RightArrow)
                key = _snake.Move(_screen, key, (ConsoleKey.LeftArrow, ConsoleKey.A), 0, 1);

            if(key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                key = _snake.Move(_screen, key, (ConsoleKey.UpArrow, ConsoleKey.W), 1, 0);

            if(key == ConsoleKey.D0)
            {
                GameOver();  
                break;
            }
            
        }while(true);
    }   
        
    private void GameOver()
    {
        Console.SetCursorPosition(Helper.width/2 - 9, Helper.height + 1);
        Console.WriteLine("== Game Over ==");
        Console.WriteLine();
        
        Console.CursorVisible = true;
    }
}