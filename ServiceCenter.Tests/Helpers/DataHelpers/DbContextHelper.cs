using System.Collections.Generic;

using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using ServiceCenter.Tests.Helpers.DataHelpers;

namespace ServiceCenter.Tests.Helpers.DataHelpers
{
	public class DbContextHelper
	{
		private static List<Client> clientsCopy = DataHelper.GetManyClients();
		private static List<Device> devicesCopy = DataHelper.GetManyDevices();
		private static List<Master> mastersCopy = DataHelper.GetManyMasters();
		private static List<Order> ordersCopy = DataHelper.GetManyOrders();

		public ApplicationContext Context { get; set; }

		public DbContextHelper()
		{
			var builder = new DbContextOptionsBuilder<ApplicationContext>();
			builder.UseInMemoryDatabase("TESTDB")
				.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
			var options = builder.Options;

			Context = new ApplicationContext(options);

			var clients = DataHelper.GetManyClients();
			var devices = DataHelper.GetManyDevices();
			var masters = DataHelper.GetManyMasters();
			var orders = DataHelper.GetManyOrders();

			//clients[0].Devices.AddRange(devices);
			//clients[0].Orders.AddRange(orders);

			//masters[0].Devices.AddRange(devices);
			//masters[0].Orders.AddRange(orders);

			//TODO: Разобраться с инстансами
			Context.Clients.AddRange(clients);
			Context.Devices.AddRange(devices);
			Context.Masters.AddRange(masters);
			Context.Orders.AddRange(orders);

			Context.SaveChanges();
		}
	}
}
