using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.Providers;
using System.Data.Common;
using System.Data.Entity;

namespace DesafioSeventh.Infra.Data
{
	public class DBContextDefault : DbContext, IContextProvider<DBContextDefault>
	{
		public DBContextDefault() : base("Default")
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBContextDefault, Migrations.Configuration>());
		}

		public DBContextDefault(DbConnection dbConnection) : base(dbConnection, true)
		{
			base.Configuration.LazyLoadingEnabled = false;

			Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBContextDefault, Migrations.Configuration>(true, new Migrations.Configuration
			{
				AutomaticMigrationsEnabled = true
			}));
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		public DBContextDefault GetContext()
		{
			return this;
		}

		public DbSet<Server> Servers { get; set; }
		public DbSet<Video> Videos { get; set; }
	}
}