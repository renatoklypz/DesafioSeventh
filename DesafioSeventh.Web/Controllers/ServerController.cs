using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DesafioSeventh.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ServerController : ControllerBase {
		private readonly IServerDomain domain;

		public ServerController(IServerDomain domain)
		{
			this.domain = domain;
		}

		[HttpGet]
		public IEnumerable<Server> Get() => domain.Get();

		[HttpGet("{id}")]
		public Server Get(Guid id) => domain.Get(id);
	
		[HttpPost]
		public Server Create([FromBody] ServerCreate server) => domain.Create(server);

		[HttpPut("{id}")]
		public Server Update(Guid id, [FromBody] ServerUpdate server) => domain.Update(id, server);

		[HttpDelete("{id}")]
		public Server Delete(Guid id) => domain.Remove(id);

		[HttpGet("available/{id}")]
		public string Avaliable(Guid id) => domain.ServerStatus(id).ToString();
	}
}