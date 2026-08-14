namespace snake;

public enum GameLevel
{
    Easy = 30,
    Normal = 15,
    Hard = 6,
    Impossible = 2
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
            PrintMenuLine("              SNAKE");
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
            Console.WriteLine("       ↑ ↓ Select    ENTER Confirm");

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

    private static void PrintMenuLine(string text)
    {
        if (text.Length > MenuWidth)
            text = text[..MenuWidth];

        Console.WriteLine($"║{text.PadRight(MenuWidth)}║");
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
            PrintMenuLine("          SELECT LEVEL");
            Console.WriteLine("╠════════════════════════════════════╣");
            PrintMenuLine("");

            for (int i = 0; i < levels.Length; i++)
            {
                string prefix = i == selected ? "> " : "  ";
                PrintMenuLine($"        {prefix}{levels[i]}");
            }

            PrintMenuLine("");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("       ↑ ↓ Select    ENTER Confirm");

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

    public static void Pause()
    {
        string message = "== PAUSE ==";

        int x = (Screen.width - message.Length) / 2;
        int y = Screen.height + 1;

        Console.SetCursorPosition(x, y);
        Console.Write(message);

        do
        {
            // Wait until Spacebar is pressed
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
            PrintMenuLine("            HOW TO PLAY");
            Console.WriteLine("╠════════════════════════════════════╣");
            PrintMenuLine("");
            PrintMenuLine("  Move the snake using:");
            PrintMenuLine("");
            PrintMenuLine("      ↑ ↓ ← →");
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

            Console.SetCursorPosition(0, Screen.height + 1);
            Console.WriteLine($"{Level} Mode");
            Console.Write($"Score: {Score}"); //também poderia ser : 0
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
        string continueMessage = "Click any key to continue...";

        int gameOverX = (Screen.width - gameOverMessage.Length) / 2;
        int continueX = (Screen.width - continueMessage.Length) / 2;

        int y = Screen.height / 2;

        Console.SetCursorPosition(gameOverX, y);
        Console.Write(gameOverMessage);

        Console.SetCursorPosition(continueX, y + 1);
        Console.Write(continueMessage);

        Console.ReadKey(true);
        Console.CursorVisible = false;

        // Move the cursor below the game area
        Console.SetCursorPosition(0, Screen.height + 3);
    }
}