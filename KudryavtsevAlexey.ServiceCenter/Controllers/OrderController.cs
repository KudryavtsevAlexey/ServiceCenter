using Microsoft.AspNetCore.Mvc;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
	public class OrderController : Controller
	{
		public IActionResult CheckOrderStatus()
		{
			return View();
		}
	}
}
