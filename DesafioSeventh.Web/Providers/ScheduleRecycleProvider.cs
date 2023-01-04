using DesafioSeventh.Domain.Providers;
using DesafioSeventh.Domain.ViewModel;
using System.Collections.Concurrent;

namespace DesafioSeventh.Web.Providers
{
	public class ScheduleRecycleProvider : IScheduleRemoveProvider<RecycleSchedule>, IScheduleRemoveWorkerProvider<RecycleSchedule> { 
		private static ConcurrentDictionary<DateTime, ScheduleRemoveManage<RecycleSchedule>> _recycle = new ConcurrentDictionary<DateTime, ScheduleRemoveManage<RecycleSchedule>>();

		public IEnumerable<ScheduleRemoveManage<RecycleSchedule>> GetRunning()
		{
			return _recycle.Values.Where(p => p.Status == ScheduleRemoveStatus.Running);
		}

		public void Remove(ScheduleRemoveManage<RecycleSchedule> entity)
		{
			_recycle.TryRemove(entity.Item.DateRemoved.Date, out var _);
		}

		public void Set(RecycleSchedule entity)
		{
			if (!_recycle.ContainsKey(entity.DateRemoved.Date))
			{
				_recycle.TryAdd(entity.DateRemoved.Date, new ScheduleRemoveManage<RecycleSchedule>(entity));
			}
		}

		IEnumerable<ScheduleRemoveManage<RecycleSchedule>> IScheduleRemoveWorkerProvider<RecycleSchedule>.Get()
		{
			return _recycle.Values.Where(p => p.Status == ScheduleRemoveStatus.Waiting);
		}
	}
}
