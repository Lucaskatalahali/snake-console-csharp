namespace snake;

public class Game
{
    private Snake _snake;
    char foodChar = 'o';
    public Screen screen;
    public Point FoodPosition {get; set;}
    public Game()
    {
        screen = new(); //first we create the screen
        _snake = new();
        FoodPosition = GenerateFood();
    }

    public Point GenerateFood()
    {
        Point p; //aqui verificarei posição de acordo ao tamanho da cobre
        do
        {
            p = Helper.PointGenerator();
           
        }while(!screen.IsGridCellEmpty(p));

        return p;
    }

    public void PrintFood()
    {
        screen.Grid[FoodPosition.X, FoodPosition.Y] = foodChar;
        Console.SetCursorPosition(FoodPosition.Y, FoodPosition.X); //No console invertemos
        Console.Write(foodChar);
    }

    public void Start()
    {
        _snake.Print(screen);
        PrintFood(); 

        var key = 'a';

        _snake.Move(screen, -1, 0);  // a = <- move on left

/*
        do
        {
            while (!Console.KeyAvailable)
            {
               // _snake.Move(screen,)
            }

           // if(char.TryParse(Console.ReadKey(true), out key))
            {
                
            }
        
        }while(true);*/
    }   
        

    public void NewMove()
    {
        
    }
}