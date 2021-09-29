using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="First name field is required"), Display(Name ="First name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last name field is required"), Display(Name ="Last name")]
		public string LastName { get; set; }

		[Required,EmailAddress(ErrorMessage ="Please, enter the valid value")]
		public string Email { get; set; }

		[Required(ErrorMessage ="Password field is required"), DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirmed password field is required"), DataType(DataType.Password),
			Compare("Password"), Display(Name ="Confirmed password")]
		public string ConfirmedPassword { get; set; }
	}
}
