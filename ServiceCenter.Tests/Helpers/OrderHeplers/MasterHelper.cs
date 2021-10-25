using KudryavtsevAlexey.ServiceCenter.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ServiceCenter.Tests.Helpers.OrderHeplers
{
    public class MasterHelper
    {
        public static IEnumerable<MasterViewModel> GetAllMasters()
        {
            return new List<MasterViewModel>
            {
                new MasterViewModel
                {
                    MasterId = 1,
                    FirstName = "MasterFN1",
                    LastName = "MasterLN21",
                    OrdersCount = 0,
                    UniqueDescription = $"Master№1 : MasterFN1 MasterLN2",
                },

                new MasterViewModel
                {
                    MasterId = 2,
                    FirstName = "MasterFN2",
                    LastName = "MasterLN2",
                    OrdersCount = 0,
                    UniqueDescription = $"Master№2 : MasterFN2 MasterLN2",
                },

                new MasterViewModel
                {
                    MasterId = 3,
                    FirstName = "MasterFN3",
                    LastName = "MasterLN3",
                    OrdersCount = 0,
                    UniqueDescription = $"Master№3 : MasterFN3 MasterLN3",
                },
            };
        }

        public static MasterViewModel GetMaster()
        {
            var masters = GetAllMasters();

            return masters.OrderBy(m => m.OrdersCount).FirstOrDefault();
        }

    }
}
