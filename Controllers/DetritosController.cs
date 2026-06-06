using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Services;

namespace OdinApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DetritosController : ControllerBase
    {
        private readonly IDebitoService _service;
        private readonly ILogger<DetritosController> _logger;

        public DetritosController(IDebitoService service, ILogger<DetritosController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Obter todos os detritos
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Detrito>>> GetAll()
        {
            _logger.LogInformation("Obtendo todos os detritos");
            var detritos = await _service.GetAllAsync();
            return Ok(detritos);
        }

        /// <summary>
        /// Obter detrito por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Detrito>> GetById(int id)
        {
            _logger.LogInformation($"Obtendo detrito com ID {id}");
            var detrito = await _service.GetByIdAsync(id);
            if (detrito == null)
                return NotFound(new { message = $"Detrito com ID {id} não encontrado" });
            return Ok(detrito);
        }

        /// <summary>
        /// Criar novo detrito
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Detrito>> Create([FromBody] Detrito detrito)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation($"Criando novo detrito: {detrito.Identificacao}");
            var created = await _service.CreateAsync(detrito);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualizar detrito
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Detrito>> Update(int id, [FromBody] Detrito detrito)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation($"Atualizando detrito com ID {id}");
            try
            {
                var updated = await _service.UpdateAsync(id, detrito);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Detrito com ID {id} não encontrado" });
            }
        }

        /// <summary>
        /// Deletar detrito
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deletando detrito com ID {id}");
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = $"Detrito com ID {id} não encontrado" });
            return NoContent();
        }
    }
}
