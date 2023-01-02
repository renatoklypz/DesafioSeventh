using DesafioSeventh.Domain.Model;
using System.Data.Common;
using System.Data.Entity;

namespace DesafioSeventh.Infra.Data
{
	public class DBContextDefault : DbContext
	{
		public DBContextDefault(DbConnection dbConnection) : base(dbConnection, true)
		{
		}

		public DbSet<Server> Servers { get; set; }
		public DbSet<Video> Videos { get; set; }
	}
}