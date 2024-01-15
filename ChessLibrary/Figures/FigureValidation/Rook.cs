namespace ChessLibrary;

public class Rook : IMoveFigure
{
    /// <summary>
    /// Validates the move for the bishop piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public bool NewCoordMoveValidate(Coord fromCoord, Coord toCoord)
    {
        return toCoord.numericLetter == fromCoord.numericLetter ^ toCoord.number == fromCoord.number;

    }

    /// <summary>
    /// Checks if the path to the new coord is empty.
    /// </summary>
    /// <param name="fromCoord">The coordinates the figure is on</param>
    /// <param name="toCoord">The coordinates it need to move to</param>
    /// <param name="board">The board with the figures</param>
    /// <returns>Tru if patch is empty</returns>
    public bool CheckPath(Coord fromCoord, Coord toCoord, Figure[,] board)
    {
        if (!NewCoordMoveValidate(fromCoord, toCoord)) return false;
        if (board[toCoord.number, toCoord.numericLetter].team ==
            board[fromCoord.number, fromCoord.numericLetter].team) 
                return false;

        if (toCoord.numericLetter == fromCoord.numericLetter &&
            fromCoord.number < toCoord.number)
        {
            for (int i = fromCoord.number + 1; i < toCoord.number - 1; i++)
            {
                if (board[i, toCoord.numericLetter].name != FigureName.empty)
                    return false;
            }
            return true;
        }
        else if (toCoord.numericLetter == fromCoord.numericLetter && fromCoord.number > toCoord.number)
        {
            for (int i = fromCoord.number - 1; i > toCoord.number + 1; i--)
            {
                if (board[i, toCoord.numericLetter].name != FigureName.empty)
                    return false;
            }
            return true;
        }
        else if (toCoord.number == fromCoord.number && fromCoord.numericLetter < toCoord.numericLetter)
        {
            for (int i = fromCoord.numericLetter + 1; i < toCoord.numericLetter - 1; i++)
            {
                if (board[toCoord.number, i].name != FigureName.empty)
                    return false;
            }
            return true;
        }
        else if (toCoord.number == fromCoord.number && fromCoord.numericLetter > toCoord.numericLetter)
        {
            for (int i = fromCoord.numericLetter - 1; i > toCoord.numericLetter + 1; i--)
            {
                if (board[toCoord.number, i].name != FigureName.empty)
                    return false;
            }
            return true;
        }
        else return false;
    }
}
