using System;

//CSD 228
//Assignment02
//Natlia Solar
// design reviewer: Chris S Ellis

namespace LWTech.Natalia.Solar.Assignment02
{
    enum Players { Human, Computer, NoOne };

    class Program
    {        
        static Random rng = new Random();
        const int humanChecker = 1;
        const int computerChecker = 2;


        static void Main(string[] args)
        {
            Console.WriteLine("Connect Four Game");
            Console.WriteLine("========================================");
            Console.WriteLine();

            int[,] grid = new int[7,7];
            bool gameOver = false;
            Players currentPlayer = Players.Human; ;            
            int columnNumber = 0;
            Players winner = Players.NoOne;
            

            // Initialize the grid
            InitializeGrid(grid);


            // While we don't have a winner...
            while (!gameOver)
            {
                //  Display the grid
                Console.WriteLine("It's your turn!  Here's the current grid:");
                DisplayGrid(grid);

                // Have a human take turn
                currentPlayer = Players.Human;

                //Prompt human for column number                
                columnNumber =  PromptColumnNumberHuman(grid);

                //Drop the checker into the column
                DropChecker(grid, columnNumber, currentPlayer);

                // Display the new grid
                DisplayGrid(grid);

                // Did he get four in a row?
                if (FoundFourInARow(grid, Players.Human))
                {
                    gameOver = true;
                    winner = Players.Human;
                    break;
                }
                
                // No. Have a computer take turn                
                currentPlayer = Players.Computer;
                Console.WriteLine("It's the computer's turn.");

                columnNumber = PromptColumnNumberComputer(grid);
                Console.ReadLine();

                Console.WriteLine($"The computer drops a checker in column #{columnNumber + 1}");
                Console.ReadLine();

                //Drop the checker into the column
                DropChecker(grid, columnNumber, currentPlayer);

                // Display the grid
                DisplayGrid(grid);
                Console.ReadLine();

                // Did the computer get four in a row?
                if (FoundFourInARow(grid, Players.Computer))
                {
                    gameOver = true;
                    winner = Players.Computer;
                    break;
                }

                //No, Is it a Tie game?
                if (TieGame(grid))
                {
                    gameOver = true;
                    winner = Players.NoOne;                    
                }
                else Console.WriteLine("\nNew round!\n");
            }

            if (winner == Players.Human) Console.WriteLine("Congratulations! You Won!");
            else if (winner == Players.Computer) Console.WriteLine("Computer won!");
            else if (winner == Players.NoOne) Console.WriteLine("It's a Tie! Game over...");
            Console.ReadLine();
        }




        private static void InitializeGrid(int[,] grid)
        {
            for (int j = 0; j < 7; j++)
            {
                for (int i = 0; i < 7; i++)
                {
                    grid[i,j] = 0;
                }
            }
        }



        private static void DisplayGrid(int[,] grid)
        {
            char c;
            Console.WriteLine("---+---+---+---+---+---+---");
            for (int j = 0; j < 7; j++)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (grid[i, j] == 1) c = 'X';
                    else if (grid[i, j] == 2) c = 'O';
                    else c = ' ';
                    Console.Write(" " + c);
                    if (i < 7)
                        Console.Write(" |");
                }
                Console.WriteLine();
                if (j < 7)
                    Console.WriteLine("---+---+---+---+---+---+---");
            }
            Console.WriteLine();
        }



        private static int PromptColumnNumberHuman(int[,] grid)
        {
            string input = "";
            bool gotColumn = false;
            int columnNumber = 0;
            do
            {
                Console.WriteLine("Which column would you like to drop a checker into (1-7)?");
                input = Console.ReadLine();
                while (!int.TryParse(input, out columnNumber) || columnNumber < 1 || columnNumber > 7)
                { 
                        Console.WriteLine("I'm sorry. I couldn't understand what you entered.  Please enter column number again.");
                        input = Console.ReadLine();
                }                
                // check if column is not full
                if (!ColumnIsFull(columnNumber - 1, grid)) gotColumn = true;
                else
                {
                    Console.WriteLine("This column is full.  Please enter different column number again.");
                    gotColumn = false;
                }
            } while (!gotColumn);
            return columnNumber-1;
        }


        private static bool ColumnIsFull(int column, int[,] grid)
        {
            for (int j = 6; j >=0; j--)
            {
                if (grid[column, j] == 0) return false;               
            }
            return true;
        }


        private static void DropChecker(int[,] grid, int columnNumber, Players currentPlayer)
        {          
            int freeSpotIndex = -1;
            int j = 0;
            for (j = 0; j<7; j++)
            {
                if (grid[columnNumber, j] == 0) freeSpotIndex = j;
            }
            if (currentPlayer == Players.Human) grid[columnNumber, freeSpotIndex] = humanChecker;
            else
            {
                grid[columnNumber, freeSpotIndex] = computerChecker;
            }
        }


        private static int PromptColumnNumberComputer(int[,] grid)
        {
            bool gotColumn = false;
            int columnNumber = 0;
            while (!gotColumn)
            {
                columnNumber = rng.Next(7);
                if (!ColumnIsFull(columnNumber, grid)) gotColumn = true;
            }
            return columnNumber;
        }


        private static bool FoundFourInARow(int[,] grid, Players currentPlayer)
        {
            int currentChecker = 0;
            int counter = 0;
            if (currentPlayer == Players.Human) currentChecker = humanChecker;
            else currentChecker = computerChecker;
            //check vertical
            for (int i = 0; i<7; i++)
            {
                for (int j = 6; j>=0; j--)
                {
                    if (grid[i, j] != currentChecker) counter = 0;                   
                    if (grid[i, j] == 0) break;
                    if (grid[i, j] == currentChecker) counter++;
                    if (counter == 4) return true;
                }
                counter = 0;
            }
            //ckeck horisontal
            for (int j = 0; j < 7; j++)
            {
                for (int i = 6; i >= 0; i--)
                {
                    if (grid[i, j] != currentChecker) counter = 0;
                    if (grid[i, j] == currentChecker) counter++;
                    if (counter == 4) return true;
                }
                counter = 0;
            }
            return false;

        }


        private static bool TieGame(int[,] grid)
        {
            for (int i = 0; i<7; i++)
            {
                if (!ColumnIsFull(i, grid)) return false;
            }
            return true;
        }

    }
}
