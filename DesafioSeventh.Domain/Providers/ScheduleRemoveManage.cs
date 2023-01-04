using System;

namespace DesafioSeventh.Domain.Providers
{
	public class ScheduleRemoveManage<T>
	{
		public ScheduleRemoveManage(T item)
		{
			Item = item;
		}

		public T Item { get; set; }
		public ScheduleRemoveStatus Status { get; set; } = ScheduleRemoveStatus.Waiting;
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
