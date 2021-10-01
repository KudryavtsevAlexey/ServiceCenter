using Microsoft.AspNetCore.Mvc;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
