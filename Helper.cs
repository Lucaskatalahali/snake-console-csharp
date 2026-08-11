namespace snake;

public static class Helper
{
    public const int width = 100;
    public const int height = 25;
    public const char foodChar = 'o';
    public static Point FoodPosition {get; set;} = default!;

    /*public static  Point PointGenerator()
    {
        Point p = new(X: Random.Shared.Next(23), Y: Random.Shared.Next(99)); //de 24 até 99 pois em 25 e 100 já tem a barreira
        return p;
    }*/

    public static void GenerateFood(Screen screen)
    {
        Point food;
        do
        {
            //food must not be generated on the grid line 
            food = new(X: Random.Shared.Next(1, height - 1), Y: Random.Shared.Next(1, width - 1));
           
        }while(!screen.IsGridCellEmpty(food));

        FoodPosition = food;

        screen.WriteToConsole(FoodPosition, foodChar);
    }
    
    public static bool HasEaten(Point headPosition) => headPosition == FoodPosition;

    public static void Delay() => Thread.Sleep(10);

    public static bool HasCollidedWithGrid(Point snake)
    {
        return
             snake.X == 0 ||
             snake.X == height - 1 ||
             snake.Y == 0 ||
             snake.Y == width - 1;
    }
}