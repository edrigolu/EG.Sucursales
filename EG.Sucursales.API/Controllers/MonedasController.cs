using EG.Sucursales.Application.DTOs;
using EG.Sucursales.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EG.Sucursales.API.Controllers
{
    public class MonedasController : Controller
    {
        private readonly IMonedaService _monedaService;

        public MonedasController(IMonedaService monedaService)
        {
            _monedaService = monedaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var monedas = await _monedaService.ListarTodasAsync();
            return Ok(monedas);
        }

        [HttpGet("activas")]
        public async Task<IActionResult> GetActivas()
        {
            var monedas = await _monedaService.ListarActivasAsync();
            return Ok(monedas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var moneda = await _monedaService.ObtenerPorIdAsync(id);
            if (moneda == null)
                return NotFound();
            return Ok(moneda);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MonedaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _monedaService.CrearAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MonedaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest("El ID no coincide");

            var success = await _monedaService.ActualizarAsync(dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _monedaService.EliminarAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
