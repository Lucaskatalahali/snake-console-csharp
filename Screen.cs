namespace snake;

public class Screen
{
    int width = 100;
    int height = 25;
    public char[,] Grid {get; set;}

    public Screen()
    {
        Grid = new char[height, width];
        Clear();
        Print();
    }

    public void Clear()
    {
        for(int x = 0; x < height; x++)
        {
            for(int y = 0; y <width; y++)
            {
                Grid[x, y] = ' ';
            }
        }
    }

    public bool IsGridCellEmpty(Point p) => Grid[p.X, p.Y] == ' ';

    public void Print()
    {
        //Console.SetWindowSize(100, 25);
        //Console.SetBufferSize(100, 25);
        Console.CursorVisible = false;

        // 1. Bordas Horizontais (Superior e Inferior)
        for (int x = 0; x < width; x++)
        {
            Console.SetCursorPosition(x, 0); // Linha do topo
            Console.Write("-");

            Console.SetCursorPosition(x, height - 1); // Linha do fundo (24)
            Console.Write("-");
        }

        // 2. Bordas Verticais (Esquerda e Direita)
        for (int y = 0; y < height; y++)
        {
            Console.SetCursorPosition(0, y);           // Coluna esquerda
            Console.Write("|");

            Console.SetCursorPosition(width - 1, y); // Coluna direita (99)
            Console.Write("|");
        }
    }
}