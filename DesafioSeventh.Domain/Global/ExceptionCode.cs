using System;

namespace DesafioSeventh.Domain.Global
{
	public class ExceptionCode : Exception
	{
		public string Code { get; }

		public ExceptionCode(string code, string message) : base(message)
		{
			Code = code;
		}
	}
}
