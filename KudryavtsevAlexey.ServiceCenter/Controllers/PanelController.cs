using AutoMapper;
using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
	[Authorize(Policy ="Master")]
	public class PanelController : Controller
	{
		private readonly ApplicationContext _db;
		private readonly IMapper _mapper;

		public PanelController(ApplicationContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		public async Task<IActionResult> CreateOrder()
		{
			var suitableMasters = await _db.Masters
				.Include(m=>m.Orders)
				.Select(m=>new MasterViewModel{MasterId = m.MasterId,
					UniqueDescription = m.UniqueDescription, OrdersCount = m.Orders.Count()})
				.ToListAsync();

			suitableMasters.OrderBy(m => m.OrdersCount);

			var masters = new List<MasterViewModel>();
			foreach (var master in suitableMasters)
			{
				master.UniqueDescription += $"; Orders count = {master.OrdersCount}";
				masters.Add(_mapper.Map<MasterViewModel>(master));
			}
			ViewBag.Masters = masters; 
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder(CompoundOrderViewModel covm)
		{
			if (!ModelState.IsValid)
			{
				return View(covm);
			}

			if (covm.Device.OnGuarantee)
			{
				covm.Order.AmountToPay = 0;
			}

			var client = _mapper.Map<Client>(covm.Client);

			var device = _mapper.Map<Device>(covm.Device);

			var master = _db.Masters.Find(covm.Master.MasterId);

			client.Devices.Add(device);
			device.Master = master;
			device.Client = client;
			master.Devices.Add(device);

			var order = _mapper.Map<Order>(covm.Order);

			order.Client = client;
			order.Device = device;
			order.Master = master;

			client.Orders.Add(order);
			device.Order = order;
			master.Orders.Add(order);

			await _db.Clients.AddAsync(client);
			await _db.Devices.AddAsync(device);
			await _db.Orders.AddAsync(order);

			await _db.SaveChangesAsync();

			return RedirectToAction("ManageOrder", "Order");
		}
	}
}
