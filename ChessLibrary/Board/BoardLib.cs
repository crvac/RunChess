namespace ChessLibrary;

public class BoardLib
{
    private Figure[,] board = new Figure[8, 8];

    /// <summary>
    /// Creates a chessboard without the coordinates.
    /// </summary>
    /// <returns>An array of the board</returns>
    public Figure[,] MakeEmptyBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j].name = FigureName.empty;
                board[i, j].team = FigureTeam.empty;
            }
        }

        return board;
    }

    /// <summary>
    /// Places the Figure otn the specified coordinates
    /// </summary>
    /// <param name="coord">specified coordinates</param>
    /// <param name="figurename">Figure</param>
    public Figure[,] PlaceOnBoard(Figure figure)
    {
        board[figure.coord.number, figure.coord.numericLetter].name = figure.name;
        board[figure.coord.number, figure.coord.numericLetter].team = figure.team;

        return board;
    }



    /// <summary>
    /// Moves the figure on the board.
    /// </summary>
    /// <param name="oldcoord">The coordinates the figure is on</param>
    /// <param name="newcoord">The coordinates the figure has to go to</param>
    public Figure[,] MoveFiguretoNewCoord(Figure figure, Coord toCoord)
    {
        board[toCoord.number, toCoord.numericLetter] = board[figure.coord.number, figure.coord.numericLetter];
        //figure.coord = new Coord(toCoord);

        board[figure.coord.number, figure.coord.numericLetter] = new Figure();

        return board;
    }

    public bool NewCordValidate(Figure figure1 ,Coord toCoord)
    {
        var coordinateActions = new CoordinateActions();
        //Console.WriteLine("Where do you want to move your piece?");
//      var newcoord = coordinateActions.InputCoorinates();
        IMoveFigure figure = null;
        switch (figure1.name)
        {
            case FigureName.B:
                figure = new Bishop();
                break;
            case FigureName.K:
                figure = new King();
                break;
            case FigureName.N:
                figure = new Knight();
                break;
            case FigureName.Q:
                figure = new Queen();
                break;
            case FigureName.R:
                figure = new Rook();
                break;
            default:
                break;
        }

        return MoveFigure(figure, figure1.coord, toCoord);
    }

    public bool MoveFigure(IMoveFigure figure, Coord coord, Coord newcoord)
    {
        return figure.NewCoordMoveValidate(coord, newcoord);
    }

    /// <summary>
    /// Checks if there's a figure on the new coordinate, then checks if it can be taken
    /// </summary>
    /// <param name="coord">The coordinate the piece is on</param>
    /// <param name="newcoord">The coordinate the piece needs to move to</param>
    /// <returns>True or Flase</returns>
    public bool TakeValidate(Coord fromCoord, Coord toCoord)
    {
        return board[toCoord.number, toCoord.numericLetter].team != board[fromCoord.number, fromCoord.numericLetter].team;
    }

    /// <summary>
    /// If after the move theres a check return true
    /// </summary>
    /// <param name="fromCoord">Figure coordinates</param>
    /// <param name="toCoord">Enemy king coordinates</param>
    /// <returns>true/false</returns>
    public bool CheckValidate(Figure figure1, Figure figure2)
    {
        IMoveFigure figure = null;
        switch (figure1.name)
        {
            case FigureName.B:
                figure = new Bishop();
                break;
            case FigureName.K:
                figure = new King();
                break;
            case FigureName.N:
                figure = new Knight();
                break;
            case FigureName.Q:
                figure = new Queen();
                break;
            case FigureName.R:
                figure = new Rook();
                break;
            default:
                break;
        }

        if (MoveFigure(figure, figure1.coord, figure2.coord))
        {

            return true;
        }
        else return false;
    }

    /// <summary>
    /// If there's already a figure on the coordinates returns false.
    /// </summary>
    /// <param name="figure">The figure you want to put on the board</param>
    /// <returns>true/false</returns>
    public bool CheckIfEmpty(Figure figure)
    {
        if (board[figure.coord.number, figure.coord.numericLetter].name == FigureName.empty)
        {
            return true;
        }
        else return false;
    }
}

