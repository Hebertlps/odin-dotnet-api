using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Services;

namespace OdinApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SatelitesController : ControllerBase
    {
        private readonly ISateliteService _service;
        private readonly ILogger<SatelitesController> _logger;

        public SatelitesController(ISateliteService service, ILogger<SatelitesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Obter todos os satélites
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Satelite>>> GetAll()
        {
            _logger.LogInformation("Obtendo todos os satélites");
            var satelites = await _service.GetAllAsync();
            return Ok(satelites);
        }

        /// <summary>
        /// Obter satélite por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Satelite>> GetById(int id)
        {
            _logger.LogInformation($"Obtendo satélite com ID {id}");
            var satelite = await _service.GetByIdAsync(id);
            if (satelite == null)
                return NotFound(new { message = $"Satélite com ID {id} não encontrado" });
            return Ok(satelite);
        }

        /// <summary>
        /// Criar novo satélite
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Satelite>> Create([FromBody] Satelite satelite)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation($"Criando novo satélite: {satelite.Nome}");
            var created = await _service.CreateAsync(satelite);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualizar satélite
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Satelite>> Update(int id, [FromBody] Satelite satelite)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation($"Atualizando satélite com ID {id}");
            try
            {
                var updated = await _service.UpdateAsync(id, satelite);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Satélite com ID {id} não encontrado" });
            }
        }

        /// <summary>
        /// Deletar satélite
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deletando satélite com ID {id}");
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = $"Satélite com ID {id} não encontrado" });
            return NoContent();
        }
    }
}
