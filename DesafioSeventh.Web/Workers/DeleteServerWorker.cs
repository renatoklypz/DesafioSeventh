using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine(ex);
						Console.ForegroundColor = ConsoleColor.White;
						item.Status = ScheduleRemoveStatus.Waiting;

					}
				}
				await Task.Delay(10000, stoppingToken);
			}
		}
	}
}
