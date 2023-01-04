using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioSeventh.Infra.Data.Migrations
{
	public class Configuration: DbMigrationsConfiguration<DBContextDefault>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			ContextKey = "DBContextDefault";
		}
	}
}
