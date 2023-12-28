namespace ChessLibrary;

public struct Figure
{
    public FigureTeam team;
    public FigureName name;
    public Coord coord;


    public Figure()
    {
        team = FigureTeam.empty;
        name = FigureName.empty;
        coord = new Coord ();
    }
    public Figure(FigureName piece)
    {
        name = piece;

        team = FigureTeam.white; // If no color is specified default color will be set to white

        coord = new Coord();
    }
    public Figure(string piece, string color)
    {
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

        coord = new Coord();
    }
    public Figure(Figure figure)
    {
        name = figure.name;
        team = figure.team;
        coord = figure.coord;
    }
}

