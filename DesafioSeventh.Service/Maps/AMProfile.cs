using AutoMapper;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;
using System;

namespace DesafioSeventh.Service.Maps
{
	public class AMProfile : Profile
	{
		public AMProfile()
			: base()
		{
			CreateMap<ServerUpdate, Server>();
			CreateMap<ServerCreate, Server>().BeforeMap((s, d) =>
			{
				if(s.Id == null)
				{
					s.Id = Guid.NewGuid();
				}
			}).ReverseMap();
		}
	}
}
