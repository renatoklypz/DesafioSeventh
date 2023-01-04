using System;
using System.IO;

namespace DesafioSeventh.Domain.Providers
{
	/// <summary>
	/// Dedica-se a manutenção do arquivo (Stream) de vídeo
	/// </summary>
	public interface IVideoFileProvider
	{
		string[] AcceptedExtensions { get; }
		/// <summary>
		/// Salva um Stream de vídeo
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		/// <param name="videoId">Id do video</param>
		/// <param name="file">Stream de vídeo</param>
		/// <param name="extension">Extensão do vídeo</param>
		/// <returns>Stream gravado</returns>
		Stream Save(Guid serverId, Guid videoId, Stream file, string extension);
		/// <summary>
		/// Obtém um Stream de vídeo gravado
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		/// <param name="videoId">Id do video</param>
		/// <param name="fileName">(OUT) Nome do arquivo</param>
		/// <returns>Stream gravado</returns>
		Stream Get(Guid serverId, Guid videoId, out string fileName);
		/// <summary>
		/// Verifica se existe um arquivo
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		/// <param name="videoId">Id do video</param>
		/// <returns>True = arquivo existe</returns>
		bool Has(Guid serverId, Guid videoId);

		/// <summary>
		/// Remove um arquivo
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		/// <param name="videoId">Id do video</param>
		void Delete(Guid serverId, Guid videoId);

		/// <summary>
		/// Remove todos os registros (Pastas e arquivos) vinculados ao vídeo
		/// </summary>
		/// <param name="serverId">Id do servidor</param>
		void DeleteServer(Guid serverId);
		void CreateServer(Guid serverId);
	}
}
