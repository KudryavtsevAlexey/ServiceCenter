using System.Collections.Generic;

namespace KudryavtsevAlexey.ServiceCenter.Models
{
	public class Client
	{
		public int ClientId { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<Order> Orders { get; set; } = new List<Order>();
		public ICollection<Device> Devices { get; set; } = new List<Device>();
	}
}
