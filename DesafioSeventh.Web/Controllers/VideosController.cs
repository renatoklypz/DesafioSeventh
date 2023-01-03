using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace DesafioSeventh.Web.Controllers
{
	[ApiController]
	[Route("api/servers/{serverId}/[controller]")]
	public class VideosController : ControllerBase
	{
		private readonly IVideoDomain domain;

		public VideosController(IVideoDomain domain)
		{
			this.domain = domain;
		}

		[HttpGet]
		public IEnumerable<Video> Get(Guid serverId)
		{
			return domain.Get(serverId);
		}

		[HttpGet("{videoId}")]
		public Video Get(Guid serverId, Guid videoId)
		{
			return domain.Get(serverId, videoId);
		}

		[HttpGet("{videoId}/Binary")]
		public async Task<IActionResult> GetDownload(Guid serverId, Guid videoId)
		{
			var down = domain.GetBinary(serverId, videoId, out string fileName);
			var file = File(down, "application/octet-stream");
			file.FileDownloadName = fileName;

			return file;
		}

		[HttpPost]
		public Video Create(Guid serverId, [FromForm] VideoViewModel form)
		{
			var file = Request.Form?.Files["binary"];

			return domain.Create(serverId, form, file.OpenReadStream(), file.FileName.Split('.').Last());
		}

		[HttpDelete("{id}")]
		public Video Delete(Guid serverId, Guid id)
		{
			return domain.Delete(serverId, id);
		}
	}
}