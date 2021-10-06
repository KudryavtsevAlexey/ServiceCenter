using System.ComponentModel;

namespace KudryavtsevAlexey.ServiceCenter.Enums
{
	public enum DeviceType
	{
		[Description("Computer technology")]
		ComputerTechnology,
		[Description("Office equipment")]
		OfficeEquipment,
		[Description("Large home appliances")]
		LargeHomeAppliances,
		[Description("Small household appliances")]
		SmallHouseholdAppliances,
		[Description("Electronics")]
		Electronics,
	}
}
