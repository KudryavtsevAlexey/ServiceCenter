using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class ClientIdentificationViewModel
	{
		[Required, Display(Name = "Your first name")]
		public string ClientFirstName { get; set; }
		[Required, Display(Name = "Your last name")]
		public string ClientLastName { get; set; }
		[EmailAddress, Required, Display(Name = "Your email")]
		public string CLientEmail { get; set; }
	}
}
