using System.ComponentModel.DataAnnotations;

namespace KudryavtsevAlexey.ServiceCenter.Enums
{
	public enum DeviceType
	{
		[Display(Name = "Computer technology")]
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
