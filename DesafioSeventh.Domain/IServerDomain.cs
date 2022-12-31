using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace DesafioSeventh.Domain
{
	public interface IServerDomain
	{
		Server Create(ServerCreate servidor);
		Server Update(Guid id, ServerUpdate servidor);
		IEnumerable<Server> Get();
		Server Get(Guid id);
		Server Remove(Guid id);
		ServerStatus ServerStatus(Guid id);
	}
}
