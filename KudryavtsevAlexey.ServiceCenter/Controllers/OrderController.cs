using AutoMapper;
using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.Services;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
    [Authorize(Policy ="Master")]
	public class OrderController : Controller
	{
		private readonly IContext _db;
		private readonly IMapper _mapper;
		private readonly IMasterService _masterService;
		private readonly IOrderService _orderService;

		public OrderController(IContext db, IMapper mapper, IMasterService masterService, IOrderService orderService)
		{
			_db = db;
			_mapper = mapper;
			_masterService = masterService;
			_orderService = orderService;
		}

		public async Task<IActionResult> CreateOrder()
		{
			ViewBag.Masters = await _masterService.GetAllMasters();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder(OrderViewModel ovm)
		{
			if (ModelState.IsValid)
			{
				if (ovm.Device.OnGuarantee)
				{
					ovm.AmountToPay = 0;
				}

				var order = await _orderService.MapOrder(ovm);

				await _db.Clients.AddAsync(order.Client);
				await _db.Devices.AddAsync(order.Device);
				await _db.Orders.AddAsync(order);

				await _db.CustomSaveChangesAsync();

				return RedirectToAction("ManageOrder", "Panel");
			}

			return View(ovm);
		}

		public async Task<IActionResult> MoreDetails(int? id)
		{
			if (id != null)
			{
				var device = await _db.Devices.FirstOrDefaultAsync(d=>d.Order.OrderId == id);

				if (device != null)
				{
					var deviceViewModel = _mapper.Map<DeviceViewModel>(device);

					return View(deviceViewModel);
				}
			}

			return NotFound();
		}

		public async Task<IActionResult> EditOrder(int? id)
		{
			if (id != null)
			{
				var order = await _db.Orders
				.Include(c=>c.Client)
				.Include(d=>d.Device)
				.Include(m=>m.Master)
				.FirstOrDefaultAsync(o => o.OrderId == id);

				if (order != null)
				{
					var model = new OrderViewModel()
					{
						OrderId = order.OrderId,
						Client = _mapper.Map<ClientViewModel>(order.Client),
						Device = _mapper.Map<DeviceViewModel>(order.Device),
						Master = _mapper.Map<MasterViewModel>(order.Master),
						AmountToPay = order.AmountToPay,
						Status = order.Status,
					};

					ViewBag.Masters = await _masterService.GetAllMasters();

					return View(model);
				}
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> EditOrder(OrderViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Device.OnGuarantee)
				{
					model.AmountToPay = 0;
				}

				var orderToDelete = await _db.Orders.FindAsync(model.OrderId);
				_db.Orders.Remove(orderToDelete);

				var order = await _orderService.MapOrder(model);

				_db.Orders.Add(order);
				await _db.CustomSaveChangesAsync();

				return RedirectToAction("ManageOrder", "Panel");
			}
			return View(model);
		}

		public async Task<IActionResult> DeleteOrder(int? id)
		{
			if (id != null)
			{
				var order = await _db.Orders.FindAsync(id);

				_db.Orders.Remove(order);
				await _db.CustomSaveChangesAsync();

				return RedirectToAction("ManageOrder", "Panel");
			}

			return NotFound();
		}

		[AllowAnonymous]
		public IActionResult CheckOrderStatus()
		{
			return View();
		}

		[HttpPost, AllowAnonymous]
		public async Task<IActionResult> CheckOrderStatusAsync(ClientViewModel client)
		{
			if (client != null)
			{
				var order = await _db.Orders
				.Include(o => o.Client)
				.Include(o => o.Device)
				.Include(o => o.Master)
				.FirstOrDefaultAsync(o => o.Client.Email == client.Email
										  && o.Client.FirstName.ToLower() == client.FirstName.ToLower()
										  && o.Client.LastName.ToLower() == client.LastName.ToLower());

				if (order != null)
				{
					var orderStatus = _mapper.Map<OrderViewModel>(order);

					orderStatus.Client = _mapper.Map<ClientViewModel>(order.Client);
					orderStatus.Device = _mapper.Map<DeviceViewModel>(order.Device);
					orderStatus.Master = _mapper.Map<MasterViewModel>(order.Master);
					orderStatus.AmountToPay = order.AmountToPay;
					orderStatus.Status = order.Status;

					return View("ShowOrderStatus", orderStatus);
				}
			}

			return View(client);
		}
	}
}
