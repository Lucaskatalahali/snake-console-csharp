namespace snake;

public class Screen
{
    public const char ObstacleChar = '#';
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
            WriteToConsole(new Point(topRow, hStart + i), ObstacleChar);
            WriteToConsole(new Point(bottomRow, hStart + i), ObstacleChar);
        }

        // 2. Barreiras Verticais (Topo e Fundo nas laterais)
        int vStart = (height - vLength) / 2; // Centraliza a coluna vertical no eixo Y
        for (int i = 0; i < vLength; i++)
        {
            WriteToConsole(new Point(vStart + i, leftCol), ObstacleChar);
            WriteToConsole(new Point(vStart + i, rightCol), ObstacleChar);
        }
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