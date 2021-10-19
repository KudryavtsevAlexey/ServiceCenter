using Microsoft.AspNetCore.Identity;

namespace ServiceCenter.Tests.Helpers
{
	public class FakeIdentityResult : IdentityResult
    {
		public FakeIdentityResult(bool succeeded)
		{
			Succeeded = succeeded;
		}
    }
}
