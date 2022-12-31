using AutoMapper;
using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Global;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;
using DesafioSeventh.Service.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace DesafioSeventh.Service
{
	public class ServerService : IServerDomain
	{
		private readonly IServerRepository repository;
		private IMapper _map;

		public ServerService()
		{
			var config = new AMConfiguration();
			_map = config.CreateMapper();
		}

		public ServerService(IServerRepository repository) : this()
		{
			this.repository = repository;
		}

		public Server Create(ServerCreate server)
		{
			if (server is null)
			{
				throw new ArgumentNullException(nameof(server));
			}

			var resultadoValidacao = new List<ValidationResult>();
			var contexto = new ValidationContext(server, null, null);
			if (!Validator.TryValidateObject(server, contexto, resultadoValidacao, true))
			{
				throw new CodeValidationException(resultadoValidacao, nameof(Server));
			}

			var srv = _map.Map<Server>(server);

			if (repository.Get(srv.Id) != null)
			{
				throw new ConflictException("Id");
			}

			srv = repository.Create(srv);

			return srv;
		}

		public IEnumerable<Server> Get()
		{
			throw new NotImplementedException();
		}

		public Server Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public Server Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public ServerStatus ServerStatus(Guid id)
		{
			throw new NotImplementedException();
		}

		public Server Update(Guid id, ServerUpdate servidor)
		{
			throw new NotImplementedException();
		}
	}
}
