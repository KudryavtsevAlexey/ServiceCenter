using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
	[Authorize(Policy ="Master")]
	public class PanelController : Controller
	{
		private readonly ApplicationContext _db;

		public PanelController(ApplicationContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> CreateOrderAsync()
		{
			var suitableMasters = await _db.Masters.Where(m=>m.Order==null).ToListAsync();
			ViewBag.Masters = suitableMasters;
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

			var client = new Client 
			{
				Email = covm.Client.Email,
				FirstName = covm.Client.FirstName,
				LastName = covm.Client.LastName,
				Devices = new List<Device>(),
			};

			var device = new Device 
			{
				Name = covm.Device.Name,
				Type = covm.Device.Type,
				ProblemDescription = covm.Device.ProblemDescription,
				Client = client,
				OnGuarantee = covm.Device.OnGuarantee,
			};

			var master = new Master 
			{
				FirstName = covm.Master.FirstName,
				LastName = covm.Master.LastName,
				Devices = new List<Device>(),
			};

			client.Devices.Add(device);
			device.Master = master;
			device.Client = client;
			master.Devices.Add(device);

			var order = new Order
			{
				CreatedAt = DateTime.UtcNow,
				AmountToPay = covm.Order.AmountToPay,
				Status = covm.Order.Status,
				Client = client,
				Device = device,
				Master = master,
			};

			client.Order = order;
			device.Order = order;
			master.Order = order;

			_db.Clients.Add(client);
			_db.Devices.Add(device);
			_db.Masters.Add(master);
			_db.Orders.Add(order);

			_db.SaveChanges();

			return RedirectToAction("Order", "ManageOrder");
		}
	}
}
