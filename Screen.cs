namespace snake;

public class Screen
{
    public char[,] Grid {get; set;}

    public Screen()
    {
        Grid = new char[Helper.height, Helper.width];
        Clear();
        Print();
    }

    public void Clear()
    {
        for(int x = 0; x < Helper.height; x++)
        {
            for(int y = 0; y <Helper.width; y++)
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

        // 1. Bordas Horizontais (Superior e Inferior)
        for (int x = 0; x < Helper.width; x++)
        {        
            WriteToConsole(new Point(0, x), '-'); // Linha do topo

            WriteToConsole(new Point(Helper.height - 1, x), '-'); // Linha do fundo
        }

        // 2. Bordas Verticais (Esquerda e Direita)
        for (int y = 0; y < Helper.height; y++)
        {
            WriteToConsole(new Point(y, 0), '|'); //Coluna esquerda

            WriteToConsole(new Point(y, Helper.width - 1), '|'); //Coluna direita (99)
        }
    }
}