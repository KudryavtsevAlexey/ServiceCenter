using AutoMapper;
using KudryavtsevAlexey.ServiceCenter.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
	public class OrderController : Controller
	{
		private readonly ApplicationContext _db;
		private readonly IMapper _mapper;

		public OrderController(ApplicationContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		public async Task<IActionResult> ManageOrder()
		{
			var orders = await _db.Orders
				.Include(o => o.Client)
				.Include(o => o.Device)
				.Include(o => o.Master)
				.ToListAsync();
			return View(orders);
		}

		public async Task<IActionResult> MoreDetailsAsync(int? id)
		{
			if (id==null)
			{
				return NotFound();
			}

			var order = await _db.Orders
				.Include(o => o.Device)
				.FirstOrDefaultAsync(o => o.OrderId == id);
				

			if (order!=null)
			{
				return View(order);
			}

			return NotFound();
		}

		public IActionResult CheckOrderStatus()
		{
			return View();
		}
	}
}
