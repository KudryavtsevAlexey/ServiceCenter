using KudryavtsevAlexey.ServiceCenter.Data;
using Microsoft.AspNetCore.Mvc;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
	public class OrderController : Controller
    {
		private readonly ApplicationContext _db;

		public OrderController(ApplicationContext db)
		{
			_db = db;
		}

        public IActionResult CheckOrderStatus()
		{
            return View();
		}
    }
}
