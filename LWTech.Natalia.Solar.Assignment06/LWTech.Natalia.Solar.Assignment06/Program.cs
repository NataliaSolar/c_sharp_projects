using System;
using System.Threading;

namespace LWTech.Natalia.Solar.Assignment06
{
    class Program
    {
        private const int ALIVE = 1;
        private const int DEAD = 0;
        private const int maxX = 60;
        private const int maxY = 40;
        private static Random rng = new Random();

        static void Main(string[] args)
        {
            bool quit = false;
            int numMilliseconds = 100;

            int[,] currentGrid = new int[maxX, maxY];
            int[,] newGrid = new int[maxX, maxY];

            InitializeTheGrid(newGrid);            

            while(true)
            {
                currentGrid = (int[,])newGrid.Clone();

                DisplayTheGrid(currentGrid);

                for (int y = 1; y < maxY - 1; y++)
                {
                    for (int x = 1; x < maxX - 1; x++)
                    {
                        int neighbors = AliveNeighborsNumber(currentGrid, x, y);

                        ApplyRulesOfLife(currentGrid, newGrid, x, y, neighbors);
                    }
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.Q:
                            quit = true;
                            break;
                        case ConsoleKey.R:
                            InitializeTheGrid(newGrid);
                            break;
                        case ConsoleKey.F:
                            FillUpEveryCell(newGrid);
                            break;
                        default:
                            break;
                    }
                }

                if (quit)
                    break;

                Thread.Sleep(numMilliseconds);
            }
        }


        private static void InitializeTheGrid(int[,] grid)
        {
            if (grid == null) throw new ArgumentNullException("Null argument passed.");
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (rng.Next(100) < 20)
                        grid[x, y] = ALIVE;
                    else
                        grid[x, y] = DEAD;
                }
            }
        }


        private static void DisplayTheGrid(int[,] grid)
        {
            if (grid == null) throw new ArgumentNullException("Null argument passed.");
            string line;
            for (int y = 0; y < maxY; y++)
            {
                line = "";
                for (int x = 0; x < maxX; x++)
                {
                    line += grid[x, y] == ALIVE ? "*" : " ";
                }
                Console.WriteLine(line);
            }
        }


        private static void FillUpEveryCell(int[,] grid)
        {
            if (grid == null) throw new ArgumentNullException("Null argument passed.");
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                        grid[x, y] = ALIVE;
                }
            }
        }

        private static int AliveNeighborsNumber(int[,] grid, int x, int y)
        {
            if (grid == null) throw new ArgumentNullException("Null argument passed.");
            int neighbors = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    neighbors += grid[x + i, y + j];
                }
            }

            neighbors -= grid[x, y];
            return neighbors;
        }

        private static void ApplyRulesOfLife(int[,] currentGrid, int[,] newGrid, int x, int y, int neighbors)
        {
            if (currentGrid == null || newGrid == null) throw new ArgumentNullException("Null argument passed.");
            if ((currentGrid[x, y] == ALIVE) && (neighbors < 2))
                newGrid[x, y] = DEAD;
            else if ((currentGrid[x, y] == ALIVE) && (neighbors > 3))
                newGrid[x, y] = DEAD;
            else if ((currentGrid[x, y] == DEAD) && (neighbors == 3))
                newGrid[x, y] = ALIVE;
            else
                newGrid[x, y] = currentGrid[x, y];
        }

    }
}
