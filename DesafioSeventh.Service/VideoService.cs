using AutoMapper;
using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Global;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.Providers;
using DesafioSeventh.Domain.ViewModel;
using DesafioSeventh.Service.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioSeventh.Service
{
	public class VideoService : IVideoDomain
	{
		private readonly IVideoRepository repository;
		private readonly IVideoFileProvider fileProfile;
		private readonly IServerDomain serverDomain;
		private IMapper _map;
		public VideoService()
		{
			var config = new AMConfiguration();
			_map = config.CreateMapper();
		}

		public VideoService(IVideoRepository repository, IVideoFileProvider fileProfile, IServerDomain serverDomain) : this()
		{
			this.repository = repository;
			this.fileProfile = fileProfile;
			this.serverDomain = serverDomain;
		}

		public Video Create(Guid serverId, VideoViewModel video, Stream file, string extension)
		{
			//Validar Parâmetro
			if(serverId == default)
			{
				throw new ArgumentNullException(nameof(serverId));
			}

			if (video is null)
			{
				throw new ArgumentNullException(nameof(video));
			}

			if (file is null)
			{
				throw new ArgumentNullException(nameof(file));
			}

			if (string.IsNullOrEmpty(extension))
			{
				throw new ArgumentException($"'{nameof(extension)}' não pode ser nulo nem vazio.", nameof(extension));
			}

			//Validar se servidor existe
			if(serverDomain.Get(serverId) == null)
			{
				throw new ConflictException("ServerId");
			}

			//Validar se vídeo tem ZERO Bytes
			if(file.Length == 0)
			{
				throw new InvalidVideoFileException("size_zero");
			}

			if (!fileProfile.AcceptedExtensions.Contains(extension))
			{
				throw new InvalidVideoFileException("format", extension, string.Join(',', fileProfile.AcceptedExtensions));
			}

			// Validar entidade
			var resultadoValidacao = new List<ValidationResult>();
			var contexto = new ValidationContext(video, null, null);
			if (!Validator.TryValidateObject(video, contexto, resultadoValidacao, true))
			{
				throw new CodeValidationException(resultadoValidacao, nameof(Video));
			}


			var videoModel = _map.Map<Video>(video);
			videoModel.ServerId = serverId;
			videoModel.SizeInBytes = file.Length;


			var result = repository.Create(videoModel, (Guid serverId, Guid videoId) =>
			{
				fileProfile.Save(serverId, videoId, file, extension);

			});

			return result;
		}

		public IEnumerable<Video> Get(Guid serverId)
		{
			return repository.Get(serverId);
		}

		public Video Get(Guid serverId, Guid videoId)
		{
			return repository.Get(serverId, videoId);
		}

		public Stream GetBinary(Guid serverId, Guid videoId, out string fileName)
		{
			return fileProfile.Get(serverId, videoId, out fileName);
		}

		public Video Delete(Guid serverId, Guid id)
		{
			var result = repository.Delete(serverId, id, (serverId, videoId) => fileProfile.Delete(serverId, videoId));
			return result;
		}

		public void DeleteAll(Guid serverId)
		{
			repository.DeleteAll(serverId);
		}
	}
}
