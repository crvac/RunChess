using ChessLibrary;

namespace ChessLib;

public struct Figure
{
    public FigureName name;
    public FigureTeam team;
    public Coord coordinates;

    public Figure()
    {
        name = FigureName.empty;
        team = FigureTeam.empty;
        coordinates = new Coord();
    }

    public Figure(string piece)
    {
        if (FigureName.TryParse(piece.ToUpper(), out FigureName _name))
        {
            name = _name;
        }
        else name = FigureName.empty;

        team = FigureTeam.white; // If no color is specified default color will be set to white

        coordinates = new Coord();
    }
    public Figure(string piece, string color, string coord)
    {
        var coordActions = new CoordinateActions();

        if (FigureName.TryParse(piece.ToUpper(), out FigureName _name))
        {
            name = _name;
        }
        else name = FigureName.empty;

        if (FigureTeam.TryParse(color.ToUpper(), out FigureTeam _team))
        {

            team = _team;
        }
        else team = FigureTeam.empty;

        coordinates = coordActions.InputCoorinates(coord);
    }
}
