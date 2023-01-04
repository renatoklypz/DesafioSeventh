using DesafioSeventh.Domain.Providers;

namespace DesafioSeventh.Web.Providers
{
	public class VideoFileProvider : IVideoFileProvider
	{
		private readonly string basePath;
		private readonly string[] acceptedExtensions;

		public VideoFileProvider(string basePath, string[] acceptedExtensions)
		{
			if (!Directory.Exists(basePath))
			{
				Directory.CreateDirectory(basePath);
			}
			this.basePath = basePath;
			this.acceptedExtensions = acceptedExtensions;
		}

		public string[] AcceptedExtensions { get => acceptedExtensions; }

		public void Delete(Guid serverId, Guid videoId)
		{
			var file = Directory.GetFiles(Path.Combine(basePath, serverId.ToString()), $"{videoId}.*").FirstOrDefault();
			if (file != null)
			{
				File.Delete(file);
			}
		}

		public void CreateServer(Guid serverId)
		{
			if (!Directory.Exists(Path.Combine(basePath, serverId.ToString())))
			{
				Directory.CreateDirectory(Path.Combine(basePath, serverId.ToString()));
			}
		}
		public void DeleteServer(Guid serverId)
		{
			if (Directory.Exists(Path.Combine(basePath, serverId.ToString())))
			{
				Directory.Delete(Path.Combine(basePath, serverId.ToString()), true);
			}
		}

		public Stream Get(Guid serverId, Guid videoId, out string fileName)
		{
			if (!Directory.Exists(Path.Combine(basePath, serverId.ToString())))
			{
				throw new Exception("File not exists");
			}

			var file = Directory.GetFiles(Path.Combine(basePath, serverId.ToString()), $"{videoId}.*").FirstOrDefault();
			if (file == null)
			{
				throw new Exception("File not exists");
			}
			else
			{
				fileName = file.Split("\\").Last();
				Stream st = File.OpenRead(file);
				return st;
			}
		}

		public bool Has(Guid serverId, Guid videoId)
		{
			return Directory.GetFiles(Path.Combine(basePath, serverId.ToString()), $"{videoId}.*").Any();
		}

		public Stream Save(Guid serverId, Guid videoId, Stream file, string extension)
		{
			CreateServer(serverId);

			using (var _file = File.Create(Path.Combine(basePath, serverId.ToString(), $"{videoId}.{extension}")))
			{
				file.Seek(0, SeekOrigin.Begin);
				file.CopyTo(_file);
				_file.Flush();
			}

			return file;
		}
	}
}
