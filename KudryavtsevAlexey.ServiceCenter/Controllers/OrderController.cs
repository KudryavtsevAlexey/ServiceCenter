using KudryavtsevAlexey.ServiceCenter.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
	public class OrderController : Controller
	{
		private readonly ApplicationContext _db;

		public OrderController(ApplicationContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> ManageOrder()
		{
			var orders = await _db.Orders
				.Include(order => order.Client)
				.Include(order => order.Device)
				.Include(order => order.Master)
				.ToListAsync();
			return View(orders);
		}

		public IActionResult CheckOrderStatus()
		{
			return View();
		}
	}
}
