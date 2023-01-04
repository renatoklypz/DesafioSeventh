using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioSeventh.Domain.Global
{
	public static class Config
	{
		public const string REGEX_IP = @"(\b25[0-5]|\b2[0-4][0-9]|\b[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}";

		public const string VAL_IPVALIDATION = "VIPValidation";
		public const string VAL_REQUIRED = "VRequired";
		public const string VAL_STRING_LENGTH = "VStringLength";
		public const string VAL_STRING_LENGTH_MIN = "VStringLengthMin";
	}
}
