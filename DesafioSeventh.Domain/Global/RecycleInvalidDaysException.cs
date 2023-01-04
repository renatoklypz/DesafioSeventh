namespace DesafioSeventh.Domain.Global
{
	public class RecycleInvalidDaysException : ExceptionCode
	{
		public RecycleInvalidDaysException(int days) : base("recycle_invalid_day", string.Format(ErrorMap.recycle_invalid_day, days))
		{

		}
	}
}
