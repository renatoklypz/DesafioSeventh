using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioSeventh.Domain.Global
{
	public class ConflictException : ExceptionCode
	{
		public ConflictException(string field) : base("err_conflict", string.Format(Messages.Conflict, field))
		{

		}
	}

	public class InvalidVideoFileException : ExceptionCode
	{
		public InvalidVideoFileException(string error, params string[] args)
			: base("vid_invalid_file", string.Format(ErrorMap.vid_invalid_file, string.Format(ErrorMap.ResourceManager.GetString($"vid_invalid_file_{error}"), args)))
		{

		}
	}
}
