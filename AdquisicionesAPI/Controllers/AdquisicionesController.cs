using Microsoft.AspNetCore.Mvc;
using AdquisicionesAPI.Models;
using System.Collections.Generic;
using System.Linq;


namespace AdquisicionesAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AdquisicionesController : ControllerBase
	{
		private static List<Adquisicion> adquisiciones = new List<Adquisicion>();

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(adquisiciones);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var adquisicion = adquisiciones.FirstOrDefault(a => a.Id == id);
			if (adquisicion == null)
				return NotFound();
			return Ok(adquisicion);
		}

		[HttpPost]
		public IActionResult Post([FromBody] Adquisicion nuevaAdquisicion)
		{
			nuevaAdquisicion.Id = adquisiciones.Count + 1;
			adquisiciones.Add(nuevaAdquisicion);
			return CreatedAtAction(nameof(GetById), new { id = nuevaAdquisicion.Id }, nuevaAdquisicion);
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] Adquisicion actualizacion)
		{
			var adquisicion = adquisiciones.FirstOrDefault(a => a.Id == id);
			if (adquisicion == null)
				return NotFound();

			adquisicion.Presupuesto = actualizacion.Presupuesto;
			adquisicion.Unidad = actualizacion.Unidad;
			adquisicion.Tipo = actualizacion.Tipo;
			adquisicion.Cantidad = actualizacion.Cantidad;
			adquisicion.ValorUnitario = actualizacion.ValorUnitario;
			adquisicion.Fecha = actualizacion.Fecha;
			adquisicion.Proveedor = actualizacion.Proveedor;
			adquisicion.Documentacion = actualizacion.Documentacion;

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var adquisicion = adquisiciones.FirstOrDefault(a => a.Id == id);
			if (adquisicion == null)
				return NotFound();

			adquisiciones.Remove(adquisicion);
			return NoContent();
		}
	}
}
