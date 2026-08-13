namespace snake;

public class Screen
{
    public const int width = 100;
    public const int height = 25;
    public char[,] Grid {get; set;}
    public Point FoodPosition {get; set;} = default!;

    public Screen()
    {
        Grid = new char[height, width];
    }

    public void Clear()
    {
        for(int x = 0; x < height; x++)
        {
            for(int y = 0; y < width; y++)
            {
                Grid[x, y] = ' ';
            }
        }
    }

    public bool IsGridCellEmpty(Point p) => Grid[p.X, p.Y] == ' ';

    public void WriteToConsole(Point point, char character)
    {
        Grid[point.X, point.Y] = character;
        Console.SetCursorPosition(point.Y, point.X); //No console inverte-se o ponto para (Y, X)
        Console.Write(character);
    }
    
    public void Print()
    {
        //Console.SetWindowSize(100, 25);
        //Console.SetBufferSize(100, 25);
        Console.CursorVisible = false;
        
        Clear();

        // 1. Bordas Horizontais (Superior e Inferior)
        for (int x = 0; x < width; x++)
        {        
            WriteToConsole(new Point(0, x), '-'); // Linha do topo

            WriteToConsole(new Point(height - 1, x), '-'); // Linha do fundo
        }

        // 2. Bordas Verticais (Esquerda e Direita)
        for (int y = 0; y < height; y++)
        {
            WriteToConsole(new Point(y, 0), '|'); //Coluna esquerda

            WriteToConsole(new Point(y, width - 1), '|'); //Coluna direita (99)
        }
    }
}