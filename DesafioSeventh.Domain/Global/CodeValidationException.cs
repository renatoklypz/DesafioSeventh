using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DesafioSeventh.Domain.Global
{
	public class CodeValidationException : ExceptionCode
	{
		public Dictionary<string, List<string>> Report { get; private set; } = new Dictionary<string, List<string>>();
		public CodeValidationException(List<ValidationResult> report, string entity) : base("err_validation", string.Format(Messages.ValidationEntityException, entity))
		{
			foreach (var item in report)
			{
				foreach (var itemName in item.MemberNames)
				{
					if (Report.ContainsKey(itemName))
					{
						Report[itemName].Add(item.ErrorMessage);
					}
					else
					{
						Report.Add(itemName, new List<string>() { item.ErrorMessage });
					}
				}
			}
		}
	}
}
