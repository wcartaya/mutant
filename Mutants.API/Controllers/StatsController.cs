using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mutants.Business.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("Stats", "Estadisticas de reclutamiento")]
    [Route("stats")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IMutantService _mutantService;

        public StatsController(IMutantService mutantService)
        {
            _mutantService = mutantService;
        }

        // GET: /stats/
        /// <summary>
        /// Estadísticas de las verificaciones de ADN
        /// </summary>
        /// <remarks>
        /// count_mutant_dna: cantidad de mutantes
        /// count_human_dna: cantidad de humanos
        /// ratio: proporción entre mutantes y humanos
        /// </remarks>
        /// <response code="200">Ejemplo de respuesta: {“count_mutant_dna”:40, “count_human_dna”:100: “ratio”:0.4}</response>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await _mutantService.GetStats());
        }

    }
}
