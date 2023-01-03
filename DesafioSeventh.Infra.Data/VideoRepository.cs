﻿using DesafioSeventh.Domain;
using DesafioSeventh.Domain.Model;
using System.Data;
using System.Data.Common;
using System.Data.Entity;

namespace DesafioSeventh.Infra.Data
{
	public class VideoRepository : IVideoRepository
	{
		readonly DBContextDefault context;
		public VideoRepository(DbConnection dbConnection)
		{
			context = new DBContextDefault(dbConnection);

		}

		public Video? Create(Video entity, Action<Guid, Guid> onCreate)
		{
			context.Videos.Add(entity); using (var trans = context.Database.BeginTransaction())
			{
				try
				{
					context.SaveChanges();
					onCreate(entity.ServerId, entity.Id);
					trans.Commit();
				}
				catch
				{
					trans.Rollback();
				}
			};

			return context.Videos.FirstOrDefault(p => p.Id == entity.Id);
		}

		public Video? Delete(Guid serverId, Guid videoId, Action<Guid, Guid> onDelete)
		{
			var server = context.Videos.FirstOrDefault(f => f.Id == videoId && f.ServerId == serverId);
			_ = context.Entry(server).State = EntityState.Deleted;
			using (var trans = context.Database.BeginTransaction())
			{
				try
				{
					context.SaveChanges();
					onDelete(serverId, videoId);
					trans.Commit();
				}
				catch
				{
					trans.Rollback();
				}
			};

			return server;
		}

		public void DeleteAll(Guid serverId)
		{
			var vd = context.Videos.Where(v => v.ServerId == serverId);
			foreach(var item in vd)
			{
				context.Entry(item).State = EntityState.Deleted;
			}

			context.SaveChanges();
		}

		public IEnumerable<Video> Get(Guid serverId)
		{
			var videos = context.Videos.Where(f => f.ServerId == serverId);
			return videos;
		}

		public Video? Get(Guid serverId, Guid videoId)
		{
			var video = context.Videos.FirstOrDefault(f => f.Id == videoId && f.ServerId == serverId);
			return video;
		}
	}
}