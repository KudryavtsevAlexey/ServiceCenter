using AutoFixture;

using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;

using ServiceCenter.Tests.Helpers.DataHelpers;

using System.Collections.Generic;
using System.Linq;

namespace ServiceCenter.Tests.Helpers.OrderHelpers
{
    public class MasterHelper
    {
        private static List<Device> devicesCopy = DataHelper.GetManyDevices();
        private static List<Order> ordersCopy = DataHelper.GetManyOrders();

        public static MasterViewModel GetMasterViewModel()
        {
            return new MasterViewModel()
            {
                MasterId = 1,
                FirstName = "FirstName1",
                LastName = "LastName1",
                OrdersCount = 1,
                UniqueDescription = "FirstName1LastName1 Orders count: 1",
            };
        }

        public static List<MasterViewModel> GetManyMastersViewModels()
        {
            return new List<MasterViewModel>()
            {
                new MasterViewModel()
				{
                    MasterId = 1,
                    FirstName = "FirstName1",
                    LastName = "LastName1",
                    OrdersCount = 1,
                    UniqueDescription = "FirstName1LastName1 Orders count: 1"
				},
                new MasterViewModel()
                {
                    MasterId = 2,
                    FirstName = "FirstName2",
                    LastName = "LastName2",
                    OrdersCount = 1,
                    UniqueDescription = "FirstName2LastName2 Orders count: 1"
                },
                new MasterViewModel()
                {
                    MasterId = 3,
                    FirstName = "FirstName3",
                    LastName = "LastName3",
                    OrdersCount = 1,
                    UniqueDescription = "FirstName3LastName3 Orders count: 1"
                },
            };
        }
    }
}
