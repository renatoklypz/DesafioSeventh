using DesafioSeventh.Domain.Global;
using System.ComponentModel.DataAnnotations;

namespace DesafioSeventh.Domain.ViewModel
{
	public class ServerUpdate
	{

		[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = Config.VAL_REQUIRED)]
		[StringLength(50, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = Config.VAL_STRING_LENGTH)]
		public string Name { get; set; } = null;

		[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = Config.VAL_REQUIRED)]
		[StringLength(20, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = Config.VAL_STRING_LENGTH)]
		[RegularExpression(Config.REGEX_IP, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = Config.VAL_IPVALIDATION)]
		public string IP { get; set; } = null;

		[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = Config.VAL_REQUIRED)]
		public int? Port { get; set; } = null;
	}
}
