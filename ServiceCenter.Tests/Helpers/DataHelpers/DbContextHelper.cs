using KudryavtsevAlexey.ServiceCenter.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using ServiceCenter.Tests.Helpers.OrderHelpers;

namespace ServiceCenter.Tests.Helpers.DataHelpers
{
	public class DbContextHelper
	{
		public ApplicationContext Context { get; set; }

		public DbContextHelper()
		{
			var builder = new DbContextOptionsBuilder<ApplicationContext>();
			builder.UseInMemoryDatabase("TESTDB")
				.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
			var options = builder.Options;

			Context = new ApplicationContext(options);

			var clients = ClientHelper.GetManyClients();
			var devices = DeviceHelper.GetManyDevices();
			var masters = MasterHelper.GetManyMasters();
			var orders = OrderHelper.GetManyOrders();

			Context.Clients.Add(ClientHelper.GetClient());
			Context.Clients.AddRange(clients);
			Context.Devices.Add(DeviceHelper.GetDevice());
			Context.Devices.AddRange(devices);
			Context.Masters.Add(MasterHelper.GetMaster());
			Context.Masters.AddRange(masters);
			Context.Orders.Add(OrderHelper.GetOrder());
			Context.Orders.AddRange(orders);

			Context.SaveChanges();
		}
	}
}
