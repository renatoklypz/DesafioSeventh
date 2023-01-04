using DesafioSeventh.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioSeventh.Domain.Providers
{
	public interface IContextProvider<T>
	{
		T GetContext();
	}
}
