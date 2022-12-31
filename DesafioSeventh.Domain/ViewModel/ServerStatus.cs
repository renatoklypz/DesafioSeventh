using DesafioSeventh.Domain.Global;
using Newtonsoft.Json;

namespace DesafioSeventh.Domain.ViewModel
{
	public class ServerStatus
	{
		[JsonIgnore]
		public ServerStatusEnum StatusEnum { get; set; } = ServerStatusEnum.NotRunning;
		public string Status { get => Messages.ResourceManager.GetString(StatusEnum.ToString()); }
	}
}
