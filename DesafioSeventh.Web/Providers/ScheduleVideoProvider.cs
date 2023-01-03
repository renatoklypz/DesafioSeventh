using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.Providers;
using System.Collections.Concurrent;

namespace DesafioSeventh.Web.Providers
{
	public class ScheduleVideoProvider : IScheduleRemoveProvider<Video>, IScheduleRemoveWorkerProvider<Video>
	{
		private static ConcurrentDictionary<Guid, ScheduleRemoveManage<Video>> _servers = new ConcurrentDictionary<Guid, ScheduleRemoveManage<Video>>();

		public IEnumerable<ScheduleRemoveManage<Video>> Get()
		{
			return _servers.Values.Where(p => p.Status == ScheduleRemoveStatus.Waiting);
		}

		public void Remove(ScheduleRemoveManage<Video> entity)
		{
			_servers.TryRemove(entity.Item.Id, out var _);
		}

		public void Set(Video entity)
		{
			if (!_servers.ContainsKey(entity.Id))
			{
				_servers.TryAdd(entity.Id, new ScheduleRemoveManage<Video>(entity));
			}
		}
	}
}
