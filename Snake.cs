namespace snake;

public class Snake
{
    public List<Point> SnakePoints {get; set;}
    public char SnakeChar {get; set;}
    public Snake()
    {
        SnakeChar = 'o';
        SnakePoints = [
                new Point(X: 12, Y: 49),
                new Point(X: 12, Y: 50),
                new Point(X: 12, Y: 51),
                ];
    }

    public void Print(Screen screen)
    {
        foreach(var point in SnakePoints)
        {
            screen.Grid[point.X, point.Y] = SnakeChar;
            Console.SetCursorPosition(point.Y, point.X); //No console invertemos
            Console.Write(SnakeChar);
        }
    }
}