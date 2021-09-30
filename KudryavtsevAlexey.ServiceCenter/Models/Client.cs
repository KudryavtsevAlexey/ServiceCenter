using System.Collections.Generic;

namespace KudryavtsevAlexey.ServiceCenter.Models
{
	public class Client
	{
		public int ClientId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Order Order { get; set; }
		public IEnumerable<Device> Devices { get; set; }
	}
}
