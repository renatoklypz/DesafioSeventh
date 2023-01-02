using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioSeventh.Domain.Model
{
	[Table("videos")]
	public class Video
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime CriadoEm { get; set; } = DateTime.Now;
		[StringLength(255), Required]
		public string Description { get; set; } = null;
		public int? SizeInBytes { get; set; } = null;
		public Guid ServidorId { get; set; }

		[ForeignKey("ServidorId")]
		public virtual Server Servidor { get; set; } = null;
	}
}
