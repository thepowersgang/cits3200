using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

namespace EightQueens
{
    public class QueenEvaluator : IEvaluator
    {
        public void Initialise(int generationCount, ArrayList individuals) { }

        public uint Evaluate(object individual)
        {
            int[] columns = new int[8];
            int[] diagonalsUp = new int[15];
            int[] diagonalsDown = new int[15];

            int attacks = 0;

            QueenArrangement qa = (QueenArrangement)individual;

            for (int row = 0; row < 8; row++)
            {
                int column = qa.GetQueenColumn(row);
                int diagonalUp = row + column;
                int diagonalDown = row + 7 - column;

                attacks += (columns[column]++) + (diagonalsUp[diagonalUp]++) + (diagonalsDown[diagonalDown]++);
            }

            return (uint)(30-attacks);
        }
    }
}
