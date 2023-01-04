using DesafioSeventh.Domain;
using DesafioSeventh.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioSeventh.Web.Controllers
{
	/// <summary>
	/// Remover conteúdos antigos
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class RecyclerController : ControllerBase
	{

		private readonly IRecycleVideoDomain recycleVideo;

		public RecyclerController(IRecycleVideoDomain recycleVideo)
		{
			this.recycleVideo = recycleVideo;
		}

		/// <summary>
		/// Aciona processo de remoção de vídeos antigos a mais de x dias
		/// </summary>
		/// <param name="days">Dias a partir de hoje. 0 = Hoje</param>
		[HttpPost("process/{days}")]
		[SwaggerResponse(202, "Sucesso ao acionar processo de remoção")]
		[SwaggerResponse(400, "Dias inválidos")]
		public void Process(int days)
		{
			recycleVideo.Recycle(days);
			Response.StatusCode = 202;
		}

		/// <summary>
		/// Obtém status de processo de remoção.
		/// </summary>
		/// <returns></returns>
		[HttpGet("status")]
		[SwaggerResponse(200, "Sucesso ao obter status do processo")]
		public RecycleStatus Status() => recycleVideo.Status;
	}
}