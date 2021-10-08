using AutoMapper;
using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;

namespace KudryavtsevAlexey.ServiceCenter.Profiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ClientViewModel, Client>()
				.ReverseMap();
			CreateMap<DeviceViewModel, Device>()
				.ReverseMap();
			CreateMap<MasterViewModel, Master>()
				.ReverseMap();
			CreateMap<OrderViewModel, Order>()
				.ReverseMap();
			CreateMap<Order, OrderViewModel>();
		}
	}
}
