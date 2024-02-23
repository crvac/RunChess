using ChessLibrary;

namespace RunChess;

internal class RunChessSession
{
    private Figure[,] _board = new Figure[8, 8];

    public void ChooseGame()
    {
        bool tryagain = true;
        do
        {
            Console.Write("Choose your game: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int game))
            {
                tryagain = false;
                switch (game)
                {
                    case 1:
                        RunGame1();
                        break;
                    case 2:
                        RunGame2();
                        break;
                    case 3:
                        RunGame3();
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        tryagain = true;
                        break;
                }
            }
        } while (tryagain);
    }
    /// <summary>
    /// Runs the application in Game 1 mode
    /// </summary>
    public void RunGame1()
    {
        var boardActions = new BoardLib();
        var printBoard = new BoardPrint();
        var coordActions = new CoordinateActions();
        var figureNameValidate = new FigureNameValidate();
        Figure figure = new Figure();
        do
        {
            //Prints an empty Board
            printBoard.PrintBoard(boardActions.MakeEmptyBoard());
            //Takes input coordinate and validates
            Console.Write("Enter the coordinates: ");
            figure.coord = coordActions.InputCoorinates(Console.ReadLine());
            //Takes input of the figure name, validates and prints on the board
            bool tryagain = true;
            do
            {
                Console.Write("Enter the figure name: ");
                string inputName = Console.ReadLine();
                //figure.team = FigureTeam.W;
                if (figureNameValidate.InputFigureName(inputName, out figure.name) != FigureName.empty)
                {
                    figure.team = FigureTeam.white;
                    //Prints the board with your figure on it
                    Figure figure1 = new Figure("K", "B");
                    figure1.coord = new Coord(3, 6);
                    boardActions.PlaceOnBoard(figure1);
                    Figure figure2 = new Figure("K", "B");
                    figure2.coord = new Coord(5, 4);
                    boardActions.PlaceOnBoard(figure2);
                    printBoard.PrintBoard(boardActions.PlaceOnBoard(figure));
                    tryagain = false;
                }
                else tryagain = true;
            } while (tryagain);

            tryagain = true;
            do
            {
                //Takes input coordinate and validates
                Console.Write("Enter the coordinates to move to: ");
                Coord toCoord = coordActions.InputCoorinates(Console.ReadLine());
                //Move validate
                if (boardActions.NewCordValidate(figure, toCoord))
                {
                    //Prints the board with your figure on the new coordinates
                    printBoard.PrintBoard(boardActions.MoveFiguretoNewCoord(figure, toCoord));
                    figure.coord = new Coord(toCoord);
                    boardActions.PlaceOnBoard(figure);
                    printBoard.PrintBoard(boardActions.OccupiedCells(FigureTeam.W));
                    tryagain = false;
                }
            } while (tryagain);
            Console.ReadLine();
        } while (true);
    }

    /// <summary>
    /// Runs the application in Game 2 mode
    /// </summary>
    public void RunGame2()
    {
        var boardActions = new BoardLib();
        var printBoard = new BoardPrint();
        var coordActions = new CoordinateActions();
        //var figureNameValidate = new FigureNameValidate();

        //Creates and print an empty board and prints it
        _board = boardActions.MakeEmptyBoard();
        printBoard.PrintBoard(_board);
        bool tryagain = true;



        Figure kingWhite = new Figure("K", "W");
        Figure kingBlack = new Figure("K", "B");
        Figure queenBlack = new Figure("Q", "B");
        Figure rook1Black = new Figure("R", "B");
        Figure rook2Black = new Figure("R", "B");


        bool stalemate = true;
        do
        {
            //Put white king on board
            Console.Write("Enter the coordinates for the white King: ");
            kingWhite.coord = coordActions.InputCoorinates(Console.ReadLine());
            _board = boardActions.PlaceOnBoard(kingWhite);

            printBoard.PrintBoard(_board);
            //Put the Black figures on the board
            tryagain = true;

            do
            {
                Console.Write("Enter the coordinates for the black King: ");
                kingBlack.coord = coordActions.InputCoorinates(Console.ReadLine());
                //Checks if WHite king will be chekced after placing down the figure, and if the coordinates are not busy by another figure
                if (boardActions.CheckValidate(kingBlack, kingWhite) || !boardActions.CheckIfEmpty(kingBlack))
                {
                    tryagain = true;
                }
                else
                {
                    _board = boardActions.PlaceOnBoard(kingBlack);
                    tryagain = false;
                }
            } while (tryagain);

            printBoard.PrintBoard(_board);

            //put the black queen on board (same logic as black king)

            do
            {
                Console.Write("Enter the coordinates for the queen: ");
                queenBlack.coord = coordActions.InputCoorinates(Console.ReadLine());
                if (boardActions.CheckValidate(queenBlack, kingWhite) || !boardActions.CheckIfEmpty(queenBlack))
                {
                    tryagain = true;
                }
                else
                {
                    _board = boardActions.PlaceOnBoard(queenBlack);
                    tryagain = false;
                }
            } while (tryagain);

            printBoard.PrintBoard(_board);

            //put 1st black rook
            do
            {
                Console.Write("Enter the coordinates for the 1st black rook: ");
                rook1Black.coord = coordActions.InputCoorinates(Console.ReadLine());
                if (!boardActions.CheckValidate(rook1Black, kingWhite)
                    && !boardActions.NewCordValidate(kingWhite, rook1Black.coord)
                    && boardActions.CheckIfEmpty(rook1Black))
                {
                    _board = boardActions.PlaceOnBoard(rook1Black);
                    tryagain = false;
                }
                else
                {
                    tryagain = true;
                }
            } while (tryagain);

            printBoard.PrintBoard(_board);

            //put 2nd black rook
            do
            {
                Console.Write("Enter the coordinates for the 2nd black rook: ");
                rook2Black.coord = coordActions.InputCoorinates(Console.ReadLine());
                if (!boardActions.CheckValidate(rook2Black, kingWhite)
                    && !boardActions.NewCordValidate(kingWhite, rook2Black.coord)
                    && boardActions.CheckIfEmpty(rook2Black))
                {
                    _board = boardActions.PlaceOnBoard(rook2Black);
                    tryagain = false;
                }
                else
                {
                    tryagain = true;
                }


            } while (tryagain);


            printBoard.PrintBoard(_board);

            if (!boardActions.FindCheck(kingWhite, kingWhite.coord) && boardActions.MateValidate(kingWhite))
            {
                Console.WriteLine("It's a stalemate");
                _board = boardActions.MakeEmptyBoard();
            }
            else stalemate = false;
        } while (stalemate);


        //PLaying chess
        int chooseFigure = 1; //A variabale to choose wich figure is moved 
        bool checkmate = false;
        do
        {


            //User moves white King
            tryagain = true;
            do
            {
                //Takes input coordinate and validates
                Console.Write("Enter the coordinates to move to: ");
                Coord toCoord = coordActions.InputCoorinates(Console.ReadLine());
                //Move validate
                if (boardActions.NewCordValidate(kingWhite, toCoord) && !boardActions.FindCheck(kingWhite, toCoord)
                    && !coordActions.AreCoordsEqual(toCoord, kingWhite.coord))
                {
                    //Prints the board with your figure on the new coordinates
                    _board = boardActions.MoveFiguretoNewCoord(kingWhite, toCoord);
                    printBoard.PrintBoard(_board);
                    kingWhite.coord = new Coord(toCoord);
                    tryagain = false;
                }
            } while (tryagain);

            // Black makes a move
            tryagain = true;
            do
            {
                if (boardActions.NewCordValidate(kingWhite, rook1Black.coord) || boardActions.NewCordValidate(kingWhite, rook2Black.coord))
                {
                    if (boardActions.NewCordValidate(kingWhite, rook1Black.coord))
                    {
                        // texapoxel rook-y kam pashtpanel taguhiov
                        Console.WriteLine("WOOOW");
                    }
                }
                //choose the pice to play with (Queen - 1 Rook1 - 2 Rook2 - 3)
                Coord game2 = new Coord();
                switch (chooseFigure)
                {
                    case 1:
                        chooseFigure = 2;
                        game2 = boardActions.CheckForGame2(queenBlack, kingWhite);
                        if (game2.letter != 'Z')
                        {
                            _board = boardActions.MoveFiguretoNewCoord(queenBlack, game2);
                            queenBlack.coord = new Coord(game2);
                            printBoard.PrintBoard(_board);

                            tryagain = false;
                        }
                        break;
                    case 2:
                        chooseFigure = 3;
                        game2 = boardActions.CheckForGame2(rook1Black, kingWhite);
                        if (game2.letter != 'Z')
                        {
                            _board = boardActions.MoveFiguretoNewCoord(rook1Black, game2);
                            rook1Black.coord = new Coord(game2);
                            printBoard.PrintBoard(_board);

                            tryagain = false;
                        }
                        break;
                    case 3:
                        chooseFigure = 1;
                        game2 = boardActions.CheckForGame2(rook2Black, kingWhite);
                        if (game2.letter != 'Z')
                        {
                            _board = boardActions.MoveFiguretoNewCoord(rook2Black, game2);
                            rook2Black.coord = new Coord(game2);
                            printBoard.PrintBoard(_board);

                            tryagain = false;
                        }
                        break;
                }
                //look for valid moves, and find the one with check
                //if no check is found play a safe move to avoid draw

            } while (tryagain);

            //If king is white king is under a check and cannot go anywhere, then its a mate.
            checkmate = boardActions.FindCheck(kingWhite, kingWhite.coord) && boardActions.MateValidate(kingWhite);

            printBoard.PrintBoard(boardActions.OccupiedCells(FigureTeam.B));
        } while (checkmate == false);
        Console.WriteLine("____CHEKCMATE____");
    }

    public void RunGame3()
    {
        var boardActions = new BoardLib();
        var printBoard = new BoardPrint();
        var coordActions = new CoordinateActions();

        _board = boardActions.MakeEmptyBoard();
        printBoard.PrintBoard(_board);

        Figure knightWhite = new Figure("N", "W");

        //Put white king on board
        Console.Write("Enter the coordinates for the white Knight: ");
        knightWhite.coord = coordActions.InputCoorinates(Console.ReadLine());
        _board = boardActions.PlaceOnBoard(knightWhite);
        printBoard.PrintBoard(_board);

        Console.Write("Enter the coordinates you wish to move to: ");
        Coord toCoord = coordActions.InputCoorinates(Console.ReadLine());

        printBoard.PrintBoard(boardActions.OccupiedCells(FigureTeam.W));

        /*
        if (boardActions.NewCordValidate(knightWhite, toCoord))
        {
            _board = boardActions.PlaceOnBoard(knightWhite);
            printBoard.PrintBoard(_board);
            Console.WriteLine("_____________1");
        }
        else
        {
            // Gives the list of 1st moves
            List<Coord> coords1 = boardActions.KnightMoveList(knightWhite);


            //checks if you can move in 2
            foreach (Coord coord1 in coords1)
            {
                Coord coord0 = knightWhite.coord;
                _board = boardActions.MoveFiguretoNewCoord(knightWhite,coord1);
                knightWhite.coord = coord1;
                //printBoard.PrintBoard(boardActions.OccupiedCells(FigureTeam.W));
                //Console.ReadLine();
                if (boardActions.NewCordValidate(knightWhite, toCoord))
                {
                    _board = boardActions.PlaceOnBoard(knightWhite);
                    knightWhite.coord = toCoord;
                    _board = boardActions.PlaceOnBoard(knightWhite);
                    printBoard.PrintBoard(_board);
                    Console.WriteLine("_______________2");
                    break;
                }
                _board = boardActions.MoveFiguretoNewCoord(knightWhite, coord0);
                knightWhite.coord = coord0;
            }

            //checks if you can goin 3
            foreach (Coord coord1 in coords1)
            {
                Coord coord0 = knightWhite.coord;
                _board = boardActions.MoveFiguretoNewCoord(knightWhite, coord1);
                knightWhite.coord = coord1;
                //printBoard.PrintBoard(boardActions.OccupiedCells(FigureTeam.W));
                //Console.ReadLine();
                bool @break = false;
                // Gives the list of 2nd moves
                List<Coord> coords2 = boardActions.KnightMoveList(knightWhite);

                foreach (Coord coord2 in coords2)
                {
                    _board = boardActions.MoveFiguretoNewCoord(knightWhite, coord2);
                    knightWhite.coord = coord2;
                    //printBoard.PrintBoard(boardActions.OccupiedCells(FigureTeam.W));
                    //Console.ReadLine();
                    if (boardActions.NewCordValidate(knightWhite, toCoord))
                    {
                        _board = boardActions.MakeEmptyBoard();
                        knightWhite.coord = coord0;
                        _board = boardActions.PlaceOnBoard(knightWhite);
                        knightWhite.coord = coord1;
                        _board = boardActions.PlaceOnBoard(knightWhite);
                        knightWhite.coord = coord2;
                        _board = boardActions.PlaceOnBoard(knightWhite);
                        knightWhite.coord = toCoord;
                        _board = boardActions.PlaceOnBoard(knightWhite);
                        printBoard.PrintBoard(_board);
                        Console.WriteLine("_______________3");
                        @break = true;
                        Console.ReadLine();
                        break;
                    }

                    if (@break)
                    {
                        break;
                    }
                    _board = boardActions.MoveFiguretoNewCoord(knightWhite, coord1);
                    knightWhite.coord = coord1;

                }
                _board = boardActions.MoveFiguretoNewCoord(knightWhite, coord0);
                knightWhite.coord = coord0;

            }
        }*/

        boardActions.KnightMove(knightWhite, toCoord);

        printBoard.PrintBoard(boardActions.GetBoard);
    }
}
