using Microsoft.AspNetCore.Mvc;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }   

        public IActionResult Index() 
        {
            return View();
        }
    }
}
