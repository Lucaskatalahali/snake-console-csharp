namespace snake;

public class Game
{
    public const int delayLimit = 10; //Runs n times and pauses for n milliseconds (it does two job)
    public static int Score {get; set;} = 0;
    private Snake _snake;
    public Screen screen;

    public Game()
    {
        screen = new(); //first we create the screen
        _snake = new();
        _snake.Print(screen);
        Helper.GenerateFood(screen);
    }

    public static void Delay() => Thread.Sleep(delayLimit);

    public static void Pause()
    {
        do
        {
            //do nothing, just pause untill Spacebar is clicked
        } while(Console.ReadKey(true).Key != ConsoleKey.Spacebar);
    }

    public void Start()
    { 
        Console.SetCursorPosition(0, Helper.height + 1);
        Console.WriteLine("Classic Mode");
        Console.Write($"Score: {Score}"); //também poderia ser : 0
        ConsoleKey key = _snake.Move(screen, ConsoleKey.A, (ConsoleKey.RightArrow, ConsoleKey.D), 0, -1);  // A -> MOVE OF LEFT
        
        do
        {
            if(key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
                key = _snake.Move(screen, key, (ConsoleKey.RightArrow, ConsoleKey.D), 0, -1);                

            if(key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                key = _snake.Move(screen,key, (ConsoleKey.DownArrow, ConsoleKey.S), -1, 0);
            
            if(key == ConsoleKey.D || key == ConsoleKey.RightArrow)
                key = _snake.Move(screen, key, (ConsoleKey.LeftArrow, ConsoleKey.A), 0, 1);

            if(key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                key = _snake.Move(screen, key, (ConsoleKey.UpArrow, ConsoleKey.W), 1, 0);

            if(key == ConsoleKey.D0)
            {
                GameOver();  
                break;
            }
            
        }while(true);
    }   
        
    public void GameOver()
    {
        Console.SetCursorPosition(0, Helper.height + 1);
        Console.WriteLine("\t\t== Game Over ==");
        Console.WriteLine($"Score: {Score}"); //desnecessário, mas apenas pra sobrescrever
        
        Console.CursorVisible = true;
    }
}