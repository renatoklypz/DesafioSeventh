using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.Providers;
using System.Data.Common;
using System.Data.Entity;

namespace DesafioSeventh.Infra.Data
{
	public class ServerRepository : IServerRepository
	{
		readonly DBContextDefault context;
		public ServerRepository(IContextProvider<DBContextDefault> dbConnection)
		{
			context = dbConnection.GetContext();
		}
		public Server? Create(Server servidor)
		{
			context.Servers.Add(servidor);
			context.SaveChanges();

			return context.Servers.FirstOrDefault(p => p.Id == servidor.Id);
		}

		public bool Exists(Guid id)
		{
			return context.Servers.Any(p => p.Id == id);
		}

		public IEnumerable<Server> Get()
		{
			return context.Servers;
		}

		public Server? Get(Guid id)
		{
			return context.Servers.FirstOrDefault(f => f.Id == id);
		}

		public Server Remove(Guid id)
		{
			var server = context.Servers.FirstOrDefault(f => f.Id == id);
			_ = context.Entry(server).State = EntityState.Deleted;
			context.SaveChanges();

			return server;
		}

		public Server Update(Guid id, Server servidor)
		{
			var a = context.Servers.FirstOrDefault(f => f.Id == id);
			context.Entry(a).State = EntityState.Detached;
			servidor.Id = id;
			context.Entry(servidor).State = EntityState.Modified;
			context.SaveChanges();
			return servidor;
		}
	}
}