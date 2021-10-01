using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

		public IActionResult CreateOrder()
		{
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
				FirstName = covm.Client.FirstName,
				LastName = covm.Client.LastName,
				Devices = new List<Device>(), //TODO: Right initialization
				Order = new Order(), //TODO: Right initialization
			};

			var device = new Device 
			{
				Type = covm.Device.Type,
				ProblemDescription = covm.Device.ProblemDescription,
				Client = new Client(), //TODO: Right initialization
				Master = new Master(), //TODO: Right initialization
				OnGuarantee = covm.Device.OnGuarantee,
				Order = new Order(), //TODO: Right initialization
			};

			var master = new Master 
			{
				FirstName = covm.Master.FirstName,
				LastName = covm.Master.LastName,
				Devices = new List<Device>(), //TODO: Right initialization
				Order = new Order(), //TODO: Right initialization
			};

			var order = new Order
			{
				CreatedAt = DateTime.UtcNow,
				AmountToPay = covm.Order.AmountToPay,
				Status = covm.Order.Status,
				Client = new Client(), //TODO: Right initialization
				Device = new Device(), //TODO: Right initialization
				Master = new Master(), //TODO: Right initialization
			};

			return View();
		}
	}
}
