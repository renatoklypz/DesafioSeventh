using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace DesafioSeventh.Domain
{
	public delegate void ServerEventHandler(Guid serverId);

	public interface IServerDomain
	{
		event ServerEventHandler OnBeforeServerCreate;
		event ServerEventHandler OnAfterServerCreate;
		event ServerEventHandler OnBeforeServerUpdate;
		event ServerEventHandler OnAfterServerUpdate;
		event ServerEventHandler OnBeforeServerDelete;
		event ServerEventHandler OnAfterServerDelete;

		Server Create(ServerCreate servidor);
		Server Update(Guid id, ServerUpdate servidor);
		IEnumerable<Server> Get();
		Server Get(Guid id);
		Server Remove(Guid id);
		RecycleStatus ServerStatus(Guid id);
	}
}
