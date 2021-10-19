using KudryavtsevAlexey.ServiceCenter.ViewModels;

namespace ServiceCenter.Tests.Helpers
{
	public class RegisterHelper
    {
        public static RegisterViewModel GetRegisterViewModel()
		{
			return new RegisterViewModel()
			{
				FirstName = "FirstName",
				LastName = "LastName",
				Email = "ExpectedEmail@test.com",
				Password = "ExpectedPassword1234",
				ConfirmedPassword = "ExpectedPassword1234"
			};
		}
    }
}
