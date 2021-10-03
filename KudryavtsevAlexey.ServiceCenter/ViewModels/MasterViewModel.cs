using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class MasterViewModel
	{
		[Required]
		public int MasterId { get; set; }
		
		public string FirstName { get; set; }

		public string UniqueDescription { get; set; }

		public string LastName { get; set; }
	}
}
