using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;

namespace ServiceCenter.Tests.Helpers.OrderHelpers
{
    public class MasterHelper
    {
        public static Master GetMaster()
		{
			return new Master()
			{
				MasterId = 1,
				FirstName = "MasterFirstName1",
				LastName = "LastName1",
				UniqueDescription = "FirstName1LastName1 Orders count: 1",
			};
		}

        public static MasterViewModel GetMasterViewModel()
		{
			return new MasterViewModel()
			{
				MasterId = 1,
				FirstName = "MasterFirstName1",
				LastName = "MasterLastName1",
				OrdersCount = 1,
				UniqueDescription = "FirstName1LastName1 Orders count: 1",
			};
		}

        public static List<Master> GetManyMasters()
		{
			return new List<Master>()
			{
				new Master()
				{
					MasterId = 2,
					FirstName = "MasterFirstName2",
					LastName = "MasterLastName2",
					UniqueDescription = "FirstName2LastName2 Orders count: 1",
				},

				new Master()
				{
					MasterId = 3,
					FirstName = "MasterFirstName3",
					LastName = "MasterLastName3",
					UniqueDescription = "FirstName3LastName3 Orders count: 1",
				},

				new Master()
				{
					MasterId = 4,
					FirstName = "MasterFirstName4",
					LastName = "MasterLastName4",
					UniqueDescription = "FirstName4LastName4 Orders count: 1",
				},

				new Master()
				{
					MasterId = 5,
					FirstName = "MasterFirstName5",
					LastName = "MasterLastName5",
					UniqueDescription = "FirstName5LastName5 Orders count: 1",
				},

				new Master()
				{
					MasterId = 6,
					FirstName = "MasterFirstName6",
					LastName = "MasterLastName6",
					UniqueDescription = "FirstName6LastName6 Orders count: 1",
				},
			};
		}

		public static List<MasterViewModel> GetManyMasterViewModels()
		{
			return new List<MasterViewModel>()
			{
				new MasterViewModel()
				{
					MasterId = 2,
					FirstName = "MasterFirstName2",
					LastName = "MasterLastName2",
					OrdersCount = 1,
					UniqueDescription = "FirstName2LastName2 Orders count: 1",
				},

				new MasterViewModel()
				{
					MasterId = 3,
					FirstName = "MasterFirstName3",
					LastName = "MasterLastName3",
					OrdersCount = 1,
					UniqueDescription = "FirstName3LastName3 Orders count: 1",
				},

				new MasterViewModel()
				{
					MasterId = 4,
					FirstName = "MasterFirstName4",
					LastName = "MasterLastName4",
					OrdersCount = 1,
					UniqueDescription = "FirstName4LastName4 Orders count: 1",
				},

				new MasterViewModel()
				{
					MasterId = 5,
					FirstName = "MasterFirstName5",
					LastName = "MasterLastName5",
					OrdersCount = 1,
					UniqueDescription = "FirstName5LastName5 Orders count: 1",
				},

				new MasterViewModel()
				{
					MasterId = 6,
					FirstName = "MasterFirstName6",
					LastName = "MasterLastName6",
					OrdersCount = 1,
					UniqueDescription = "FirstName6LastName6 Orders count: 1",
				},
			};
		}
    }
}
