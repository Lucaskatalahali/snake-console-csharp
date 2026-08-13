namespace snake;

public class Snake
{
    private LinkedList<Point> SnakePoints {get; set;}
    private char SnakeBody {get; set;}
    private char SnakeHead {get; set;}
    private Queue<ConsoleKey> _inputQueue;

    public Snake()
    {
        SnakeBody = 'o';
        SnakeHead = 'x';
        SnakePoints = new();

        //The snake position starts aproximately in the middle of the grib 
        SnakePoints.AddLast(new Point(X: Screen.height/2, Y: Screen.width/2));
        SnakePoints.AddLast(new Point(X: Screen.height/2, Y: Screen.width/2 + 1));
        SnakePoints.AddLast(new Point(X: Screen.height/2, Y: Screen.width/2 + 2));

        _inputQueue = new();
    }

    private bool HasEaten(Point foodPosition) => SnakePoints.First!.Value == foodPosition;

    private bool HasCollided(Point snake, Screen screen)
    {
        return
             snake.X == 0 ||
             snake.X == Screen.height - 1 ||
             snake.Y == 0 ||
             snake.Y == Screen.width - 1 ||
             (screen.Grid[snake.X, snake.Y] != ' ' && snake != screen.FoodPosition);
    }

    private void RemoveTail(Screen screen)
    {
        LinkedListNode<Point> last = SnakePoints.Last!;
        SnakePoints.RemoveLast();
        screen.WriteToConsole(last.Value, ' ');
    }

    public void Print(Screen screen)
    {
        int headControl = 0;
        foreach(var point in SnakePoints)
        {
            if(headControl == 0)
            {
                screen.WriteToConsole(point, SnakeHead); 
                headControl++;
            }
            else
            {
                screen.WriteToConsole(point, SnakeBody);
            }
        }    
    }

    public ConsoleKey Move(Screen screen, ConsoleKey key, (ConsoleKey subkey1, ConsoleKey subkey2) subkey, int x, int y)
    {   
        ConsoleKey input;
        while (true)
        {
            screen.WriteToConsole(SnakePoints.First!.Value, SnakeBody);

            //Novo ponto (a nova cabeça) estará na frente da actual cabeça
            Point p = SnakePoints.First!.Value with { X = SnakePoints.First.Value.X + x, Y = SnakePoints.First.Value.Y + y};
            SnakePoints.AddFirst(p); 

            
            //Verificar se comeu colidiu com a grade ou mordeu a cauda
            if (HasCollided(SnakePoints.First.Value, screen))
            {
                screen.WriteToConsole(SnakePoints.First.Value, SnakeHead);
                return ConsoleKey.D0; //Digit 0 means game over
            } 
            
            if (HasEaten(screen.FoodPosition))
            {
                Helper.GenerateFood(screen);
                Game.Score ++;
                Console.SetCursorPosition(0, Screen.height + 2); //+2 pra ir no Score
                Console.Write($"Score: {Game.Score}");
                
            }
                
            else
                //Se não comeu, a cauda da cobra deve desaparecer (o que passou a ser na verdade a nova cabeça), simulando andamento da cobra
                RemoveTail(screen);

            screen.WriteToConsole(SnakePoints.First.Value, SnakeHead); 

            //Pausar o screen um pouco e ler as entradas de movimento do usuário

            int delay = (int)Game.Level;
            if(key == ConsoleKey.A || key == ConsoleKey.LeftArrow || key == ConsoleKey.D || key == ConsoleKey.RightArrow)
                delay = delay/2; // run for approximately half the waiting time

            for(int i = 0; i < delay; i++)
            {
                Game.Delay();
                if (Console.KeyAvailable)
                {
                    if(_inputQueue.Count <= 2)
                    _inputQueue.Enqueue(Console.ReadKey(true).Key);
                }
            }

            if(_inputQueue.Count != 0)
            {
                input = _inputQueue.Dequeue();

                if(input == ConsoleKey.Spacebar)
                {
                    Game.Pause();
                }

                if(input != ConsoleKey.A && input != ConsoleKey.LeftArrow && 
                    input != ConsoleKey.W && input != ConsoleKey.UpArrow && 
                    input != ConsoleKey.D && input != ConsoleKey.RightArrow && 
                    input != ConsoleKey.S && input != ConsoleKey.DownArrow ||
                    (input == subkey.subkey1) || input == subkey.subkey2)
                    continue;
                break;
            }
        }

        return input;
    }
}