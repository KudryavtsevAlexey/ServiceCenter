using Microsoft.AspNetCore.Identity;

namespace ServiceCenter.Tests.Helpers
{
	public class FakeSignInResult : SignInResult
    {
		public FakeSignInResult(bool succeeded)
		{
			Succeeded = succeeded;
		}
	}
}
