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
            
        }while(true);

       // _snake.Move(screen, 0, -1); //MOVE ON LEFT 
       // _snake.Move(screen, -1, 0);  // W = <- move UP
       // _snake.Move(screen, 0, -1); //MOVE ON LEFT 

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