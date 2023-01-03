using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using DesafioSeventh.Web.Helpers;

namespace DesafioSeventh.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ServersController : ControllerBase
	{
		private readonly IServerDomain domain;

		public ServersController(IServerDomain domain)
		{
			this.domain = domain;
		}

		/// <summary>
		/// Obtér a lista de servidores cadastrados
		/// </summary>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(204, Type = typeof(void))]
		public IEnumerable<Server> Get() => domain.Get();

		/// <summary>
		/// Obter um servidor por Id
		/// </summary>
		/// <param name="id">Identificação do servidor</param>
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404, Type = typeof(WebException))]
		public Server Get(Guid id) => domain.Get(id);

		/// <summary>
		/// Criar um novo servidor
		/// </summary>
		/// <param name="server">Dados do Servidor</param>
		/// <returns>Servidor criado</returns>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(409, Type = typeof(WebException))]
		[ProducesResponseType(400, Type = typeof(WebException))]
		public Server Create([FromBody] ServerCreate server)
		{
			var result = domain.Create(server);
			Response.StatusCode = 201;
			return result;
		}

		/// <summary>
		/// Altera os dados de um servidor cadastrados
		/// </summary>
		/// <param name="id">Id do servidor cadastrado</param>
		/// <param name="server">Dados do servidor atualizado</param>
		/// <returns>Servidor atualizado</returns>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404, Type = typeof(WebException))]
		[ProducesResponseType(400, Type = typeof(WebException))]
		public Server Update(Guid id, [FromBody] ServerUpdate server) => domain.Update(id, server);

		/// <summary>
		/// Remove um servidor
		/// </summary>
		/// <param name="id">Id do servidor</param>
		/// <returns>Servidor removido</returns>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404, Type = typeof(WebException))]
		public Server Delete(Guid id) => domain.Remove(id);

		/// <summary>
		/// Verifica se o servidor (IP + PORT) está acessível
		/// </summary>
		/// <param name="id">Id do servidor</param>
		/// <returns>Status do servidor</returns>
		[HttpGet("available/{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404, Type = typeof(WebException))]
		public RecycleStatus Available(Guid id) => domain.ServerStatus(id);
	}
}