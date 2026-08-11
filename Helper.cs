namespace snake;

public static class Helper
{
    public const char foodChar = 'o';
    public static Point FoodPosition {get; set;} = default!;
    public static  Point PointGenerator()
    {
        Point p = new(X: Random.Shared.Next(23), Y: Random.Shared.Next(99)); //de 24 até 99 pois em 25 e 100 já tem a barreira
        return p;
    }

    public static void GenerateFood(Screen screen)
    {
        Point p; //aqui verificarei posição de acordo ao tamanho da cobre
        do
        {
            p = PointGenerator();
           
        }while(!screen.IsGridCellEmpty(p));

        FoodPosition = p;
        
        screen.Grid[FoodPosition.X, FoodPosition.Y] = foodChar;
        Console.SetCursorPosition(FoodPosition.Y, FoodPosition.X); //No console invertemos
        Console.Write(foodChar);
    }
    
    public static bool HasEaten(Point headPosition) => headPosition == FoodPosition;
}