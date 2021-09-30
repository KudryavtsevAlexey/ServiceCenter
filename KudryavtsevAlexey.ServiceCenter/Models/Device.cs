using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.Models
{
	public class Device
	{
		public int DeviceId { get; set; }
		public string ProblemDescription { get; set; }
		public int ClientId { get; set; }
		public Client Client { get; set; }
		public int MasterId { get; set; }
		public Master Master { get; set; }
		public Order Order { get; set; }
		public enum DeviceType
		{
			[Display(Name ="Computer technology")]
			ComputerTechnology,
			[Display(Name = "Office equipment")]
			OfficeEquipment,
			[Display(Name = "Large household appliances")]
			LargeHomeAppliances,
			[Display(Name = "Small household appliances")]
			SmallHouseholdAppliances,
			Electronics,
		}
	}
}
