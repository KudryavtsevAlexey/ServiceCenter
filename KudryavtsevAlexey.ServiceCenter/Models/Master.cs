using System.Collections.Generic;

namespace KudryavtsevAlexey.ServiceCenter.Models
{
    public class Master
    {
        public int MasterId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Device> Devices { get; set; }
    }
}
