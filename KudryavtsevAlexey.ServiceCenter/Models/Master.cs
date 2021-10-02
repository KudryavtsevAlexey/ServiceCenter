using System.Collections.Generic;

namespace KudryavtsevAlexey.ServiceCenter.Models
{
	public class Master
	{
		public int MasterId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Order Order { get; set; }
		public string UniqueDescription { get; set; }
		public List<Device> Devices { get; set; }
	}
}
