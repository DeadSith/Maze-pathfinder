using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeePathfinderLib
{
    public struct Cell
    {
        public int Column;

        public int Row;

        public Cell(Cell input)
        {
            Row = input.Row;
            Column = input.Column;
        }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return $"{{{Row} {Column}}} ";
        }
    }
}
