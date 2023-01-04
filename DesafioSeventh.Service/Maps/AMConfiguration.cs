using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioSeventh.Service.Maps
{
	public class AMConfiguration : MapperConfiguration
	{
		public AMConfiguration()
			: base(a =>
			{
				a.AddProfile<AMProfile>();
				a.AddProfile<VideoProfile>();
			})
		{
		}
	}
}
