using KudryavtsevAlexey.ServiceCenter.Enums;

namespace KudryavtsevAlexey.ServiceCenter.Models
{
	public class Device
	{
		public int DeviceId { get; set; }
		public string Name { get; set; }
		public string ProblemDescription { get; set; }
		public int ClientId { get; set; }
		public Client Client { get; set; }
		public int MasterId { get; set; }
		public Master Master { get; set; }
		public Order Order { get; set; }
		public DeviceType Type { get; set; }
		public bool OnGuarantee { get; set; }
	}
}
