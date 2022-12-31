using DesafioSeventh.Domain.Model;
using System;
using System.Collections.Generic;

namespace DesafioSeventh.Domain
{
	public interface IServerRepository
	{
		Server Create(Server servidor);
		Server Update(Guid id, Server servidor);
		IEnumerable<Server> Get();
		Server Get(Guid id);
		Server Remove(Guid id);
	}
}
