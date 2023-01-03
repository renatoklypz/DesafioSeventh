using DesafioSeventh.Domain;
using DesafioSeventh.Infra.Data;
using DesafioSeventh.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using DesafioSeventh.Web.Providers;
using DesafioSeventh.Domain.Providers;
using DesafioSeventh.Domain.Model;

namespace DesafioSeventh.Web.Helpers
{
	public class TEE : IMetadataDetailsProvider
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
			services.AddTransient<IScheduleRemoveProvider<Server>, ScheduleServerProvider>();
			services.AddTransient<IScheduleRemoveWorkerProvider<Server>, ScheduleServerProvider>();
			services.AddTransient<IScheduleRemoveProvider<Video>, ScheduleVideoProvider>();
			services.AddTransient<IScheduleRemoveWorkerProvider<Video>, ScheduleVideoProvider>();

			services.AddTransient<IVideoFileProvider>(_ => new VideoFileProvider(Configuration["VideoConfig:Path"], Configuration.GetSection("VideoConfig:AcceptedExtensions").Get<string[]>()));
			services.AddTransient<IServerRepository>(_ => new ServerRepository(GetConnection()));
			services.AddTransient<IVideoRepository>(_ => new VideoRepository(GetConnection()));

			services.AddTransient<IServerDomain>((s) =>
			{
				var result = new ServerService(s.GetService<IServerRepository>());

				result.OnBeforeServerDelete += (id) =>
				{
					var vp = s.GetService<IScheduleRemoveProvider<Server>>();
					vp.Set(result.Get(id));

					var vd = s.GetService<IVideoDomain>();

					vd.DeleteAll(serverId: id);

				};
				return result;
			});

			services.AddTransient<IVideoDomain, VideoService>();
			return services;
		}
	}
}
