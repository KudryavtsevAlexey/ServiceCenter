using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Integrations
{
	public class GitHubClientIntegration : IGitHubClientIntegration
	{
		public GitHubClient GetClient()
		{
			var client = new GitHubClient(new ProductHeaderValue("ServiceCenter"));

			var tokenAuth = new Credentials("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");
			client.Credentials = tokenAuth;

			return client;
		}

		public async Task<List<ProjectCard>> GetProjectCards(GitHubClient client, int serviceCenterProjectId)
		{
			var columns = await client.Repository.Project.Column
				.GetAll(serviceCenterProjectId);

			var cards = new List<ProjectCard>();

			foreach (var column in columns)
			{
				var columnCards = await client.Repository.Project.Card
					.GetAll(column.Id);

				cards.AddRange(columnCards);
			}

			return cards;
		}
	}
}
