namespace DesafioSeventh.Tests
{
	using DesafioSeventh.Domain;
	using DesafioSeventh.Domain.Global;
	using DesafioSeventh.Domain.Model;
	using DesafioSeventh.Domain.ViewModel;
	using DesafioSeventh.Service;

	public class ServerTests
	{
		IServerRepository _serverRepository = Substitute.For<IServerRepository>();
		IServerDomain _serverDomain;
		Func<Guid, ServerCreate, Server> makeClass = (Guid id, ServerCreate sc) => new Server
		{
			Id = id,
			Name = sc.Name,
			IP = sc.IP,
			Port = sc.Port.Value
		};
		public ServerTests()
		{

		}

		[SetUp]
		public void Setup()
		{
			_serverDomain = new ServerService(_serverRepository);
		}

		/// <summary>
		/// Teste do campo IP
		/// </summary>
		[Test]
		[TestCase("127.0.0.1", true, Category = "Server")]
		[TestCase("192.168.0.1", true, Category = "Server")]
		[TestCase("127.0.0", false, Category = "Server")]
		[TestCase("256.256.0.1", false, Category = "Server")]
		[TestCase("256.256.0.1123123123", false, Category = "Server")]
		[TestCase(null, false, Category = "Server")]
		public void IPValidationTest(string ip, bool success)
		{
			var id = Guid.NewGuid();

			ServerCreate sc = new ServerCreate
			{
				Name = "Server Test",
				IP = ip,
				Port = 80
			};
			ServerUpdate su = new ServerUpdate
			{
				Name = "Server Test",
				IP = ip,
				Port = 80
			};
			_serverRepository.Get(Arg.Any<Guid>()).Returns(_ => makeClass(id, sc));
			_serverRepository.Create(Arg.Any<Server>()).Returns(_ => makeClass(id, sc));

			if (success)
			{
				_serverRepository.Exists(Arg.Any<Guid>()).Returns(false);
				_serverDomain.Create(sc);
				_serverRepository.Exists(Arg.Any<Guid>()).Returns(true);
				_serverDomain.Update(id, su);
			}
			else
			{
				_serverRepository.Exists(Arg.Any<Guid>()).Returns(false);
				Assert.Catch<CodeValidationException>(() => _serverDomain.Create(sc));
				_serverRepository.Exists(Arg.Any<Guid>()).Returns(true);
				Assert.Catch<CodeValidationException>(() => _serverDomain.Update(id, su));
			}
		}

		/// <summary>
		/// Criação de Servidor
		/// </summary>
		[Test]
		[TestCase(null, "Servidor 1", "127.0.0.1", 8080, false, null, Category = "Server")]
		[TestCase("2ecb5d1b-1d1a-4361-8759-3b03055d6cda", "Servidor 2", "127.0.0.1", 8080, true, 409, Category = "Server")]
		[TestCase(null, "Servidor 3", "256.0.0.1", 80, true, 400, Category = "Server")]
		public void CreateTest(string id, string name, string ip, int? port, bool isError, int? codeError)
		{
			ServerCreate sc = new ServerCreate
			{
				Id = string.IsNullOrEmpty(id) ? null : new Guid(id),
				Name = name,
				IP = ip,
				Port = port
			};

			_serverRepository.Create(Arg.Any<Server>()).Returns(new Server
			{
				Id = sc.Id ?? Guid.NewGuid(),
				Name = sc.Name,
				IP = sc.IP,
				Port = sc.Port.Value
			});

			if (isError && codeError.HasValue)
			{
				switch (codeError.Value)
				{
					case 409:
						_serverRepository.Exists(Arg.Any<Guid>()).Returns(true);
						_serverRepository.Get(new Guid(id)).Returns(new Server { Id = sc.Id.Value, Name = sc.Name });
						Assert.Catch<ConflictException>(() =>
						{
							var a = _serverDomain.Create(sc);
						});
						break;
					case 400:
						_serverRepository.Exists(Arg.Any<Guid>()).Returns(false);
						_serverRepository.Get(Arg.Any<Guid>()).Returns(returnThis: null as Server);
						Assert.Catch<CodeValidationException>(() =>
						{
							var a = _serverDomain.Create(sc);
						});
						break;
				}
			}
			else
			{
				_serverRepository.Exists(Arg.Any<Guid>()).Returns(false);
				_serverRepository.Create(Arg.Any<Server>()).Returns(_ => makeClass(sc.Id ?? Guid.NewGuid(), sc));

				var server = _serverDomain.Create(sc);
				if (id == null)
				{
					Assert.That(Guid.Empty, Is.Not.EqualTo(server.Id), "Não gerou o ID automático");
				}
				else
				{
					Assert.That(new Guid(id), Is.EqualTo(server.Id), "Gerou um ID quando já existia um.");
				}
			}
		}
	}
}