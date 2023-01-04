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
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime ModifiedAt { get; set; } = DateTime.Now;
		[StringLength(255), Required]
		public string Description { get; set; } = null;
		public long SizeInBytes { get; set; } = 0;
		public Guid ServerId { get; set; }

		[ForeignKey("ServerId")]
		public virtual Server Server { get; set; } = null;
	}
}
