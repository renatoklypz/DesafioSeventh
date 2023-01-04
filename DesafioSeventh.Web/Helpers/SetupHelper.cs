using DesafioSeventh.Domain;
using DesafioSeventh.Infra.Data;
using DesafioSeventh.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data.Common;
using System.Data.SqlClient;
using DesafioSeventh.Web.Providers;
using DesafioSeventh.Domain.Providers;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;

namespace DesafioSeventh.Web.Helpers
{
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

		public static IServiceCollection AddWorkers(this IServiceCollection services)
		{
			services.AddTransient<IScheduleRemoveProvider<Server>, ScheduleServerProvider>();
			services.AddTransient<IScheduleRemoveWorkerProvider<Server>, ScheduleServerProvider>();
			services.AddTransient<IScheduleRemoveProvider<Video>, ScheduleVideoProvider>();
			services.AddTransient<IScheduleRemoveWorkerProvider<Video>, ScheduleVideoProvider>();
			services.AddTransient<IScheduleRemoveProvider<RecycleSchedule>, ScheduleRecycleProvider>();
			services.AddTransient<IScheduleRemoveWorkerProvider<RecycleSchedule>, ScheduleRecycleProvider>();

			return services;
		}
		public static IServiceCollection AddInject(this IServiceCollection services)
		{
			services.AddWorkers();

			services.AddSingleton<IContextProvider<DBContextDefault>>(_ => new DBContextDefault(GetConnection()));

			services.AddTransient<IVideoFileProvider>(_ => new VideoFileProvider(Configuration["VideoConfig:Path"], Configuration.GetSection("VideoConfig:AcceptedExtensions").Get<string[]>()));
			services.AddTransient<IServerRepository, ServerRepository>();
			services.AddTransient<IVideoRepository, VideoRepository>();

			services.AddTransient<IRecycleVideoDomain, RecycleVideoService>();

			services.AddTransient<IServerDomain>((s) =>
			{
				var result = new ServerService(s.GetService<IServerRepository>(), s.GetService<IVideoDomain>());
				result.OnAfterServerCreate += (id) =>
				{
					var videoFileProv = s.GetService<IVideoFileProvider>();
					videoFileProv?.CreateServer(id);
				};

				result.OnAfterServerDelete += (id) =>
				{
					var scheduleServerProvider = s.GetService<IScheduleRemoveProvider<Server>>();
					scheduleServerProvider?.Set(result.Get(id));
				};
				return result;
			});

			services.AddTransient<IVideoDomain, VideoService>();
			return services;
		}
	}
}
