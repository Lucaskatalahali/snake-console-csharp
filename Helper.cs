namespace snake;

public static class Helper
{
    public const char foodChar = '\u25CF';//\u25CB'; // ○
    
    public static void GenerateFood(Screen screen)
    {
        Point food;
        do
        {
            //food must not be generated on the grid line 
            food = new(X: Random.Shared.Next(1, Screen.height - 1), Y: Random.Shared.Next(1, Screen.width - 1));
           
        }while(!screen.IsGridCellEmpty(food));

        screen.FoodPosition = food;

        screen.WriteToConsole(food, foodChar, ConsoleColor.Red);
    }
}