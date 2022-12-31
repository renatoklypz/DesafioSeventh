using DesafioSeventh.Domain.Global;
using System.ComponentModel.DataAnnotations;

namespace DesafioSeventh.Domain.ViewModel
{
	public class ServerUpdate
	{

		[Required]
		[StringLength(50)]
		public string Name { get; set; } = null;

		[Required]
		[RegularExpression(Config.REGEX_IP)]
		public string IP { get; set; } = null;

		[Required]
		public int? Port { get; set; } = null;
	}
}
