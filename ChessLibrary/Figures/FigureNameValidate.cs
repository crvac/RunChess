using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary;

public class FigureNameValidate
{
    public FigureName InputFigureName(string input, out FigureName figurename)
    {
        if (FigureName.TryParse(input.ToUpper(), out figurename))
        {
            return figurename;
        }
        else return FigureName.empty;
    }
}
