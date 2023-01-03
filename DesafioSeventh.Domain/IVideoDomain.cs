using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace DesafioSeventh.Domain
{
	/// <summary>
	/// Dedica-se as regras de negócio para manutenção de vídeo
	/// </summary>
	public interface IVideoDomain
	{
		/// <summary>
		/// Cria um vídeo
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		/// <param name="entity">Informações do Vídeo</param>
		/// <param name="file">Stream do arquivo de vídeo</param>
		/// <param name="extension">Extensão do vídeo</param>
		/// <returns>Informações do vídeo</returns>
		Video Create(Guid serverId, VideoViewModel entity, Stream file, string extension);

		/// <summary>
		/// Apaga um vídeo
		/// </summary>
		/// <param name="serverId">Id do Servidor</param>
		/// <param name="videoId">Id do vídeo</param>
		/// <returns>Video apagado</returns>
		Video Delete(Guid serverId, Guid videoId);
		/// <summary>
		/// Obtém uma lista de vídeos de um servidor
		/// </summary>
		/// <param name="serverId">Id do Servidor</param>
		/// <returns>Lista de vídeos</returns>
		IEnumerable<Video> Get(Guid serverId);
		/// <summary>
		/// Obtém um vídeo pelo código
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		/// <param name="videoId">Id do vídeo</param>
		/// <returns>Informações do Vídeo</returns>
		Video Get(Guid serverId, Guid videoId);
		/// <summary>
		/// Obtém o arquivo de vídeo
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		/// <param name="videoId">Id do vídeo</param>
		/// <param name="fileName">Nome do arquivo gravado</param>
		/// <returns></returns>
		Stream GetBinary(Guid serverId, Guid videoId, out string fileName);
		void DeleteAll(Guid serverId);
	}
}
