using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.Providers;

namespace DesafioSeventh.Web.Workers
{
	public class DeleteServerWorker : BackgroundService
	{
		private readonly IScheduleRemoveWorkerProvider<Server> provider;
		private readonly IVideoFileProvider fileProvider;

		public DeleteServerWorker(IScheduleRemoveWorkerProvider<Server> provider, IVideoFileProvider fileProvider)
		{
			this.provider = provider;
			this.fileProvider = fileProvider;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				foreach (var item in provider.Get())
				{
					item.Status = ScheduleRemoveStatus.Running;
					try
					{
						Console.WriteLine($"ServerId: {item.Item.Id}");
						fileProvider.DeleteServer(item.Item.Id);
						provider.Remove(item);
					}
					catch (Exception ex)
					{
						if (item.CreatedAt <= DateTime.Now.AddDays(-1))
						{
							provider.Remove(item);
						}
						else
						{
							item.Status = ScheduleRemoveStatus.Waiting;
						}
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine(ex);
						Console.ForegroundColor = ConsoleColor.White;

					}
				}
				await Task.Delay(10000, stoppingToken);
			}
		}
	}
}
