namespace snake;

public class Snake
{
    public LinkedList<Point> SnakePoints {get; set;}
    public char SnakeBody {get; set;}
    public char SnakeHead {get; set;}
    private Queue<char> _inputQueue;

    public Snake()
    {
        SnakeBody = 'o';
        SnakeHead = 'x';
        SnakePoints = new();

        //The snake position starts aproximately in the middle of the grib 
        SnakePoints.AddLast(new Point(X: Helper.height/2, Y: Helper.width/2));
        SnakePoints.AddLast(new Point(X: Helper.height/2, Y: Helper.width/2 + 1));
        SnakePoints.AddLast(new Point(X: Helper.height/2, Y: Helper.width/2 + 2));

        _inputQueue = new();
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

    private void RemoveTail(Screen screen)
    {
        LinkedListNode<Point> last = SnakePoints.Last!;
        SnakePoints.RemoveLast();
        screen.WriteToConsole(last.Value, ' ');
    }

    private void Delay() => Thread.Sleep(10);

    public char Move(Screen screen, char key, char subkey, int x, int y)
    {
        char input;
        while (true)
        {
            screen.WriteToConsole(SnakePoints.First!.Value, SnakeBody);

            //Novo ponto (a nova cabeça) estará na frente da actual cabeça
            Point p = SnakePoints.First!.Value with { X = SnakePoints.First.Value.X + x, Y = SnakePoints.First.Value.Y + y};
            SnakePoints.AddFirst(p); 


            //guardar a nova cabeça na grid e escrever no terminal
           // screen.Grid[SnakePoints.First.Value.X, SnakePoints.First.Value.Y] = SnakeHead;

            screen.WriteToConsole(SnakePoints.First.Value, SnakeHead);     

            //Verificar se comeu comida ou se mordeu ou pancou na parede

            if (Helper.HasEaten(SnakePoints.First.Value))
                Helper.GenerateFood(screen);
            else
                //Se não comeu, a cauda da cobra deve desaparecer (o que passou a ser na verdade a nova cabeça), simulando andamento da cobra
                RemoveTail(screen);

            //Pausar o screen um pouco e ler as entradas de movimento do usuário

            for(int i = 0; i < 10; i++)
            {
                Delay();
                if (Console.KeyAvailable)
                {
                    if(_inputQueue.Count <= 2)
                    _inputQueue.Enqueue(char.ToUpper(Console.ReadKey(true).KeyChar));
                }
            }

            if(_inputQueue.Count != 0)
            {
                input = _inputQueue.Dequeue();
                if((input != 'A' && input != 'W' && input != 'D' && input != 'S') || input == subkey) //depois acrescentar teclar pausa
                    continue;
                break;
            }
        }

        return input;
    }
}