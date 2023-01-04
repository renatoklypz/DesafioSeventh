using DesafioSeventh.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DesafioSeventh.Domain.ViewModel
{
	public class VideoViewModel
	{

		[StringLength(255), Required]
		public string Description { get; set; } = null;
	}
}
