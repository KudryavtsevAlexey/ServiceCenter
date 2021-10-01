using KudryavtsevAlexey.ServiceCenter.Enums;
using System;

namespace KudryavtsevAlexey.ServiceCenter.Models
{
	public class Order
	{
		public int OrderId { get; set; }
		public int ClientId { get; set; }
		public Client Client { get; set; }
		public int DeviceId { get; set; }
		public Device Device { get; set; }
		public int MasterId { get; set; }
		public Master Master { get; set; }
		public decimal AmountToPay { get; set; }
		public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
		public Status Status { get; set; }
	}
}
