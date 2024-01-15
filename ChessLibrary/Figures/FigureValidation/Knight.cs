namespace ChessLibrary;

public class Knight : IMoveFigure
{
    /// <summary>
    /// Validates the move for the bishop piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public bool NewCoordMoveValidate(Coord fromCoord, Coord toCoord)
    {
        // All possible moves of a knight
        int[] X = { 2, 2, 1, 1, -2, -2, -1, -1 };
        int[] Y = { 1, -1, 2, -2, 1, -1, 2, -2 };

        // Check if the move is valid or not
        for (int i = 0; i < 8; i++)
        {
            if (toCoord.numericLetter == fromCoord.numericLetter + X[i] && toCoord.number == fromCoord.number + Y[i])
            {
                return true;

            }
        }
        return false;
    }

    public bool CheckPath(Coord fromCoord, Coord toCoord, Figure[,] board)
    {
        return true;
    }
}

