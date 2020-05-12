using Mutants.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mutant.Validators
{
    /// <summary>
    /// Verifica si hay secuencias de manera lineal.
    /// </summary>
    public class LinealSecuence : ISecuenceValidator
    {
        public async Task<int> CountAllSecuence(string[] dna)
        {
            int countSecuence = 0;
            for (int index = 0; index < MutantConstants.MatrixLen && countSecuence < 2; ++index)
            {
                Task<int> CountVerticalSecuenceTask = CountVerticalSecuence(dna, index);
                Task<int> CountHorizontalSecuenceTask = CountHorizontalSecuence(dna, index);
                countSecuence += await CountVerticalSecuenceTask + await CountHorizontalSecuenceTask;
            }
            return countSecuence;
        }
        /// <summary>
        /// Verifica si hay secuencias de manera horizontal.
        /// </summary>
        private async Task<int> CountHorizontalSecuence(string[] dna, int row)
        {
            int countLetter = 1;
            char lastLetter = dna[row][0];
            for (var index = 1;
                index < MutantConstants.MatrixLen   //El indice está dentro del tamaño permitido
                && countLetter < MutantConstants.CantSecuence  //Aún no se completa la secuencia requerida
                && MutantConstants.MatrixLen - index + countLetter >= MutantConstants.CantSecuence;  // Aún hay oportunidad de completar la secuencia
                index++)
            {
                countLetter = lastLetter == dna[row][index] ? ++countLetter : 1;
                lastLetter = dna[row][index];
            }
            return countLetter >= MutantConstants.CantSecuence ? 1 : 0;
        }
        /// <summary>
        /// Verifica si hay secuencias de manera vertical.
        /// </summary>
        private async Task<int> CountVerticalSecuence(string[] dna, int col)
        {
            //TODO: Cómo es igual que el horizontal se debería implementar en un sólo método que invierta filas * columnas
            int countLetter = 1;
            char lastLetter = dna[0][col];
            for (var index = 1;
                index < MutantConstants.MatrixLen &&
                countLetter < MutantConstants.CantSecuence &&
                MutantConstants.MatrixLen - index + countLetter >= MutantConstants.CantSecuence;
                index++)
            {
                countLetter = lastLetter == dna[index][col] ? ++countLetter : 1;
                lastLetter = dna[index][col];
            }
            return countLetter >= MutantConstants.CantSecuence ? 1 : 0;
        }
    }
}
