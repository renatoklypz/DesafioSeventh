using DesafioSeventh.Domain.Global;
using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioSeventh.Domain.ViewModel
{
	public class ServerCreate: ServerUpdate
	{
		/// <summary>
		/// Ao não definir o "Id" o mesmo será gerado automaticamente
		/// </summary>
		public Guid? Id { get; set; } = null;
	}
}
