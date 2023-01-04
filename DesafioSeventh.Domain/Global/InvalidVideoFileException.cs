namespace DesafioSeventh.Domain.Global
{
	public class InvalidVideoFileException : ExceptionCode
	{
		public InvalidVideoFileException(string error, params string[] args)
			: base("vid_invalid_file", string.Format(ErrorMap.vid_invalid_file, string.Format(ErrorMap.ResourceManager.GetString($"vid_invalid_file_{error}"), args)))
		{

		}
	}
}
