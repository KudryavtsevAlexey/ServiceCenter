using System.Collections.Generic;
using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;

using ServiceCenter.Tests.Helpers.DataHelpers;

namespace ServiceCenter.Tests.Helpers.OrderHelpers
{
    public class ClientHelper
    {
        private static List<Device> devicesCopy = DataHelper.GetManyDevices();
        private static List<Order> ordersCopy = DataHelper.GetManyOrders();

        public static ClientViewModel GetClientViewModel()
		{
            return new ClientViewModel()
            {
                Email = "ExpectedEmail1@mail.ru",
                FirstName = "FirstName1",
                LastName = "LastName1",
            };
		}

        public static List<ClientViewModel> GetManyClientViewModels()
		{
            return new List<ClientViewModel>()
            {
                new ClientViewModel()
				{
                    Email = "ExpectedEmail1@mail.ru",
                    FirstName = "FirstName1",
                    LastName = "LastName1"
				},
                new ClientViewModel()
                {
                    Email = "ExpectedEmail2@mail.ru",
                    FirstName = "FirstName2",
                    LastName = "LastName2"
                },
                new ClientViewModel()
                {
                    Email = "ActualEmail1@mail.ru",
                    FirstName = "ActualFirstName1",
                    LastName = "ActualLastName1"
                },
            };
		}
    }
}
