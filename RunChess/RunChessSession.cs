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
            Console.Write("Choose youtr game: ");
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
                    default:
                        Console.WriteLine("Invalid input");
                        tryagain = true;
                        break;
                }
            }
        }while (tryagain);
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
        var figureNameValidate = new FigureNameValidate();

        //Creates an empty board and prints it
        _board = boardActions.MakeEmptyBoard();
        printBoard.PrintBoard(_board);

        //Put white king on board
        Figure kingWhite = new Figure("K","W");
        Console.Write("Enter the coordinates for the white King: ");
        kingWhite.coord = coordActions.InputCoorinates(Console.ReadLine());
        _board = boardActions.PlaceOnBoard(kingWhite);

        printBoard.PrintBoard(_board);
        //Put the Black figures on the board
        bool tryagain = true;

        Figure kingBlack = new Figure("K", "B");
        do
        {
            Console.Write("Enter the coordinates for the black King: ");
            kingBlack.coord = coordActions.InputCoorinates(Console.ReadLine());
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

        Figure queenBlack = new Figure("Q", "B");
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

        Figure rook1Black = new Figure("R", "B");        
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

        Figure rook2Black = new Figure("R", "B");
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
    }
}
