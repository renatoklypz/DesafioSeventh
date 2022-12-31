using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioSeventh.Domain.Global
{
	public class ConflictException : ExceptionCode
	{
		public ConflictException(string field) : base(409, string.Format(Messages.Conflict, field))
		{

		}
	}
}
