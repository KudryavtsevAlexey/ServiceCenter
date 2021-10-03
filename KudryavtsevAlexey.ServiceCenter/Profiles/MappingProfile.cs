using AutoMapper;
using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;

namespace KudryavtsevAlexey.ServiceCenter.Profiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ClientViewModel, Client>();
			CreateMap<DeviceViewModel, Device>();
			CreateMap<MasterViewModel, Master>()
				.ReverseMap();
			CreateMap<OrderViewModel, Order>();
		}
	}
}
