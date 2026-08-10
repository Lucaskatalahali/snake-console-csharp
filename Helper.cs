namespace snake;

public static class Helper
{
    public static  Point PointGenerator()
    {
        Point p = new(X: Random.Shared.Next(25), Y: Random.Shared.Next(100)); //de 24 até 99 pois em 25 e 100 já tem a barreira
        return p;
    }
}