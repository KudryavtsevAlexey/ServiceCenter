using KudryavtsevAlexey.ServiceCenter.Integrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
	public class AboutController : Controller
    {
		private readonly IGitHubClientIntegration _gitHub;
		private readonly IConfiguration _configuration;

		public AboutController(IGitHubClientIntegration gitHub, IConfiguration configuration)
		{
			_gitHub = gitHub;
			_configuration = configuration;
		}

        public async Task<IActionResult> AboutStateOfProject()
{
			var client = _gitHub.GetClient();

			var projects = await client
				.Repository
				.Project
				.GetAllForRepository("KudryavtsevAlexey", "ServiceCenter");

			var serviceCenterProject = projects.FirstOrDefault(x=>x.Name == "ServiceCenter");

			if (serviceCenterProject != null)
			{
				var cards = await _gitHub
					.GetProjectCards(client, serviceCenterProject.Id);

				return View(cards);
			}

			return RedirectToAction("Index", "Home");
		}

        public async Task<IActionResult> AboutProjectCreator()
        {
	        var client = _gitHub.GetClient();

	        var repositories = await client.Repository.GetAllForUser("KudryavtsevAlexey");

	        return View(repositories);
        }
    }
}
