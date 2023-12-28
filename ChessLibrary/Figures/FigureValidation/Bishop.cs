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
}

