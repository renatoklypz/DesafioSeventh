using System.Collections.Generic;

namespace DesafioSeventh.Domain.Providers
{
	public interface IScheduleRemoveWorkerProvider<T>: IScheduleRemoveProvider<T>
	{
		IEnumerable<ScheduleRemoveManage<T>> Get();
		void Remove(ScheduleRemoveManage<T> entity);
	}
}
