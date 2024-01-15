namespace ChessLibrary;

public class Bishop : IMoveFigure
{
    /// <summary>
    /// Validates the move for the bishop piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public bool NewCoordMoveValidate(Coord fromCoord, Coord toCoord)
    { 
        return (Math.Abs(toCoord.numericLetter - fromCoord.numericLetter) == Math.Abs(toCoord.number - fromCoord.number));
    }

    public bool CheckPath(Coord fromCoord, Coord toCoord, Figure[,] board)
    {
        if (!NewCoordMoveValidate(fromCoord, toCoord)) return false;
        if (board[toCoord.number, toCoord.numericLetter].team == board[fromCoord.number, fromCoord.numericLetter].team) return false;

        if (toCoord.numericLetter > fromCoord.numericLetter && toCoord.number > fromCoord.number)
        {
            for (int i = 1; i < Math.Abs(toCoord.numericLetter - fromCoord.numericLetter) - 1; i++)
            {
                if (board[fromCoord.number + i, fromCoord.numericLetter + i].name != FigureName.empty)
                    return false;
            }
            return true;
        }
        else if (toCoord.numericLetter < fromCoord.numericLetter && toCoord.number < fromCoord.number)
        {
            for (int i = 1; i < Math.Abs(toCoord.numericLetter - fromCoord.numericLetter) - 1; i++)
            {
                if (board[fromCoord.number - i, fromCoord.numericLetter - i].name != FigureName.empty)
                    return false;
            }
            return true;
        }
        else if (toCoord.numericLetter > fromCoord.numericLetter && toCoord.number < fromCoord.number)
        {
            for (int i = 1; i < Math.Abs(toCoord.numericLetter - fromCoord.numericLetter) - 1; i++)
            {
                if (board[fromCoord.number - i, fromCoord.numericLetter + i].name != FigureName.empty)
                    return false;
            }
            return true;
        }
        else if (toCoord.numericLetter < fromCoord.numericLetter && toCoord.number > fromCoord.number)
        {
            for (int i = 1; i < Math.Abs(toCoord.numericLetter - fromCoord.numericLetter) - 1; i++)
            {
                if (board[fromCoord.number + i, fromCoord.numericLetter - i].name != FigureName.empty)
                    return false;
            }
            return true;
        }
        else return false;
    }
}

