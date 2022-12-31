using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioSeventh.Domain.Global
{
	public class CodeValidationException : ExceptionCode
	{
		public List<ValidationResult> Validation { get; set; }
		public CodeValidationException(List<ValidationResult> validation, string entity) : base(400, string.Format(Messages.ValidationEntityException, entity))
		{
			Validation = validation;
		}
	}
}
