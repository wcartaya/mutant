using Mutants.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mutant.Validators
{
    /// <summary>
    /// Verifica si hay secuencias de manera oblicua (diagonales negativas).
    /// </summary>
    public class NegativeDiagonalSecuence : ISecuenceValidator
    {
        public async Task<int> CountAllSecuence(string[] dna)
        {
            int countSecuence = 0;
            Task<int> CountNegativeDiagonalSecuencePTask = CountNegativeDiagonalSecuence(dna, 0, MutantConstants.MatrixLen - 1);
            for (int index = 1; index < MutantConstants.MatrixLen / 2 && countSecuence < 2; ++index)
            {
                Task<int> CountNegativeDiagonalSecuenceHTask = CountNegativeDiagonalSecuence(dna, 0, MutantConstants.MatrixLen - 1 - index);
                Task<int> CountNegativeDiagonalSecuenceVTask = CountNegativeDiagonalSecuence(dna, index, MutantConstants.MatrixLen - 1);
                countSecuence += await CountNegativeDiagonalSecuenceHTask + await CountNegativeDiagonalSecuenceVTask;
            }
            return countSecuence += await CountNegativeDiagonalSecuencePTask;
        }

        /// <summary>
        /// Verifica si hay 4 letras seguidas en una diagonal.
        /// </summary>
        private async Task<int> CountNegativeDiagonalSecuence(string[] dna, int row, int col)
        {
            int max = Math.Abs(row - col);
            int countLetter = 1;
            char lastLetter = dna[row][col];
            for (int index = 1;
                index <= max &&
                countLetter < MutantConstants.CantSecuence &&
                max - index + countLetter >= MutantConstants.CantSecuence;
                ++index)
            {
                countLetter = lastLetter == dna[row + index][col - index] ? ++countLetter : 1;
                lastLetter = dna[row + index][col - index];
            }
            return countLetter >= MutantConstants.CantSecuence ? 1 : 0;
        }
    }
}
