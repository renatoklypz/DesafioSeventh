using System;

namespace DesafioSeventh.Domain.Model
{
	public class Video
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime CriadoEm { get; set; } = DateTime.Now;
		public string Description { get; set; } = null;
		public int? SizeInBytes { get; set; } = null;
		public Guid ServidorId { get; set; }

		public virtual Server Servidor { get; set; } = null;
	}
}
