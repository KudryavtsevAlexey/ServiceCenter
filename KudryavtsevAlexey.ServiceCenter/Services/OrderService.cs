using AutoMapper;
using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Services
{
    public class OrderService : IOrderService
	{
		private readonly IMapper _mapper;
		private readonly IContext _db;

		public OrderService(IContext db,IMapper mapper)
		{
			_mapper = mapper;
			_db = db;
		}

		public async Task<Order> MapOrder(OrderViewModel model)
		{
			var client = _mapper.Map<Client>(model.Client);

			var device = _mapper.Map<Device>(model.Device);

			var master = await _db.Masters.FindAsync(model.Master.MasterId);

			client.Devices.Add(device);
			device.Master = master;
			device.Client = client;
			master.Devices.Add(device);

			var order = _mapper.Map<Order>(model);

			order.Client = client;
			order.Device = device;
			order.Master = master;

			client.Orders.Add(order);
			device.Order = order;
			master.Orders.Add(order);

			return order;
		}
	}
}
