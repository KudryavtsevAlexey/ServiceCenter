using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class RegisterViewModel
	{
		[Required, Display(Name ="First name")]
		public string FirstName { get; set; }
		[Required, Display(Name ="Last name")]
		public string LastName { get; set; }
		[Required,EmailAddress]
		public string Email { get; set; }
		[Required, DataType(DataType.Password)]
		public string Password { get; set; }
		[Required, DataType(DataType.Password), Compare("Password"), Display(Name ="Confirmed password")]
		public string ConfirmedPassword { get; set; }
	}
}
