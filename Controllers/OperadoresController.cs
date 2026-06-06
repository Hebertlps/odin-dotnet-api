using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Services;

namespace OdinApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OperadoresController : ControllerBase
    {
        private readonly IOperadorService _service;
        private readonly ILogger<OperadoresController> _logger;

        public OperadoresController(IOperadorService service, ILogger<OperadoresController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Obter todos os operadores
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Operador>>> GetAll()
        {
            _logger.LogInformation("Obtendo todos os operadores");
            var operadores = await _service.GetAllAsync();
            return Ok(operadores);
        }

        /// <summary>
        /// Obter operador por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Operador>> GetById(int id)
        {
            _logger.LogInformation($"Obtendo operador com ID {id}");
            var operador = await _service.GetByIdAsync(id);
            if (operador == null)
                return NotFound(new { message = $"Operador com ID {id} não encontrado" });
            return Ok(operador);
        }

        /// <summary>
        /// Criar novo operador
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Operador>> Create([FromBody] Operador operador)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation($"Criando novo operador: {operador.Nome}");
            var created = await _service.CreateAsync(operador);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualizar operador
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Operador>> Update(int id, [FromBody] Operador operador)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation($"Atualizando operador com ID {id}");
            try
            {
                var updated = await _service.UpdateAsync(id, operador);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Operador com ID {id} não encontrado" });
            }
        }

        /// <summary>
        /// Deletar operador
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deletando operador com ID {id}");
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = $"Operador com ID {id} não encontrado" });
            return NoContent();
        }
    }
}
