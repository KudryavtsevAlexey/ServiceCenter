namespace ServiceCenter.Tests.Helpers
{
	public class UserManagerHelper
{
        public static FakeApplicationUser GetApplicationUser()
{
			return new FakeApplicationUser
			{
				UserName = "FirstNameLastName",
				FirstName = "FirstName",
				LastName = "LastName",
				Email = "ExpectedEmail@test.com",
				Password = "ExpectedPassword1234",
				PasswordHash = "ExpectedPassword1234",
			};
		}
    }
}
