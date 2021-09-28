namespace KudryavtsevAlexey.ServiceCenter.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public enum DeviceType
        {
            Smartphone,
            Laptop,
            Desktop,
            Keyboard,
            Headphones,
        }
        public string Description { get; set; }
        public Client Client { get; set; }
        public int ClientId  { get; set; }
        public Master Master { get; set; }
        public int MasterId { get; set; }
    }
}
