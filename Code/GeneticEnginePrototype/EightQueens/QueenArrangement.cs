using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightQueens
{
    public class QueenArrangement
    {
        int[] queenColumns = new int[8];

        public QueenArrangement()
        {
        }

        public QueenArrangement(QueenArrangement queenArrangement)
        {
            Array.Copy(queenArrangement.queenColumns, queenColumns, 8);
        }

        public void SetQueenColumn(int row, int column)
        {
            queenColumns[row] = column;
        }

        public int GetQueenColumn(int row)
        {
            return queenColumns[row];
        }

        override
        public string ToString()
        {
            string result = "";

            for (int i = 0; i < 8; i++)
            {                
                int c = queenColumns[i];

                result += "+-+-+-+-+-+-+-+-+\n";

                for (int j = 0; j < c; j++)
                {
                    result += "| ";
                }

                result += "|*";

                for (int j = c + 1; j < 8; j++)
                {
                    result += "| ";
                }

                result += "|\n";
            }

            result += "+-+-+-+-+-+-+-+-+\n";

            return result;
        }
    }
}
