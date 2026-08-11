# 🐍 Snake Game (Console)

A classic Snake game built in **C#** to be played directly in the terminal/console.

Control the snake, eat the food that spawns on the screen to grow, and try to get the highest score possible. The game ends if you collide with the walls or bite your own tail.

---

## 🕹️ Controls

| Key | Action |
| :--- | :--- |
| **W** | Move **Up** |
| **S** | Move **Down** |
| **A** | Move **Left** |
| **D** | Move **Right** |

**Note:** Movement is controlled **exclusively via the WASD keys** (arrow keys are not supported yet).

---

## 🚀 How to Run

### Prerequisites

1. [.NET 10.0 SDK](https://dotnet.microsoft.com/) installed on your machine.

> If you have an older version installed (such as .NET 8), open the `.csproj` file and change `<TargetFramework>net10.0</TargetFramework>` to your installed version.

2. Get the code by downloading the ZIP or by cloning the repository using the terminal.
3. Open the project folder in your terminal or IDE (such as VS Code) and run:
   ```bash
   dotnet run

## Notes
Moving up and down may seem faster because monospace characters in the console are taller than they are wide.
Make sure your terminal window has a minimum size of 100 columns by 25 lines to prevent rendering glitches.
