namespace snake;

public static class Helper
{
    public const int width = 100;
    public const int height = 25;
    public const char foodChar = 'o';
    public static Point FoodPosition {get; set;} = default!;
    
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

    public static bool HasCollidedWithGrid(Point snake)
    {
        return
             snake.X == 0 ||
             snake.X == height - 1 ||
             snake.Y == 0 ||
             snake.Y == width - 1;
    }

    public static int ReadOption(int limit)
    {
        while (true)
        {
            Console.WriteLine();
            Console.Write("Select Option: ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int option) && option >= 1 && option <= limit)
            {
                return option;
            }
            Console.WriteLine("Invalid option! Try again...");
        }
    }
}