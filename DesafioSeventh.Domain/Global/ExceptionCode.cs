using System;

namespace DesafioSeventh.Domain.Global
{
	public class ExceptionCode : Exception
	{
		public int HttpCode { get; }

		public ExceptionCode(int httpCode, string message) : base(message)
		{
			HttpCode = httpCode;
		}
	}
}
