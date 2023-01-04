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
}
