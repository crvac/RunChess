using ChessLibrary;

namespace ChessLibrary;

/// <summary>
/// Takes in the string of the coordinate input and gives integer values for number and letter.
/// </summary>
public struct Coord
{
    public char letter;
    public int numericLetter;
    public int number;

    public Coord()
    {
        letter = 'Z';
        numericLetter = 99;
        number = 99;
    }
    public Coord (string coordinate)
    {
        letter = char.ToUpper(coordinate[0]);
        number = int.Parse(coordinate[1].ToString()) - 1;
        numericLetter = (int)Letters.Parse(typeof(Letters), letter.ToString());
    }

    public Coord(Coord coord)
    {
        letter = coord.letter;
        numericLetter = coord.numericLetter;
        number = coord.number;
    }
}
