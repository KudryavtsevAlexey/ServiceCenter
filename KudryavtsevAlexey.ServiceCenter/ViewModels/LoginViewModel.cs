using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class LoginViewModel
	{

		[EmailAddress, Required]
		public string Email { get; set; }
		[DataType(DataType.Password), Required]
		public string Password { get; set; }
	}
}
