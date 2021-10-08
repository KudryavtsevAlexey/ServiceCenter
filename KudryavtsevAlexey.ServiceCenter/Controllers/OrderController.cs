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
		private readonly ApplicationContext _db;
		private readonly IMapper _mapper;
		private readonly IMasterService _masterService;
		private readonly IOrderService _orderService;

		public OrderController(ApplicationContext db, IMapper mapper, IMasterService masterService, IOrderService orderService)
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

			await _orderService.MapOrder(covm);

			return RedirectToAction("ManageOrder", "Panel");
		}

		public async Task<IActionResult> MoreDetails(int? id)
		{
			if (id==null)
			{
				return NotFound();
			}

			var device = await _db.Devices
				.FirstOrDefaultAsync(d => d.Order.OrderId == id);
				

			if (device!=null)
			{
				var dvm = _mapper.Map<DeviceViewModel>(device);
				return View(dvm);
			}

			return NotFound();
		}

		public async Task<IActionResult> EditOrder(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var order = await _db.Orders
				.Include(o=>o.Client)
				.Include(o=>o.Device)
				.Include(o=>o.Master)
				.FirstOrDefaultAsync(o=>o.OrderId == id);

			if (order!=null)
			{
				var covm = new CompoundOrderViewModel 
				{
					Client = _mapper.Map<ClientViewModel>(order.Client),
					Device = _mapper.Map<DeviceViewModel>(order.Device),
					Master = _mapper.Map<MasterViewModel>(order.Master),
					Order = _mapper.Map<OrderViewModel>(order),
				};

				ViewBag.Masters = await _masterService.GetAllMasters();

				return View(covm);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> EditOrder(CompoundOrderViewModel covm)
		{
			if (!ModelState.IsValid)
			{
				return View(covm);
			}

			if (covm.Device.OnGuarantee)
			{
				covm.Order.AmountToPay = 0;
			}

			var orderToDelete = await _db.Orders.FindAsync(covm.Order.OrderId);
			_db.Orders.Remove(orderToDelete);

			await _orderService.MapOrder(covm);

			return RedirectToAction("ManageOrder", "Panel");
		}

		public async Task<IActionResult> DeleteOrder(int? id)
		{
			if (id==null)
			{
				return NotFound();
			}

			var order = await _db.Orders.FindAsync(id);

			_db.Remove(order);
			await _db.SaveChangesAsync();

			return RedirectToAction("ManageOrder", "Panel");
		}

		[AllowAnonymous]
		public IActionResult CheckOrderStatus()
		{
			return View();
		}

		[HttpPost, AllowAnonymous]
		public async Task<IActionResult> CheckOrderStatusAsync(ClientViewModel client)
		{
			if (client == null)
			{
				return NotFound();
			}

			var order = await _db.Orders
				.Include(o => o.Client)
				.Include(o=>o.Device)
				.Include(o=>o.Master)
				.FirstOrDefaultAsync(o => o.Client.Email == client.Email
				&& o.Client.FirstName.ToLower() == client.FirstName.ToLower()
				&& o.Client.LastName.ToLower() == client.LastName.ToLower());

			if (order!=null)
			{
				var orderStatus = _mapper.Map<CompoundOrderViewModel>(order);

				orderStatus.Client = _mapper.Map<ClientViewModel>(order.Client);
				orderStatus.Device = _mapper.Map<DeviceViewModel>(order.Device);
				orderStatus.Master = _mapper.Map<MasterViewModel>(order.Master);
				orderStatus.Order = _mapper.Map<OrderViewModel>(order);

				return View("ShowOrderStatus", orderStatus);
			}
			return View(client);
		}
	}
}
