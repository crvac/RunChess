namespace ChessLibrary;

public interface IMoveFigure
{
    public bool NewCoordMoveValidate(Coord coord, Coord newcoord);
}
