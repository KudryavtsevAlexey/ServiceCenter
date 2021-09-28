using System;

namespace KudryavtsevAlexey.ServiceCenter.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Device Device { get; set; }
        public int DeviceId { get; set; }
        public Master Master { get; set; }
        public int MasterId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
