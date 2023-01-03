using AutoMapper;
using DesafioSeventh.Domain.Model;
using DesafioSeventh.Domain.ViewModel;

namespace DesafioSeventh.Service.Maps
{
	public class VideoProfile: Profile
	{
		public VideoProfile() : base()
		{
			CreateMap<VideoViewModel, Video>();
		}
	}
}
