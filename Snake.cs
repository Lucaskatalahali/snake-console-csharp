namespace snake;

public class Snake
{
    public LinkedList<Point> SnakePoints {get; set;}
    
    public char SnakeBody {get; set;}
    public char SnakeHead {get; set;}
    public Snake()
    {
        SnakeBody = 'o';
        SnakeHead = 'x';
        SnakePoints = new();

        //The snake position starts aproximately in the middle of the grib 
        SnakePoints.AddLast(new Point(X: 12, Y: 49));
        SnakePoints.AddLast(new Point(X: 12, Y: 50));
        SnakePoints.AddLast(new Point(X: 12, Y: 51));
    }

    public void Print(Screen screen)
    {
        int headControl = 0;
        foreach(var point in SnakePoints)
        {
            if(headControl == 0)
            {
                screen.Grid[point.X, point.Y] = SnakeHead;
                Console.SetCursorPosition(point.Y, point.X); //No console invertemos
                Console.Write(SnakeHead);
                headControl++;
            }
            else
            {
                screen.Grid[point.X, point.Y] = SnakeBody;
                Console.SetCursorPosition(point.Y, point.X); //No console inverte-se
                Console.Write(SnakeBody);
            }
        }    
    }

    public char Move(Screen screen, char key, int x, int y)
    {
        char input = key;
        do{
            while (!Console.KeyAvailable)
            {
                LinkedListNode<Point> last = SnakePoints.Last!;
                SnakePoints.RemoveLast();

                screen.Grid[last.Value.X, last.Value.Y] = ' ';
                Console.SetCursorPosition(SnakePoints.First!.Value.Y, SnakePoints.First!.Value.X);
                Console.Write(SnakeBody);

                Console.SetCursorPosition(last.Value.Y, last.Value.X); //No console inverte-se
                Console.Write(' ');

                Point p = SnakePoints.First!.Value with { X = SnakePoints.First.Value.X + x, Y = SnakePoints.First.Value.Y + y};
                SnakePoints.AddFirst(p);

                screen.Grid[SnakePoints.First.Value.X, SnakePoints.First.Value.Y] = SnakeHead;
                Console.SetCursorPosition(SnakePoints.First.Value.Y, SnakePoints.First.Value.X); //No console inverte-se
                Console.Write(SnakeHead);
                Thread.Sleep(200);
            }

            input = char.ToUpper(Console.ReadKey(true).KeyChar);
            if(input != 'A' && input != 'W' && input != 'D' && input != 'S') //depois acrescentar teclar pausa
            input = key;
            
        }while(input == key);

        return input;
    }
}