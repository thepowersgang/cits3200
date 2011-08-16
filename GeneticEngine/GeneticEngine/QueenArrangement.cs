using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    class QueenArrangement
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

        public void print()
        {
            for (int i = 0; i < 8; i++)
            {
                int c = queenColumns[i];

                Console.WriteLine("+-+-+-+-+-+-+-+-+");

                for (int j = 0; j < c; j++)
                {
                    Console.Write("| ");
                }

                Console.Write("|*");

                for (int j = c+1; j < 8; j++)
                {
                    Console.Write("| ");
                }

                Console.WriteLine("|");
            }

            Console.WriteLine("+-+-+-+-+-+-+-+-+");
        }
    }
}
