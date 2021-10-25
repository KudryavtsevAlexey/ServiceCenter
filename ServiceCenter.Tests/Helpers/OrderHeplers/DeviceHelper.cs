using KudryavtsevAlexey.ServiceCenter.Models;

namespace ServiceCenter.Tests.Helpers.OrderHeplers
{
    public class DeviceHelper
    {
        public static Device GetDevice()
        {
            return new Device()
            {
                DeviceId = 1,
                Name = "DeviceName",
                OnGuarantee = true,
                ProblemDescription = "DeviceProblemDescription",
                Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.SmallHouseholdAppliances,
            };
        }
    }
}
