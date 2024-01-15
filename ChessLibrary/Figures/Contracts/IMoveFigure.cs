namespace ChessLibrary;

public interface IMoveFigure
{
    public bool NewCoordMoveValidate(Coord coord, Coord newcoord);

    public bool CheckPath(Coord fromCoord, Coord toCoord, Figure[,] board);
}
