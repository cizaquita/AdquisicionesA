using System;

namespace AdquisicionesAPI.Models
{
	public class Adquisicion
	{
		public int Id { get; set; }
		public string Presupuesto { get; set; } = string.Empty;
		public string Unidad { get; set; } = string.Empty;
		public string Tipo { get; set; } = string.Empty;
		public int Cantidad { get; set; }
		public decimal ValorUnitario { get; set; }
		public decimal ValorTotal => Cantidad * ValorUnitario;
		public DateTime Fecha { get; set; }
		public string Proveedor { get; set; } = string.Empty;
		public string Documentacion { get; set; } = string.Empty;
	}
}
