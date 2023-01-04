using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using DesafioSeventh.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioSeventh.Web.Controllers
{
	/// <summary>
	/// Manutenção de Servidor
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	[SwaggerDiscriminator("Manutenção de servidor")]
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
		[SwaggerResponse(200)]
		public IEnumerable<Server> Get() => domain.Get();

		/// <summary>
		/// Obter um servidor por Id
		/// </summary>
		/// <param name="id">Identificação do servidor</param>
		[HttpGet("{id}")]
		[SwaggerResponse(200, "Sucesso ao obter um servidor")]
		[SwaggerResponse(404, "Servidor não encontrado",Type = typeof(WebException))]
		public Server Get(Guid id) => domain.Get(id);

		/// <summary>
		/// Criar um novo servidor
		/// </summary>
		/// <param name="server">Dados do Servidor</param>
		/// <returns>Servidor criado</returns>
		[HttpPost]
		[SwaggerResponse(201, "Servidor criado com sucesso")]
		[SwaggerResponse(409, "Id informado já existe. (Opte por não preencher o Id do Servidor)", Type = typeof(WebException))]
		[SwaggerResponse(400, "Erro de preenchimento", Type = typeof(WebException))]
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
		[SwaggerResponse(200, "Sucesso ao alterar um servidor")]
		[SwaggerResponse(404, "Servidor não encontrado", Type = typeof(WebException))]
		[SwaggerResponse(400, "Erro de preenchimento", Type = typeof(WebException))]
		public Server Update(Guid id, [FromBody] ServerUpdate server) => domain.Update(id, server);

		/// <summary>
		/// Remove um servidor
		/// </summary>
		/// <param name="id">Id do servidor</param>
		/// <returns>Servidor removido</returns>
		[HttpDelete("{id}")]
		[SwaggerResponse(200, "Sucesso ao apagar um servidor")]
		[SwaggerResponse(404, "Servidor não encontrado", Type = typeof(WebException))]
		public Server Delete(Guid id) => domain.Delete(id);

		/// <summary>
		/// Verifica se o servidor (IP + PORT) está acessível
		/// </summary>
		/// <param name="id">Id do servidor</param>
		/// <returns>Status do servidor</returns>
		[HttpGet("available/{id}")]
		[SwaggerResponse(200, "Sucesso ao obter o status do servidor")]
		[SwaggerResponse(404, "Servidor não encontrado", Type = typeof(WebException))]
		public RecycleStatus Available(Guid id) => domain.ServerStatus(id);
	}
}