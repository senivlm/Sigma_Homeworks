using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixLib
{
    public enum ForEachPattern : byte
    {
        Horizontal, // default
        Horizontal_Alternately,

        Vertical,
        //Vertical_Alternately,

        Diagonal_UpDown,
        Diagonal_DownUp,

        //AntiClockwise
    }
}
