namespace KudryavtsevAlexey.ServiceCenter.ViewModels
{
	public class CompoundOrderViewModel
	{
		public ClientViewModel Client { get; set; }

		public DeviceViewModel Device { get; set; }

		public MasterViewModel Master { get; set; }

		public OrderViewModel Order { get; set; }
	}
}
