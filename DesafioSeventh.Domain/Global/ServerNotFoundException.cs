namespace DesafioSeventh.Domain.Global
{
	public class ServerNotFoundException : ExceptionCode
	{
		public ServerNotFoundException(string serverId) : base("server_not_found", string.Format(ErrorMap.vid_server_not_found, serverId))
		{

		}
	}
}
