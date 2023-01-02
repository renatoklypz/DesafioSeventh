﻿using AutoMapper;
using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Global;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;
using DesafioSeventh.Service.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Net.Sockets;

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

			if (repository.Exists(srv.Id))
			{
				throw new ConflictException("Id");
			}

			srv = repository.Create(srv);

			return srv;
		}

		public IEnumerable<Server> Get()
		{
			return repository.Get();
		}

		public Server Get(Guid id)
		{
			return repository.Get(id);
		}

		public Server Remove(Guid id)
		{

			if (!repository.Exists(id))
			{
				throw new NotExistsException("Server", id.ToString());
			}

			return repository.Remove(id);
		}

		public ServerStatus ServerStatus(Guid id)
		{
			ServerStatus result = new();
			if (!repository.Exists(id))
			{
				throw new NotExistsException("Server", id.ToString());
			}
			var server = repository.Get(id);

			using (TcpClient client = new())
			{
				try
				{
					client.Connect(server.IP, server.Port);

					using NetworkStream ClientStream = client.GetStream();
					result.StatusEnum = ServerStatusEnum.Running;
				}
				catch (SocketException)
				{
					result.StatusEnum = ServerStatusEnum.NotRunning;
				}
			}

			return result;
		}

		public Server Update(Guid id, ServerUpdate servidor)
		{
			if (!repository.Exists(id))
			{
				throw new NotExistsException("Server", id.ToString());
			}
			else
			{
				var serverOld = Get(id);
				var serverUp = _map.Map<Server>(servidor);
				serverUp.Id = id;
				serverUp.ModifiedAt = DateTime.Now;
				serverUp.CreatedAt = serverOld.CreatedAt;

				var resultadoValidacao = new List<ValidationResult>();
				var contexto = new ValidationContext(servidor, null, null);
				if (!Validator.TryValidateObject(servidor, contexto, resultadoValidacao, true))
				{
					throw new CodeValidationException(resultadoValidacao, nameof(Server));
				}

				repository.Update(id, serverUp);
				return serverUp;
			}
		}
	}
}
