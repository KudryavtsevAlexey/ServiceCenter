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

		public async Task<IActionResult> CreateOrderAsync()
		{
			var suitableMasters = await _db.Masters.Where(m=>m.Order==null).ToListAsync();
			if (suitableMasters.Count()!=0)
			{
				var masters = new List<MasterViewModel>();
				foreach (var master in suitableMasters)
				{
					masters.Add(_mapper.Map<MasterViewModel>(master));
				}
				ViewBag.Masters = masters;
			}
			return View();
		}

		[HttpPost]
		public IActionResult CreateOrder(CompoundOrderViewModel covm)
		{
			if (!ModelState.IsValid)
			{
				return View(covm);
			}

			if (covm.Device.OnGuarantee)
			{
				covm.Order.AmountToPay = 0;
			}

			//var client = new Client 
			//{
			//	Email = covm.Client.Email,
			//	FirstName = covm.Client.FirstName,
			//	LastName = covm.Client.LastName,
			//	Devices = new List<Device>(),
			//};

			//var device = new Device 
			//{
			//	Name = covm.Device.Name,
			//	Type = covm.Device.Type,
			//	ProblemDescription = covm.Device.ProblemDescription,
			//	Client = client,
			//	OnGuarantee = covm.Device.OnGuarantee,
			//};
			var client = _mapper.Map<Client>(covm.Client);

			var device = _mapper.Map<Device>(covm.Device);

			client.Devices = new List<Device>();

			var master = _db.Masters.Find(covm.Master.MasterId);
			master.Devices = new List<Device>();

			client.Devices.Add(device);
			device.Master = master;
			device.Client = client;
			master.Devices.Add(device);

			var order = _mapper.Map<Order>(covm.Order);

			order.Client = client;
			order.Device = device;
			order.Master = master;

			client.Order = order;
			device.Order = order;
			master.Order = order;

			_db.Clients.Add(client);
			_db.Devices.Add(device);
			_db.Orders.Add(order);

			_db.SaveChanges();

			return RedirectToAction("Order", "ManageOrder");
		}
	}
}
