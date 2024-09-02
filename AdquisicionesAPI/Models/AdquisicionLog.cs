namespace AdquisicionesAPI.Models
{
	public class AdquisicionLog
	{
		public int Id { get; set; }
		public int AdquisicionId { get; set; }
		public DateTime FechaCambio { get; set; }
		public string TipoCambio { get; set; } = string.Empty;
		public string DetallesCambio { get; set; } = string.Empty;
		public string Usuario { get; set; } = string.Empty;
	}
}
