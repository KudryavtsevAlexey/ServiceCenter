using System.Collections.Generic;

using AutoFixture;

using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;

using ServiceCenter.Tests.Helpers.DataHelpers;

namespace ServiceCenter.Tests.Helpers.OrderHelpers
{
    public class DeviceHelper
    {
        private static List<Master> mastersCopy = DataHelper.GetManyMasters();
        private static List<Order> ordersCopy = DataHelper.GetManyOrders();

        public static DeviceViewModel GetDeviceViewModel()
		{
            return new DeviceViewModel()
            {
                DeviceId = 1,
                ProblemDescription = "ProblemDescription1",
                Name = "DeviceName",
                OnGuarantee = false,
                Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.ComputerTechnology,
            };
		}

        public static List<DeviceViewModel> GetManyDeviceViewModels()
		{
            return new List<DeviceViewModel>()
            {
                new DeviceViewModel()
				{
                    DeviceId = 1,
                    ProblemDescription = "ProblemDescription1",
                    Name = "DeviceName1",
                    OnGuarantee = false,
                    Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.ComputerTechnology
				},
                new DeviceViewModel()
                {
                    DeviceId = 2,
                    ProblemDescription = "ProblemDescription2",
                    Name = "DeviceName2",
                    OnGuarantee = true,
                    Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.Electronics
                },
                new DeviceViewModel()
                {
                    DeviceId = 3,
                    ProblemDescription = "ProblemDescription3",
                    Name = "DeviceName3",
                    OnGuarantee = false,
                    Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.SmallHouseholdAppliances
                },
            };
		}
    }
}
