using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.Providers;
using System.Collections.Concurrent;

namespace DesafioSeventh.Web.Providers
{
	public class ScheduleVideoProvider : IScheduleRemoveProvider<Video>, IScheduleRemoveWorkerProvider<Video>
	{
		private static ConcurrentDictionary<Guid, ScheduleRemoveManage<Video>> _videos = new ConcurrentDictionary<Guid, ScheduleRemoveManage<Video>>();

		public IEnumerable<ScheduleRemoveManage<Video>> GetRunning()
		{
			return _videos.Values.Where(p => p.Status == ScheduleRemoveStatus.Running);
		}
		public IEnumerable<ScheduleRemoveManage<Video>> Get()
		{
			return _videos.Values.Where(p => p.Status == ScheduleRemoveStatus.Waiting);
		}

		public void Remove(ScheduleRemoveManage<Video> entity)
		{
			_videos.TryRemove(entity.Item.Id, out var _);
		}

		public void Set(Video entity)
		{
			if (!_videos.ContainsKey(entity.Id))
			{
				_videos.TryAdd(entity.Id, new ScheduleRemoveManage<Video>(entity));
			}
		}
	}
}
