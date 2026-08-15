namespace snake;

public enum GameLevel
{
    Easy = 30,
    Normal = 15,
    Hard = 6,
    Insane = 2
}
public class Game
{
    private bool _obstacle = false;
    public static GameLevel Level {get; private set;} = GameLevel.Easy;
    private const int MenuWidth = 36;
    private int _selectedMenuOption = 0;
    private const int delayLimit = 10;
    public static int Score {get; set;} = 0;
    private Snake _snake = null!;
    private Screen _screen = null!;

    private bool Menu()
    {
        int selected = _selectedMenuOption;

        Console.CursorVisible = false;

        while (true)
        {
            string[] options =
        [
            "Start Game",
            $"Change Level ({Level})",
            $"Obstacles ({(_obstacle ? "Enabled" : "Disabled")})",
            "How to Play",
            "Exit"
        ];
            Console.Clear();

            Console.WriteLine("╔════════════════════════════════════╗");
            PrintMenuLineCentered("SNAKE GAME");
            Console.WriteLine("╠════════════════════════════════════╣");
            PrintMenuLine("");

            for (int i = 0; i < options.Length; i++)
            {
                string prefix = i == selected ? "> " : "  ";
                PrintMenuLine($"        {prefix}{options[i]}");
            }

            PrintMenuLine("");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.WriteLine();
            // crédito
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("By Lucas \u2022 github.com/lucaskatalahali");
            Console.ResetColor();

            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
            {
                selected--;

                if (selected < 0)
                    selected = options.Length - 1;
            }
            else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
            {
                selected++;

                if (selected >= options.Length)
                    selected = 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                _selectedMenuOption = selected;

                if (selected == 0)
                {
                    Console.Clear();
                    return true;
                }

                if (selected == 1)
                {
                    Level = ChangeGameLevel();
                    continue;
                }

                if (selected == 2)
                {
                    _obstacle = !_obstacle;
                    continue;
                }

                if (selected == 3)
                {
                    ShowHowToPlay();
                    continue;
                }

                if (selected == 4)
                {
                    Console.Clear();
                    Console.CursorVisible = true;
                    return false;
                }
            }
        }
    }

    // Alinha à esquerda e preenche até a borda direita
    private static void PrintMenuLine(string text)
    {
        if (text.Length > MenuWidth)
            text = text[..MenuWidth];

        Console.WriteLine($"║{text.PadRight(MenuWidth)}║");
    }

    // Centraliza perfeitamente o texto entre as duas bordas
    private static void PrintMenuLineCentered(string text)
    {
        if (text.Length > MenuWidth)
            text = text[..MenuWidth];

        int totalSpaces = MenuWidth - text.Length;
        int leftPadding = totalSpaces / 2;
        int rightPadding = totalSpaces - leftPadding;

        Console.WriteLine($"║{new string(' ', leftPadding)}{text}{new string(' ', rightPadding)}║");
    }

    private GameLevel ChangeGameLevel()
    {
        var levels = Enum.GetValues<GameLevel>()
                        .OrderByDescending(l => (int)l)
                        .ToArray();

        int selected = Array.IndexOf(levels, Level);

        while (true)
        {
            Console.Clear();

            Console.WriteLine("╔════════════════════════════════════╗");
            PrintMenuLineCentered("SELECT LEVEL");
            Console.WriteLine("╠════════════════════════════════════╣");
            PrintMenuLine("");

            for (int i = 0; i < levels.Length; i++)
            {
                string prefix = i == selected ? "> " : "  ";
                PrintMenuLine($"        {prefix}{levels[i]}");
            }

            PrintMenuLine("");
            Console.WriteLine("╚════════════════════════════════════╝");

            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
            {
                selected--;

                if (selected < 0)
                    selected = levels.Length - 1;
            }
            else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
            {
                selected++;

                if (selected >= levels.Length)
                    selected = 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                return levels[selected];
            }
        }
    }

    public static void Delay() => Thread.Sleep(delayLimit);

    public static void Pause(Screen screen)
    {
        string message = "== PAUSE ==";

        int x = screen.LeftOffset + (Screen.width - message.Length) / 2;
        int y = screen.TopOffset + Screen.height + 1;

        Console.SetCursorPosition(x, y);
        Console.Write(message);

        do
        {
        } while (Console.ReadKey(true).Key != ConsoleKey.Spacebar);

        Console.SetCursorPosition(x, y);
        Console.Write(new string(' ', message.Length));
    }

