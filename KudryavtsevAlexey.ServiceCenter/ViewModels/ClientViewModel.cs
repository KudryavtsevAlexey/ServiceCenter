using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class ClientViewModel
    {
        [EmailAddress ,Required]
		public string Email { get; set; }
		[Display(Name ="First name"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name"), Required]
        public string LastName { get; set; }
    }
}
