using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Model;
using System.Data;
using System.Data.Common;
using System.Data.Entity;

namespace DesafioSeventh.Infra.Data
{
	public class DBContextDefault: DbContext
	{
		public DBContextDefault(DbConnection dbConnection): base(dbConnection, true)
		{

		}

		public DbSet<Server> Servers { get; set; }
		public DbSet<Video> Videos { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}

	public class ServerRepository : IServerRepository
	{
		readonly DBContextDefault context;
		public ServerRepository(DbConnection dbConnection)
		{
			context = new DBContextDefault(dbConnection);

		}
		public Server? Create(Server servidor)
		{
			context.Entry(servidor);
			context.SaveChanges();

			return context.Servers.FirstOrDefault(p=> p.Id == servidor.Id);
		}

		public IEnumerable<Server> Get()
		{
			throw new NotImplementedException();
		}

		public Server Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public Server Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public Server Update(Guid id, Server servidor)
		{
			throw new NotImplementedException();
		}
	}
}