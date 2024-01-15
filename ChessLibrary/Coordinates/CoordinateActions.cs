namespace ChessLibrary;

public class CoordinateActions
{

    /// <summary>
    /// Takes the input coordinates.
    /// </summary>
    /// <returns>coordinates</returns>
    public Coord InputCoorinates(string input)
    {
        if (input.Length == 2 && (char.ToUpper(input[0]) <= 'H' && char.ToUpper(input[0]) >= 'A')
                && int.TryParse(input[1].ToString(), out int coord2) && coord2 >= 1 && coord2 <= 8)
        {
            return new Coord(input);
        }
        else return InputCoorinates(Console.ReadLine());
    }

    /// <summary>
    /// Checks if the coordinates are equal
    /// </summary>
    /// <param name="cord1"></param>
    /// <param name="cord2"></param>
    /// <returns></returns>
    public bool AreCoordsEqual(Coord cord1, Coord cord2)
    {
        return cord1.number == cord2.number && cord1.numericLetter == cord2.numericLetter;
    }
}
