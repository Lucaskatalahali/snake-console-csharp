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
        Point food; //aqui verificarei posição de acordo ao tamanho da cobre
        do
        {
            food = new(X: Random.Shared.Next(height - 2), Y: Random.Shared.Next(width - 2)); //de 24 até 99 pois em 25 e 100 já tem a barreira();
           
        }while(!screen.IsGridCellEmpty(food));

        FoodPosition = food;
        
        screen.WriteToConsole(FoodPosition, foodChar);
    }
    
    public static bool HasEaten(Point headPosition) => headPosition == FoodPosition;
}