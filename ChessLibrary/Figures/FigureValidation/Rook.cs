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
}
