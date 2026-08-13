namespace snake;

public static class Helper
{
    public const char foodChar = 'o';
    
    public static void GenerateFood(Screen screen)
    {
        Point food;
        do
        {
            //food must not be generated on the grid line 
            food = new(X: Random.Shared.Next(1, Screen.height - 1), Y: Random.Shared.Next(1, Screen.width - 1));
           
        }while(!screen.IsGridCellEmpty(food));

        screen.FoodPosition = food;

        screen.WriteToConsole(food, foodChar);
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