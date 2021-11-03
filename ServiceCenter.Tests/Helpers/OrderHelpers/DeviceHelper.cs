using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;

namespace ServiceCenter.Tests.Helpers.OrderHelpers
{
    public class DeviceHelper
    {
        public static Device GetDevice()
		{
			return new Device()
			{
				DeviceId = 1,
				Name = "DeviceName1",
				ProblemDescription = "ProblemDescription1",
				ClientId = 1,
				MasterId = 1,
				OnGuarantee = false,
				Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.ComputerTechnology,
			};
		}

        public static DeviceViewModel GetDeviceViewModel()
		{
			return new DeviceViewModel()
			{
				DeviceId = 1,
				Name = "DeviceName1",
				ProblemDescription = "ProblemDescription1",
				OnGuarantee = false,
				Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.ComputerTechnology,
			};
		}

        public static List<Device> GetManyDevices()
		{
			return new List<Device>()
			{
				new Device()
				{
					DeviceId = 2,
					Name = "DeviceName2",
					ProblemDescription = "ProblemDescription2",
					ClientId = 2,
					MasterId = 2,
					OnGuarantee = true,
					Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.ComputerTechnology,
				},

				new Device()
				{
					DeviceId = 3,
					Name = "DeviceName3",
					ProblemDescription = "ProblemDescription3",
					ClientId = 3,
					MasterId = 3,
					OnGuarantee = true,
					Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.SmallHouseholdAppliances,
				},

				new Device()
				{
					DeviceId = 4,
					Name = "DeviceName4",
					ProblemDescription = "ProblemDescription4",
					ClientId = 4,
					MasterId = 4,
					OnGuarantee = false,
					Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.OfficeEquipment,
				},

				new Device()
				{
					DeviceId = 5,
					Name = "DeviceName5",
					ProblemDescription = "ProblemDescription5",
					ClientId = 5,
					MasterId = 5,
					OnGuarantee = false,
					Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.LargeHomeAppliances,
				},

				new Device()
				{
					DeviceId = 6,
					Name = "DeviceName6",
					ProblemDescription = "ProblemDescription6",
					ClientId = 6,
					MasterId = 6,
					OnGuarantee = true,
					Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.Electronics,
				},
			};
		}

		public static List<DeviceViewModel> GetManyDeviceViewModels()
		{
			return new List<DeviceViewModel>()
			{
				new DeviceViewModel()
				{
					DeviceId = 2,
					Name = "DeviceName2",
					ProblemDescription = "ProblemDescription2",
					OnGuarantee = true,
					Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.ComputerTechnology,
				},

				new DeviceViewModel()
				{
					DeviceId = 3,
					Name = "DeviceName3",
					ProblemDescription = "ProblemDescription3",
					OnGuarantee = true,
					Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.Electronics,
				},

				new DeviceViewModel()
				{
					DeviceId = 4,
					Name = "DeviceName4",
					ProblemDescription = "ProblemDescription4",
					OnGuarantee = false,
					Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.LargeHomeAppliances,
				},

				new DeviceViewModel()
				{
					DeviceId = 5,
					Name = "DeviceName5",
					ProblemDescription = "ProblemDescription5",
					OnGuarantee = false,
					Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.OfficeEquipment,
				},

				new DeviceViewModel()
				{
					DeviceId = 6,
					Name = "DeviceName6",
					ProblemDescription = "ProblemDescription6",
					OnGuarantee = true,
					Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.SmallHouseholdAppliances,
				},
			};
		}
    }
}
