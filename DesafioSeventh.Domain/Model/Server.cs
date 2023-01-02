using DesafioSeventh.Domain.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioSeventh.Domain.Model
{
	[Table("servers")]
	public class Server
	{

		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime ModifiedAt { get; set; } = DateTime.Now;

		[Required]
		[StringLength(50)]
		public string Name { get; set; } = null;

		[Required]
		[RegularExpression(Config.REGEX_IP)]
		public string IP { get; set; } = null;

		[Required]
		public int Port { get; set; }

		public virtual IEnumerable<Video> Videos { get; set; } = null;
	}
}
