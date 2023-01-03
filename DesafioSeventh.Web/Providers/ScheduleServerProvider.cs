using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.Providers;
using System.Collections.Concurrent;

namespace DesafioSeventh.Web.Providers
{
	public class ScheduleServerProvider : IScheduleRemoveProvider<Server>, IScheduleRemoveWorkerProvider<Server>
	{
		private static ConcurrentDictionary<Guid, ScheduleRemoveManage<Server>> _servers = new ConcurrentDictionary<Guid, ScheduleRemoveManage<Server>>();

		public IEnumerable<ScheduleRemoveManage<Server>> Get()
		{
			return _servers.Values.Where(p => p.Status == ScheduleRemoveStatus.Waiting);
		}

		public void Remove(ScheduleRemoveManage<Server> entity)
		{
			_servers.TryRemove(entity.Item.Id, out var _);
		}

		public void Set(Server entity)
		{
			if (!_servers.ContainsKey(entity.Id))
			{
				_servers.TryAdd(entity.Id, new ScheduleRemoveManage<Server>(entity));
			}
		}
	}
}
