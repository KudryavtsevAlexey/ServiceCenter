using KudryavtsevAlexey.ServiceCenter.Enums;

namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class OrderStatusViewModel
	{
		public string ClientEmail { get; set; }
		public string ClientFirstName { get; set; }
		public string ClientLastName { get; set; }
		public DeviceType Type { get; set; }
		public string DeviceName { get; set; }
		public bool OnGuarantee { get; set; }
		public string ProblemDescription { get; set; }
		public Status OrderStatus { get; set; }
		public decimal AmountToPay { get; set; }
	}
}
