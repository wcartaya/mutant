using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mutants.Business.Dtos;
using Mutants.Business.IServices;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Mutants.Controllers
{
    [SwaggerTag("mutant", "Operaciones con mutantes.")]
    [Route("mutant")]
    [ApiController]
    public class MutantsController : ControllerBase
    {
        private readonly IMutantService _mutantService;
        public MutantsController(IMutantService mutantService)
        {
            _mutantService = mutantService;
        }

        // POST: /mutant/
        /// <summary>
        /// Valida si un humano es mutante por su cadena de adn
        /// </summary>
        /// <remarks>
        /// Se recibe un arreglo de string que representa cada fila de una tabla (NxN) con la secuencia de ADN.
        /// Las letras de los Strings solo pueden ser: (A,T,C,G) que representan cada base nitrogenada del ADN.
        /// Se sabe si un humano es mutante, si se encuentra más de una secuencia de cuatro letras iguales ,
        ///  de forma oblicua, horizontal o vertical.
        /// </remarks>
        /// <param name="dna">Arreglo de string que representa el adn (6x6).
        /// Ejemplo: 
        /// {
        /// "dna":["ATGGGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]
        /// }
        /// </param>
        /// <response code="403">Forbidden. No es mutante.</response>              
        /// <response code="200">Ok. Es mutante.</response>        
        /// <response code="400">BadRequest. Formato del objeto incorrecto.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] DnaDto dna)
        {            
            if (this.ModelState.IsValid)
            {
                if (await _mutantService.IsMutant(dna.dna))
                {
                    return Ok();
                }
                return StatusCode(403);
            }
            return BadRequest(ModelState);
        }
    }
}
