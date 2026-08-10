namespace snake;

public record Food(Point FoodPosition);
public class Game
{
    char foodChar = 'o';
    public Screen screen;
    public Point FoodPosition {get; set;}
    public Game()
    {
        screen = new(); //first we create the screen
        FoodPosition = GenerateFood();
    }

    public Point GenerateFood()
    {
        Point p; //aqui verificarei posição de acordo ao tamanho da cobre
        do
        {
            p = new(X: Random.Shared.Next(25), Y: Random.Shared.Next(100)); //de 24 até 99 pois em 25 e 100 já tem a barreira
           
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
        Snake snake = new();
    }
}