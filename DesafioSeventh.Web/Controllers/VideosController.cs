using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using WebException = DesafioSeventh.Web.Helpers.WebException;

namespace DesafioSeventh.Web.Controllers
{
	/// <summary>
	/// Manutenção de vídeos
	/// </summary>
	[ApiController]
	[Route("api/servers/{serverId}/[controller]")]
	public class VideosController : ControllerBase
	{
		private readonly IVideoDomain domain;

		public VideosController(IVideoDomain domain)
		{
			this.domain = domain;
		}

		/// <summary>
		/// Obtém a lista de vídeos de um servidor
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		/// <returns>Lista de vídeos</returns>
		[HttpGet]
		[SwaggerResponse(200)]
		public IEnumerable<Video> Get(Guid serverId)
		{
			return domain.Get(serverId);
		}

		/// <summary>
		/// Obtém um vídeo
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		/// <param name="videoId">Id do vídeo</param>
		///<returns>Informações de um vídeo</returns>
		[HttpGet("{videoId}")]
		[SwaggerResponse(200, "Sucesso ao obter um vídeo")]
		[SwaggerResponse(404, "Vídeo  não encontrado", Type = typeof(WebException))]
		public Video Get(Guid serverId, Guid videoId)
		{
			return domain.Get(serverId, videoId);
		}

		/// <summary>
		/// Obtém um arquivo de vídeo (Download)
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		/// <param name="videoId">Id do vídeo</param>
		/// <returns>Binário de vídeo</returns>
		[HttpGet("{videoId}/Binary")]
		[SwaggerResponse(200, "Sucesso ao obter um vídeo", typeof(void))]
		[SwaggerResponse(404, "Vídeo  não encontrado", Type = typeof(WebException))]
		public IActionResult GetDownload(Guid serverId, Guid videoId)
		{
			var down = domain.GetBinary(serverId, videoId, out string fileName);
			var file = File(down, "application/octet-stream");
			file.FileDownloadName = fileName;

			return file;
		}

		/// <summary>
		/// Cria um Vídeo
		/// </summary>
		/// <param name="serverId">Id do Servidor</param>
		/// <param name="videoInfo">Informações de Vídeo + Binário</param>
		/// <returns>Informação de Vídeo criado</returns>
		[HttpPost]
		[SwaggerResponse(201, "Sucesso ao criar um vídeo")]
		[SwaggerResponse(400, "Erro de validação", Type = typeof(WebException))]
		[SwaggerResponse(404, "Vídeo  não encontrado", Type = typeof(WebException))]
		public Video Create(Guid serverId, [FromForm] VideoViewModel videoInfo)
		{
			var file = Request.Form?.Files["binary"];

			var result = domain.Create(serverId, videoInfo, file.OpenReadStream(), file.FileName.Split('.').Last());
			Response.StatusCode = (int)HttpStatusCode.Created;
			return result;
		}

		/// <summary>
		/// Apaga um vídeo
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		/// <param name="videoId">Id do vídeo</param>
		/// <returns>Vídeo Apagado</returns>
		[HttpDelete("{videoId}")]
		[SwaggerResponse(200, "Sucesso ao apagar um vídeo")]
		[SwaggerResponse(404, "Vídeo  não encontrado", Type = typeof(WebException))]
		public Video Delete(Guid serverId, Guid videoId)
		{
			return domain.Delete(serverId, videoId);
		}
	}
}