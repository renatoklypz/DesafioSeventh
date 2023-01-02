namespace DesafioSeventh.Domain.Global
{
	public class NotExistsException: ExceptionCode
	{
		public NotExistsException(string entity, string id): base($"err_not_exists", string.Format(ErrorMap.NotExists, entity, id))
		{

		}
	}
}
