using KudryavtsevAlexey.ServiceCenter.Enums;
using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class OrderViewModel
	{
		[Display(Name ="Amount to pay"), Required]
		public decimal AmountToPay { get; set; }
		[Required]
		public Status Status { get; set; }
	}
}
