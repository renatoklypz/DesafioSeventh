using DesafioSeventh.Domain;
using DesafioSeventh.Infra.Data;
using DesafioSeventh.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace DesafioSeventh.Web.Helpers
{
	public class TEE: IMetadataDetailsProvider
	{

	}
	public static class SetupHelper
	{
		public static string? ConnectionString { get => Configuration?.GetConnectionString("Default"); }
		public static IConfiguration? Configuration { get; set; } = null;
		public static DbConnection GetConnection() => new SqlConnection(ConnectionString);

		public static JsonSerializerSettings JSONFormatSetting
		{
			get => new()
			{
				ContractResolver = new DefaultContractResolver()
				{
					NamingStrategy = new CamelCaseNamingStrategy()
				},
				Formatting = Formatting.None,
				NullValueHandling = NullValueHandling.Ignore,
				TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
			};
		}

		public static IServiceCollection AddInject(this IServiceCollection services)
		{
			services.AddTransient<IServerRepository>(_ => new ServerRepository(GetConnection()));
			services.AddTransient<IServerDomain, ServerService>();
			return services;
		}
	}
}
