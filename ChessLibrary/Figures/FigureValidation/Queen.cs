namespace ChessLibrary;

public class Queen : IMoveFigure
{
    /// <summary>
    /// Validates the move for the bishop piece
    /// </summary>
    /// <param name="oldcoord">Old coordinate</param>
    /// <param name="newcoord">new coordinate to validate</param>
    /// <returns>true or false</returns>
    public bool NewCoordMoveValidate(Coord fromCoord, Coord toCoord)
    {
        var bishop = new Bishop();
        var rook = new Rook();
        return bishop.NewCoordMoveValidate(fromCoord, toCoord) || rook.NewCoordMoveValidate(fromCoord, toCoord);
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
        var bishop = new Bishop();
        var rook = new Rook();

        if (!NewCoordMoveValidate(fromCoord, toCoord)) return false;

        if (bishop.NewCoordMoveValidate(fromCoord, toCoord)) return bishop.CheckPath(fromCoord, toCoord, board);
        else if (rook.NewCoordMoveValidate(fromCoord, toCoord)) return rook.CheckPath(fromCoord, toCoord, board);
        else return false;
    }
}
