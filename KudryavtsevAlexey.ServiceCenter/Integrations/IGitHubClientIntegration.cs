using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Integrations
{
	public interface IGitHubClientIntegration
	{
		public GitHubClient GetClient();

		public Task<List<ProjectCard>> GetProjectCards(GitHubClient client, int serviceCenterProjectId);
	}
}
