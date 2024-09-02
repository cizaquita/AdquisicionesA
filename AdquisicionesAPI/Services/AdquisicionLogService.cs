using AdquisicionesAPI.Models;

namespace AdquisicionesAPI.Services
{
	public class AdquisicionLogService
	{
		private readonly List<AdquisicionLog> _adquisicionesLog = new List<AdquisicionLog>();
		private int _nextId = 1;

		public IEnumerable<AdquisicionLog> GetAll() => _adquisicionesLog;
		public List<AdquisicionLog>? Get(int id) => _adquisicionesLog.Where(a => a.AdquisicionId == id).ToList();

		public void Add(AdquisicionLog adquisicionLog)
		{
			adquisicionLog.Id = _nextId++;
			_adquisicionesLog.Add(adquisicionLog);
		}
	}
}
