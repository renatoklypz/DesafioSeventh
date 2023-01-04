using DesafioSeventh.Domain.Model;
using System;
using System.Collections.Generic;

namespace DesafioSeventh.Domain
{
	/// <summary>
	/// Dedica-se a manutenção de banco de dados
	/// </summary>
	public interface IVideoRepository
	{
		/// <summary>
		/// Cria um vídeo no banco de dados
		/// </summary>
		/// <param name="entity">Informações do vídeo</param>
		/// <param name="onCreate">Ação antes de confirmar a criação. Execute a criação do arquivo aqui, em caso de falha trate com EXCEPTION.</param>
		/// <returns>Informação do vídeo criado</returns>
		Video Create(Video entity, Action<Guid, Guid> onCreate);
		/// <summary>
		/// Apaga um vídeo no banco de dados
		/// </summary>
		/// <param name="serverId">Id do Servidor</param>
		/// <param name="videoId">Id do vídeo</param>
		/// <param name="onDelete">Ação antes de confirmar a exclusão. Execute a exclusão do arquivo aqui, em caso de falha trate com EXCEPTION.</param>
		/// <returns>Informação do vídeo apagado</returns>
		Video Delete(Guid serverId, Guid videoId, Action<Guid, Guid> onDelete);
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
		void DeleteAll(Guid serverId);
		IEnumerable<Video> GetByDateBefore(DateTime dateRemoved);
	}
}
