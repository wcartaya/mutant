using Business.Mutant.Validators;
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
    public class DnaValidator : IDnaValidator
    {
        /// <summary>
        /// Realiza llamadas de manera simultanea a diferentes tareas para validar si encuentra secuencia de 4 letras seguidas iguales
        /// </summary>
        public async Task<bool> IsMutant(string[] dna)
        {
            SecuenceValidator secuenceValidator;

            secuenceValidator = new SecuenceValidator(new LinealSecuence());
            Task<int> linealSecuenceTask = secuenceValidator.CountAllSecuence(dna);

            secuenceValidator = new SecuenceValidator(new PositiveDiagonalSecuence());
            Task<int> positiveDiagonalSecuenceTask = secuenceValidator.CountAllSecuence(dna);

            secuenceValidator = new SecuenceValidator(new NegativeDiagonalSecuence());
            Task<int> negativeDiagonalSecuenceTask = secuenceValidator.CountAllSecuence(dna);

            return (
                await linealSecuenceTask +
                await positiveDiagonalSecuenceTask + 
                await negativeDiagonalSecuenceTask
                ) > 1;
        }
    }
}
