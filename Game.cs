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

    public void Start()
    { 
        Console.SetCursorPosition(0, Helper.height + 1);
        Console.WriteLine("Classic Mode");
        Console.Write($"Score: {Score}"); //também poderia ser : 0
        char key = _snake.Move(screen, 'A', 'D', 0, -1);  // A -> MOVE OF LEFT
        
        do
        {
            if(key == 'A')
                key = _snake.Move(screen, key, 'D', 0, -1);                

            if(key == 'W')
                key = _snake.Move(screen,key, 'S', -1, 0);
            
            if(key == 'D')
                key = _snake.Move(screen, key, 'A', 0, 1);

            if(key == 'S')
                key = _snake.Move(screen, key, 'W', 1, 0);

            if(key == '0')
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