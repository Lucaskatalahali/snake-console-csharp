namespace snake;

public class Screen
{
    public const char ObstacleChar = '\u2588'; // █
    public const int width = 100;
    public const int height = 25;
    public int LeftOffset { get; set; }
    public int TopOffset { get; set; }
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

    public void WriteToConsole(Point point, char character, ConsoleColor? color = null)
    {
        Grid[point.X, point.Y] = character;
        Console.SetCursorPosition(LeftOffset + point.Y, TopOffset + point.X); //No console inverte-se o ponto para (Y, X)

        if (color.HasValue)
        {
            Console.ForegroundColor = color.Value;
            Console.Write(character);
            Console.ResetColor();
        }
        else
        {
            Console.Write(character);
        }
    }

    public void PrintObstacles()
    {
        int hLength = width / 4;   // Largura da barreira horizontal (25 blocos)
        int vLength = height / 4;  // Altura da barreira vertical (6 blocos)

        // Posições estratégicas (afastadas do centro exato para dar espaço de manobra)
        int topRow = height / 4;          // Posição Y da barreira horizontal superior
        int bottomRow = (height * 3) / 4;  // Posição Y da barreira horizontal inferior
        
        int leftCol = width / 4;          // Posição X da barreira vertical esquerda
        int rightCol = (width * 3) / 4;    // Posição X da barreira vertical direita

        // 1. Barreiras Horizontais (Esquerda e Direita no topo/fundo)
        int hStart = (width - hLength) / 2; // Centraliza a linha horizontal no eixo X
        for (int i = 0; i < hLength; i++)
        {
            WriteToConsole(new Point(topRow, hStart + i), ObstacleChar, ConsoleColor.DarkGray);
            WriteToConsole(new Point(bottomRow, hStart + i), ObstacleChar, ConsoleColor.DarkGray);
        }

        // 2. Barreiras Verticais (Topo e Fundo nas laterais)
        int vStart = (height - vLength) / 2; // Centraliza a coluna vertical no eixo Y
        for (int i = 0; i < vLength; i++)
        {
            WriteToConsole(new Point(vStart + i, leftCol), ObstacleChar, ConsoleColor.DarkGray);
            WriteToConsole(new Point(vStart + i, rightCol), ObstacleChar, ConsoleColor.DarkGray);
        }
    }
    
    public void Print()
    {
        //Console.SetWindowSize(100, 25);
        //Console.SetBufferSize(100, 25);
        Console.CursorVisible = false;
         Console.CursorVisible = false;

        LeftOffset = Math.Max(0, (Console.WindowWidth - width) / 2);
        TopOffset = Math.Max(0, (Console.WindowHeight - height) / 2);
        
        Clear();

        // 1. Bordas Horizontais (Superior e Inferior)
        for (int x = 0; x < width; x++)
        {        
            WriteToConsole(new Point(0, x), '═', ConsoleColor.DarkGray); // Linha do topo

            WriteToConsole(new Point(height - 1, x), '═', ConsoleColor.DarkGray); // Linha do fundo
        }

        // 2. Bordas Verticais (Esquerda e Direita)
        for (int y = 0; y < height; y++)
        {
            WriteToConsole(new Point(y, 0), '║', ConsoleColor.DarkGray); //Coluna esquerda

            WriteToConsole(new Point(y, width - 1), '║', ConsoleColor.DarkGray); //Coluna direita (99)
        }

        // 3. Cantos
        WriteToConsole(new Point(0, 0), '╔', ConsoleColor.DarkGray);
        WriteToConsole(new Point(0, width - 1), '╗', ConsoleColor.DarkGray);
        WriteToConsole(new Point(height - 1, 0), '╚', ConsoleColor.DarkGray);
        WriteToConsole(new Point(height - 1, width - 1), '╝', ConsoleColor.DarkGray);
    }
}