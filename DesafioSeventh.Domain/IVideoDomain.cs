using DesafioSeventh.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace DesafioSeventh.Domain
{
	public interface IVideoDomain
	{
		Video Create(Video video);
		Video Update(Video video);
		IEnumerable<Video> Get(Guid serverId);
		Video Get(Guid serverId, Guid videoId);
		Stream GetBinary(Guid serverId, Guid videoId);

	}
}
