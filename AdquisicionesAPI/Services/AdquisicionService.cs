using AdquisicionesAPI.Models;
using System.Text;

namespace AdquisicionesAPI.Services
{
	public class AdquisicionService
	{
		private readonly List<Adquisicion> _adquisiciones = new List<Adquisicion>();
		private int _nextId = 1;
		private readonly AdquisicionLogService _adquisicionLogService;

		public AdquisicionService(AdquisicionLogService adquisicionLogService)
		{
			_adquisicionLogService = adquisicionLogService;
		}

		public IEnumerable<Adquisicion> GetAll() => _adquisiciones;

		public Adquisicion? Get(int id) => _adquisiciones.FirstOrDefault(a => a.Id == id);

		public void Add(Adquisicion adquisicion)
		{
			adquisicion.Id = _nextId++;
			_adquisiciones.Add(adquisicion);

			var log = new AdquisicionLog
			{
				AdquisicionId = adquisicion.Id,
				FechaCambio = DateTime.Now,
				TipoCambio = "Creación",
				DetallesCambio = "",
				Usuario = "UsuarioActual"
			};
			_adquisicionLogService.Add(log);
		}

		public void Update(Adquisicion adquisicion)
		{
			var index = _adquisiciones.FindIndex(a => a.Id == adquisicion.Id);
			if (index != -1)
			{
				var adquisicionOriginal = _adquisiciones[index];
				var detallesCambio = new StringBuilder();

				if (adquisicionOriginal.Presupuesto != adquisicion.Presupuesto)
				{
					detallesCambio.AppendLine($"Presupuesto: de '{adquisicionOriginal.Presupuesto}' a '{adquisicion.Presupuesto}'");
				}
				if (adquisicionOriginal.Unidad != adquisicion.Unidad)
				{
					detallesCambio.AppendLine($"Unidad: de '{adquisicionOriginal.Unidad}' a '{adquisicion.Unidad}'");
				}
				if (adquisicionOriginal.Tipo != adquisicion.Tipo)
				{
					detallesCambio.AppendLine($"Tipo: de '{adquisicionOriginal.Tipo}' a '{adquisicion.Tipo}'");
				}
				if (adquisicionOriginal.Cantidad != adquisicion.Cantidad)
				{
					detallesCambio.AppendLine($"Cantidad: de '{adquisicionOriginal.Cantidad}' a '{adquisicion.Cantidad}'");
				}
				if (adquisicionOriginal.ValorUnitario != adquisicion.ValorUnitario)
				{
					detallesCambio.AppendLine($"Valor Unitario: de '{adquisicionOriginal.ValorUnitario}' a '{adquisicion.ValorUnitario}'");
				}
				if (adquisicionOriginal.ValorTotal != adquisicion.ValorTotal)
				{
					detallesCambio.AppendLine($"Valor Total: de '{adquisicionOriginal.ValorTotal}' a '{adquisicion.ValorTotal}'");
				}
				if (adquisicionOriginal.Fecha != adquisicion.Fecha)
				{
					detallesCambio.AppendLine($"Fecha: de '{adquisicionOriginal.Fecha:yyyy-MM-dd}' a '{adquisicion.Fecha:yyyy-MM-dd}'");
				}
				if (adquisicionOriginal.Proveedor != adquisicion.Proveedor)
				{
					detallesCambio.AppendLine($"Proveedor: de '{adquisicionOriginal.Proveedor}' a '{adquisicion.Proveedor}'");
				}
				if (adquisicionOriginal.Documentacion != adquisicion.Documentacion)
				{
					detallesCambio.AppendLine($"Documentación: de '{adquisicionOriginal.Documentacion}' a '{adquisicion.Documentacion}'");
				}

				_adquisiciones[index] = adquisicion;
				var log = new AdquisicionLog
				{
					AdquisicionId = adquisicion.Id,
					FechaCambio = DateTime.Now,
					TipoCambio = "Actualización",
					DetallesCambio = detallesCambio.ToString(),
					Usuario = "UsuarioActual"
				};
				_adquisicionLogService.Add(log);
			}

		}

		public void Delete(int id)
		{
			var index = _adquisiciones.FindIndex(a => a.Id == id);
			if (index != -1)
			{
				_adquisiciones.RemoveAt(index);
			}
		}
	}
}
