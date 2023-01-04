﻿namespace DesafioSeventh.Tests
{
	using DesafioSeventh.Domain;
	using DesafioSeventh.Domain.Global;
	using DesafioSeventh.Domain.Model;
	using DesafioSeventh.Domain.Providers;
	using DesafioSeventh.Domain.ViewModel;
	using DesafioSeventh.Service;

	public class VideoTests
	{
		IServerDomain _serverDomain = Substitute.For<IServerDomain>();
		IVideoDomain _videoDomain;
		IVideoRepository _videoRepository = Substitute.For<IVideoRepository>();
		IVideoFileProvider _videoProfile = Substitute.For<IVideoFileProvider>();

		Stream streamValid = new MemoryStream(Convert.FromBase64String("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"));


		[SetUp]
		public void Setup()
		{
			_videoDomain = new VideoService(_videoRepository, _videoProfile, _serverDomain);

			_videoProfile.AcceptedExtensions.Returns(new string[1] { "mp4" });
		}

		[Test]
		public void FileSizeZero()
		{
			var serverId = Guid.NewGuid();
			var streamZero = new MemoryStream(Convert.FromBase64String(""));

			_serverDomain.Get(Arg.Any<Guid>()).Returns(new Server { Id = serverId });

			Assert.Catch<InvalidVideoFileException>(() =>
			{
				_videoDomain.Create(serverId, new VideoViewModel
				{
					Description = "FileZero"
				}, streamZero, "mp4");
			});
		}

		[Test]
		public void InvalidExtension()
		{
			var serverId = Guid.NewGuid();

			_serverDomain.Get(Arg.Any<Guid>()).Returns(new Server { Id = serverId });

			Assert.Catch<InvalidVideoFileException>(() =>
			{
				_videoDomain.Create(serverId, new VideoViewModel
				{
					Description = "FileZero"
				}, streamValid, "pdf");
			});
		}
		[Test]
		public void CreateSuccess()
		{
			var serverId = Guid.NewGuid();
			var createRepo = false;
			_serverDomain.Get(Arg.Any<Guid>()).Returns(new Server { Id = serverId });
			_videoRepository.Create(Arg.Any<Video>(), Arg.Any<Action<Guid, Guid>>()).Returns((a) =>
			{
				createRepo = true;
				Video vids = (Video)a[0];
				Assert.That(vids.ServerId, Is.EqualTo(serverId));
				Assert.That(vids.SizeInBytes, Is.EqualTo(streamValid.Length));
				return vids;
			});

			var vid = _videoDomain.Create(serverId, new VideoViewModel
			{
				Description = "FileZero"
			}, streamValid, "mp4");

			Assert.That(createRepo, Is.True);
		}
	}
}