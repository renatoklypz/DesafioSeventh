using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Global;
using DesafioSeventh.Domain.Providers;
using DesafioSeventh.Domain.ViewModel;
using System;
using System.Linq;

namespace DesafioSeventh.Service
{
	public class RecycleVideoService : IRecycleVideoDomain
	{
		private readonly IScheduleRemoveProvider<RecycleSchedule> workerProvider;

		public RecycleVideoService(IScheduleRemoveProvider<RecycleSchedule> workerProvider)
		{
			this.workerProvider = workerProvider;
		}

		public RecycleStatus Status => new() { StatusEnum = workerProvider.GetRunning().Any() ? ServerStatusEnum.Running : ServerStatusEnum.NotRunning };


		public void Recycle(int days)
		{
			if(days < 0)
			{
				throw new RecycleInvalidDaysException(days);
			}
			workerProvider.Set(new RecycleSchedule { DateRemoved = DateTime.Today.AddDays(-days) });
		}
	}
}