    private void ShowHowToPlay()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("╔════════════════════════════════════╗");
            PrintMenuLineCentered("HOW TO PLAY");
            Console.WriteLine("╠════════════════════════════════════╣");
            PrintMenuLine("");
            PrintMenuLine("  Move the snake using:");
            PrintMenuLine("");
            PrintMenuLine("      the arrow keys");
            PrintMenuLine("      or");
            PrintMenuLine("      W / A / S / D");
            PrintMenuLine("");
            PrintMenuLine("  Eat the food to increase");
            PrintMenuLine("  your score.");
            PrintMenuLine("");
            PrintMenuLine("  Avoid the walls and obstacles.");
            PrintMenuLine("");
            PrintMenuLine("  Press SPACE to pause.");
            PrintMenuLine("");
            PrintMenuLine("  Maximized window (100x30)");
            PrintMenuLine("  recommended for best view.");
            PrintMenuLine("");
            PrintMenuLine("  Press ESC to return.");
            PrintMenuLine("");
            Console.WriteLine("╚════════════════════════════════════╝");

            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
                return;
        }
    }

    public void Start()
    {
        while (true)
        {
            bool startGame = Menu();

            if (!startGame)
                return;

            Score = 0;
            _screen = new(); //first we create the screen
            _snake = new();

            _screen.Print();

            if(_obstacle) _screen.PrintObstacles();
            _snake.Print(_screen);
            Helper.GenerateFood(_screen);

            Console.SetCursorPosition(
                _screen.LeftOffset,
                _screen.TopOffset + Screen.height + 1
            );
            
            Console.WriteLine($"Difficulty: {Level}");

            Console.SetCursorPosition(
                _screen.LeftOffset,
                _screen.TopOffset + Screen.height + 2
            );

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Score: {Score}");
            Console.ResetColor();

            ConsoleKey key = _snake.Move(_screen, ConsoleKey.A, (ConsoleKey.RightArrow, ConsoleKey.D), 0, -1);  // A -> MOVE OF LEFT
            
            bool gameOver = false;

            do
            {
                if(key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
                    key = _snake.Move(_screen, key, (ConsoleKey.RightArrow, ConsoleKey.D), 0, -1);                

                if(key == ConsoleKey.W || key == ConsoleKey.UpArrow)
                    key = _snake.Move(_screen,key, (ConsoleKey.DownArrow, ConsoleKey.S), -1, 0);
                
                if(key == ConsoleKey.D || key == ConsoleKey.RightArrow)
                    key = _snake.Move(_screen, key, (ConsoleKey.LeftArrow, ConsoleKey.A), 0, 1);

                if(key == ConsoleKey.S || key == ConsoleKey.DownArrow)
                    key = _snake.Move(_screen, key, (ConsoleKey.UpArrow, ConsoleKey.W), 1, 0);

                if(key == ConsoleKey.D0)
                {
                    gameOver = true;
                }
            } while(!gameOver);

            GameOver();
        }   
    }
        
    private void GameOver()
    {
        string gameOverMessage = "== GAME OVER ==";
        string continueMessage = "click any key to continue...";
        int leftOffset = Math.Max(0, (Console.WindowWidth - Screen.width) / 2);
        int topOffset = Math.Max(0, (Console.WindowHeight - Screen.height) / 2);

        int gameOverX = leftOffset + (Screen.width - gameOverMessage.Length) / 2;
        int continueX = leftOffset + (Screen.width - continueMessage.Length) / 2;

        int y = topOffset + Screen.height / 2;

        Console.ForegroundColor = ConsoleColor.Red;

        Console.SetCursorPosition(gameOverX, y);
        Console.Write(gameOverMessage);

        Console.ForegroundColor = ConsoleColor.DarkGray;

        Console.SetCursorPosition(continueX, y + 1);
        Console.Write(continueMessage);
        Console.ResetColor();

        // Dá uma pequena pausa (500ms) para o jogador absorver o impacto da batida
        Thread.Sleep(1000);
        // Limpa qualquer tecla que tenha sido clicada durante o calor do jogo
        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }

        Console.ReadKey(true);
        Console.CursorVisible = false;

        // Move the cursor below the game area
        Console.SetCursorPosition(0, Screen.height + 3);
    }
}