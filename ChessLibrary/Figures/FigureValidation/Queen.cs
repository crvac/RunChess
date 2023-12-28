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
}
