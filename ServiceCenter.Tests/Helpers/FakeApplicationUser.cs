using KudryavtsevAlexey.ServiceCenter.Data;

namespace ServiceCenter.Tests.Helpers
{
	public class FakeApplicationUser : ApplicationUser
    {
		public string Password { get; set; }
	}
}
