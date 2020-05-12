using Mutants.Common.Constants;
using System;
using System.Threading.Tasks;

namespace Mutants.Business.Mutant
{
    /// <summary>
    /// Valida si un humano es mutante por su cadena de adn
    /// </summary>
    /// <remarks>
    /// Se recibe un arreglo de string que representa cada fila de una tabla (NxN) con la secuencia de ADN.
    /// Las letras de los Strings solo pueden ser: (A,T,C,G) que representan cada base nitrogenada del ADN.
    /// Se sabe si un humano es mutante, si se encuentra más de una secuencia de cuatro letras iguales ,
    ///  de forma oblicua, horizontal o vertical.
    /// </remarks>
    /// <param name="dna">Arreglo de string que representa el adn (6x6).</param>
    /// <response bool>True or False</response> 
    public class DnaValidator
    {
        private int countAllSecuences;
        /// <summary>
        /// Realiza llamadas de manera simultanea a diferentes tareas para validar si encuentra secuencia de 4 letras seguidas iguales
        /// </summary>
        public async Task<bool> IsMutant(string[] dna)
        {
            //TODO:  Analizar: se puede modificar countAllSecuences en cada método para que salga antes si llega a 2 secuencias y que sean de tipo void ?
            countAllSecuences = 0;
            Task<int> linealSecuenceTask = CountAllLinealSecuence(dna);
            Task<int> positiveDiagonalSecuenceTask = CountAllPositiveDiagonalSecuence(dna);
            Task<int> negativeDiagonalSecuenceTask = CountAllNegativeDiagonalSecuence(dna);
            return (
                await linealSecuenceTask +
                await positiveDiagonalSecuenceTask + 
                await negativeDiagonalSecuenceTask
                ) > 1;
        }
        #region LinealSecuence
        /// <summary>
        /// Verifica si hay secuencias de manera vertical u horizontal. Las llamadas son en paralelo
        /// </summary>
        private async Task<int> CountAllLinealSecuence(string[] dna)
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
            return countLetter >= MutantConstants.CantSecuence? 1 : 0;
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
        #endregion
        #region PositiveDiagonal
        /// <summary>
        /// Verifica si hay secuencias en diagonales positivas. Las llamadas son en paralelo
        /// </summary>
        private async Task<int> CountAllPositiveDiagonalSecuence(string[] dna)
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
        private async Task<int> CountPositiveDiagonalSecuence(string[] dna,int row, int col)
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
                countLetter = lastLetter == dna[row+index][col+index]? ++countLetter : 1;
                lastLetter = dna[row + index][col + index];
            }
            return countLetter >= MutantConstants.CantSecuence ? 1 : 0;
        }
        #endregion
        #region NegativeDiagonal
        /// <summary>
        /// Verifica si hay secuencias en diagonales negativas. Las llamadas son en paralelo
        /// </summary>
        private async Task<int> CountAllNegativeDiagonalSecuence(string[] dna)
        {
            int countSecuence = 0;
            Task<int> CountNegativeDiagonalSecuencePTask = CountNegativeDiagonalSecuence(dna, 0, MutantConstants.MatrixLen-1);
            for (int index = 1; index < MutantConstants.MatrixLen / 2 && countSecuence < 2; ++index)
            {
                Task<int> CountNegativeDiagonalSecuenceHTask = CountNegativeDiagonalSecuence(dna, 0, MutantConstants.MatrixLen-1-index);
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
        #endregion
    }
}
