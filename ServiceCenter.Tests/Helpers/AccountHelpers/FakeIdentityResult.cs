using Microsoft.AspNetCore.Identity;

namespace ServiceCenter.Tests.Helpers.AccountHeplers
{
	public class FakeIdentityResult : IdentityResult
    {
		public FakeIdentityResult(bool succeeded)
		{
			Succeeded = succeeded;
		}
    }
}
