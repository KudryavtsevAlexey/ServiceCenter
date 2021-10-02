using KudryavtsevAlexey.ServiceCenter.Enums;
using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class DeviceViewModel
	{
		[Required]
		public string Name { get; set; }

		[Display(Name = "Problem description"), Required, MinLength(20), MaxLength(300)]
		public string ProblemDescription { get; set; }

		[Required]
		public DeviceType Type { get; set; }

		[Display(Name ="On guarantee"), Required]
		public bool OnGuarantee { get; set; }
	}
}
