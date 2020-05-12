using Mutants.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mutant.Validators
{
    /// <summary>
    /// Verifica si hay secuencias de manera oblicua (diagonales positivas).
    /// </summary>
    public class PositiveDiagonalSecuence : ISecuenceValidator
    {
        public async Task<int> CountAllSecuence(string[] dna)
        {
            int countSecuence = 0;
            Task<int> CountPositiveDiagonalSecuencePTask = CountPositiveDiagonalSecuence(dna, 0, 0);
            for (int index = 1; index < MutantConstants.MatrixLen / 2 && countSecuence < 2; ++index)
            {
                Task<int> CountPositiveDiagonalSecuenceHTask = CountPositiveDiagonalSecuence(dna, 0, index);
                Task<int> CountPositiveDiagonalSecuenceVTask = CountPositiveDiagonalSecuence(dna, index, 0);
                countSecuence += await CountPositiveDiagonalSecuenceHTask + await CountPositiveDiagonalSecuenceVTask;
            }
            return countSecuence += await CountPositiveDiagonalSecuencePTask;
        }

        /// <summary>
        /// Verifica si hay 4 letras seguidas en una diagonal.
        /// </summary>
        private async Task<int> CountPositiveDiagonalSecuence(string[] dna, int row, int col)
        {
            int max = MutantConstants.MatrixLen - Math.Max(row, col);
            int countLetter = 1;
            char lastLetter = dna[row][col];
            for (int index = 1;
                index < max &&
                countLetter < MutantConstants.CantSecuence &&
                max - index + countLetter >= MutantConstants.CantSecuence;
                ++index)
            {
                countLetter = lastLetter == dna[row + index][col + index] ? ++countLetter : 1;
                lastLetter = dna[row + index][col + index];
            }
            return countLetter >= MutantConstants.CantSecuence ? 1 : 0;
        }
    }
}
