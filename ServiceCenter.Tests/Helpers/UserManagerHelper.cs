namespace ServiceCenter.Tests.Helpers
{
	public class UserManagerHelper
    {
        public static FakeApplicationUser GetApplicationUser()
		{
            return new FakeApplicationUser() 
            {
                Email = "ExpectedEmail@test.com",
                Password = "ExpectedPassword1234"
            };
		}
    }
}
