using KudryavtsevAlexey.ServiceCenter.Enums;
using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class OrderViewModel
	{
		public int OrderId { get; set; }
		[Display(Name ="Amount to pay"), Required]
		public decimal AmountToPay { get; set; }
		[Required]
		public Status Status { get; set; }
	}
}
