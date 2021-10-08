using System.ComponentModel.DataAnnotations;
using KudryavtsevAlexey.ServiceCenter.Enums;

namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class OrderViewModel
	{
		public ClientViewModel Client { get; set; }
		public DeviceViewModel Device { get; set; }
		public MasterViewModel Master { get; set; }
		public int OrderId { get; set; }
		[Display(Name ="Amount to pay"), Required]
		public decimal AmountToPay { get; set; }
		[Required]
		public Status Status { get; set; }
	}
}
