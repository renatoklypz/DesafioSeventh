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

		public ServerTests()
		{

		}

		[SetUp]
		public void Setup()
		{
			_serverDomain = new ServerService(_serverRepository);
		}

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
				Port = sc.Port
			});

			if (isError && codeError.HasValue)
			{
				switch (codeError.Value)
				{
					case 409:
						_serverRepository.Get(new Guid(id)).Returns(new Server { Id = sc.Id.Value, Name = sc.Name });
						Assert.Catch<ConflictException>(() =>
						{
							var a = _serverDomain.Create(sc);
						});
						break;
					case 400:
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
				_serverRepository.Get(Arg.Any<Guid>()).Returns(returnThis: null as Server);

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