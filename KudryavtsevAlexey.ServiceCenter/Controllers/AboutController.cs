using KudryavtsevAlexey.ServiceCenter.Integrations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
	public class AboutController : Controller
    {
		private readonly IGitHubClientIntegration _gitHub;

		public AboutController(IGitHubClientIntegration gitHub)
		{
			_gitHub = gitHub;
		}

        public async Task<IActionResult> AboutStateOfProject()
		{
			var client = _gitHub.GetClient();

			var projects = await client
				.Repository
				.Project
				.GetAllForRepository("KudryavtsevAlexey", "ServiceCenter");

			var serviceCenterProject = projects.FirstOrDefault(x=>x.Name == "ServiceCenter");

			var cards = await _gitHub
				.GetProjectCards(client, serviceCenterProject.Id);

			return View(cards);
		}
    }
}
