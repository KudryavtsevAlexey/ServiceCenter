using System.Collections.Generic;

namespace KudryavtsevAlexey.ServiceCenter.Models
{
	public class Master
	{
		public int MasterId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<Order> Orders { get; set; } = new List<Order>();
		public string UniqueDescription { get; set; }
		public ICollection<Device> Devices { get; set; } = new List<Device>();
	}
}
