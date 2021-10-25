using KudryavtsevAlexey.ServiceCenter.Data;

namespace ServiceCenter.Tests.Helpers.AccountHelpers
{
    public class FakeApplicationUser : ApplicationUser
    {
        public string Password { get; set; }
	}
}
