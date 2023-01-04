using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.Providers;
using DesafioSeventh.Domain.ViewModel;

namespace DesafioSeventh.Web.Workers
{
	public class RecycleWorker : BackgroundService
	{
		private readonly IScheduleRemoveWorkerProvider<RecycleSchedule> provider;
		private readonly IVideoDomain videoDomain;

		public RecycleWorker(IScheduleRemoveWorkerProvider<RecycleSchedule> provider, IVideoDomain videoDomain)
		{
			this.provider = provider;
			this.videoDomain = videoDomain;
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
						IEnumerable<Video> toDelete = videoDomain.GetByDateBefore(item.Item.DateRemoved).ToList();
						foreach (var itemDelete in toDelete)
						{
							videoDomain.Delete(itemDelete.ServerId, itemDelete.Id);
						}
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
						Console.WriteLine($"Recycle: {ex}");
						Console.ForegroundColor = ConsoleColor.White;
					}

				}
				await Task.Delay(10000, stoppingToken);
			}
		}
	}
}
