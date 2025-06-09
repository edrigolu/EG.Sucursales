using EG.Sucursales.Application.DTOs;
using EG.Sucursales.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EG.Sucursales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SucursalController : Controller
    {
        private readonly ISucursalService _sucursalService;

        public SucursalController(ISucursalService sucursalService)
        {
            _sucursalService = sucursalService;
        }

        /// <summary>
        /// Lista todas las sucursales registradas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SucursalDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var sucursales = await _sucursalService.ListarAsync();
            return Ok(sucursales);
        }

        /// <summary>
        /// Obtiene una sucursal por su ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(SucursalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var sucursal = await _sucursalService.ObtenerPorIdAsync(id);
            if (sucursal == null)
                return NotFound();
            return Ok(sucursal);
        }

        /// <summary>
        /// Crea una nueva sucursal.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] SucursalDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existe = await _sucursalService.ExisteCodigoAsync(dto.Codigo);
            if (existe)
                return BadRequest($"Ya existe una sucursal con el código {dto.Codigo}");

            var id = await _sucursalService.CrearAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        /// <summary>
        /// Actualiza una sucursal existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] SucursalDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = await _sucursalService.ActualizarAsync(dto);
            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Elimina una sucursal por su ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _sucursalService.EliminarAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Verifica si ya existe una sucursal con el código especificado.
        /// </summary>
        [HttpGet("existe-codigo/{codigo:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExisteCodigo(int codigo)
        {
            var existe = await _sucursalService.ExisteCodigoAsync(codigo);
            return Ok(existe);
        }
    }
}
