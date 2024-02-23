using ChessLib;
using System.Net.NetworkInformation;

namespace ChessLibrary;

public class BoardLib
{
    private Figure[,] board = new Figure[8, 8];

    public Figure[,] GetBoard { get { return board; } }

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
                board[i, j].coord = new Coord(i, j);
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
        board[figure.coord.number, figure.coord.numericLetter] = new Figure(figure);

        return board;
    }



    /// <summary>
    /// Moves the figure on the board.
    /// </summary>
    /// <param name="oldcoord">The coordinates the figure is on</param>
    /// <param name="newcoord">The coordinates the figure has to go to</param>
    public Figure[,] MoveFiguretoNewCoord(Figure figure, Coord toCoord)
    {
        board[toCoord.number, toCoord.numericLetter] = new Figure(figure);
        board[toCoord.number, toCoord.numericLetter].coord = new Coord(toCoord);

        board[figure.coord.number, figure.coord.numericLetter] = new Figure();
        //board[figure.coord.number, figure.coord.numericLetter].coord = new Coord(figure.coord.number, figure.coord.numericLetter);

        return board;
    }

    /// <summary>
    /// Checks if the figure can go to the specified coordinate
    /// </summary>
    /// <param name="figure1">The figure</param>
    /// <param name="toCoord">The coord to go to</param>
    /// <returns>true/false</returns>
    public bool NewCordValidate(Figure figure1, Coord toCoord)
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
        return figure.NewCoordMoveValidate(coord, newcoord) && figure.CheckPath(coord, newcoord, board);
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
    /// If there's a check returns true
    /// </summary>
    /// <param name="figure1">Figure coordinates</param>
    /// <param name="king">Enemy king coordinates</param>
    /// <returns>true/false</returns>
    public bool CheckValidate(Figure figure1, Figure king)
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

        return MoveFigure(figure, figure1.coord, king.coord);
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

    /// <summary>
    /// Checks if theres check to the specified king
    /// </summary>
    /// <param name="king">King that needs to be checked for checks</param>
    /// <param name="newCoord">if the move is doen by the king (if king is not moved newCoord = king.Coord)</param>
    /// <returns>treu/false</returns>
    public bool FindCheck(Figure king, Coord newCoord)
    {
        king.coord = new Coord(newCoord);

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j].name != FigureName.empty && board[i, j].team != king.team)
                {
                    if (CheckValidate(board[i, j], king))
                        return true;
                    else continue;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Looks for avalable moves to check the king
    /// </summary>
    /// <param name="figure">figure</param>
    /// <param name="enemyKing">king to be checked</param>
    /// <returns>coordinates for the figure, if no check is found return an empty figure</returns>
    public Coord CheckForGame2(Figure figure, Figure enemyKing)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j].name == FigureName.empty)
                {
                    //Console.WriteLine(i + " " + j);
                    //figure.coord = new Coord(i, j);
                    if (NewCordValidate(figure, board[i, j].coord) /*&& CheckValidate(figure, enemyKing)*/)
                    {
                        Coord tempCoord = new Coord(figure.coord);
                        figure.coord = new Coord(i, j);
                        if (CheckValidate(figure, enemyKing) && !NewCordValidate(enemyKing, figure.coord))
                        {
                            /*board[i, j] = new Figure(figure);
                            board[tempCoord.number, tempCoord.numericLetter] = new Figure();*/

                            return board[i, j].coord;
                        }
                        else
                        {
                            figure.coord = new Coord(tempCoord);
                        }
                    }
                }
            }
        }
        return new Coord();
    }

    /// <summary>
    /// Validates mate without check
    /// </summary>
    /// <param name="king">King</param>
    /// <returns>T/F</returns>
    public bool MateValidate(Figure king)
    {
        //if (!FindCheck(king, king.coord))
        //    return false;
        //All king avlaable moves
        int[] x = { 0, 1, 1, 1, 0, -1, -1, -1 };
        int[] y = { 1, 1, 0, -1, -1, -1, 0, 1 };

        //MOve king to new coords to see if the move is valid
        for (int m = 1; m < 8; m++)
        {
            //validation
            if (0 <= king.coord.number + y[m] && king.coord.number + y[m] < 8 &&
                0 <= king.coord.numericLetter + x[m] && king.coord.numericLetter + x[m] < 8)
            {
                //Coord potentialMove = new Coord(king.coord.number + y[m], king.coord.numericLetter + x[m]);
                var ocupCells = new Figure[8,8];
                ocupCells = OccupiedCells(FigureTeam.B); //CAN U APPLY OPERATORS ON ENUM?

                if (ocupCells[king.coord.number + y[m], king.coord.numericLetter + x[m]].team == FigureTeam.empty)
                {
                    return false;
                }
                else continue;
            }
        }
        return true;
    }

    /// <summary>
    /// Returns the board with all occupied cells for the specified team
    /// </summary>
    /// <param name="team">Team</param>
    /// <returns>a board</returns>
    public Figure[,] OccupiedCells(FigureTeam team)
    {
        Figure[,] ocupBoard = new Figure[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                ocupBoard[i, j] = new Figure();
            }
        }
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j].team == team)
                {
                    ocupBoard[i, j] = new Figure(board[i, j]);
                    for (int k = 0; k < 8; k++) 
                    {
                        for (int m = 0; m < 8; m++)
                        {
                            if (NewCordValidate(board[i, j], new Coord(k, m)))
                            {
                                ocupBoard[k, m] = new Figure(board[i, j]);
                            }
                        }
                    }
                }
            }
        }
        return ocupBoard;
    }

    /// <summary>
    /// Lists all avlable moves for the night from the current position
    /// </summary>
    /// <param name="knight">KNight</param>
    /// <returns>the list of coords</returns>
    public List<Coord> KnightMoveList(Figure knight)
    {
        List<Coord> result = new List<Coord>();
        Figure[,] nBoard = OccupiedCells(knight.team);

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (nBoard[i,j].name == knight.name)
                {
                    result.Add(new Coord(i, j));
                }
            }
        }
        return result;
    }

    public bool KnightMove(Figure knight, Coord toCoord)
    {
        // On first move
        if (NewCordValidate(knight, toCoord))
        {
            knight.coord = toCoord;
            board = PlaceOnBoard(knight);
            return true;
        }
        else
        {
            // second move
            List<Coord> coords = KnightMoveList(knight);
            if (SeekMove(coords, toCoord))
            {
                return true;
            }
            else
            {
                // third move
                foreach (Coord coord in coords)
                {
                    knight.coord = coord;
                    List<Coord> coords2 = KnightMoveList(knight);
                    if (SeekMove(coords2, toCoord))
                    {
                        return true;
                    }
                }
            }
            return true;
        }
    }

    public bool SeekMove(List<Coord> coords, Coord toCoord)
    {
        Figure knight = new Figure { name = FigureName.N, team = FigureTeam.W };

        foreach (Coord coord in coords)
        {
            knight.coord = coord;
            if (NewCordValidate(knight, toCoord))
            {
                board = PlaceOnBoard(knight);
                knight.coord = toCoord;
                board = PlaceOnBoard(knight);
                return true;
            }
        }
        return false;
    }

}

