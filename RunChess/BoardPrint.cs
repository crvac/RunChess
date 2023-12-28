using ChessLibrary;

namespace RunChess;

internal class BoardPrint
{
    /// <summary>
    /// Prints out the board with the coordinates.
    /// </summary>
    /// <param name="board">Chess board</param>
    public void PrintBoard(Figure[,] board)
    {
        string[][] coordinates = new string[9][];

        coordinates[0] = new string[9];
        for (int i = 0; i < 9; i++)
        {
            if (i < 8) coordinates[0][i + 1] = ((Letters)i).ToString();
            if (i > 0)
            {
                coordinates[i] = new string[1];
                coordinates[i][0] = Convert.ToString(i);
            }
            else coordinates[0][0] = " ";
        }

        for (int i = 0; i < 9; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(coordinates[i][j] + " ");
                }
            }
            else
            {
                Console.WriteLine();
                Console.Write(coordinates[i][0]);
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j + 2) % 2 == 0) Console.BackgroundColor = ConsoleColor.DarkRed; //■
                    else Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" ");
                    if (board[i - 1, j].name == FigureName.empty)
                    {
                        Console.Write(" ");
                    }
                    else if (board[i - 1, j].team == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(board[i - 1, j].name);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(board[i - 1, j].name);
                    }

                }
                Console.ResetColor();
            }
        }
        Console.WriteLine();
    }
}
