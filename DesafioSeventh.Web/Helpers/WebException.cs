namespace DesafioSeventh.Web.Helpers
{
	public class WebException
	{
		public string? Code { get; set; }
		public string? Message { get; set; }
		public object? Report { get; internal set; }
	}
}
