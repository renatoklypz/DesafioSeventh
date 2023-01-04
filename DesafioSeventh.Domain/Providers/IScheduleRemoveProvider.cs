using System.Collections.Generic;

namespace DesafioSeventh.Domain.Providers
{
	public interface IScheduleRemoveProvider<T>
	{
		void Set(T entity);
		IEnumerable<ScheduleRemoveManage<T>> GetRunning();
	}
}
