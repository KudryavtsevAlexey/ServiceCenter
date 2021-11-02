using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KudryavtsevAlexey.ServiceCenter.Models;

using Moq;

using ServiceCenter.Tests.Helpers.OrderHelpers;

namespace ServiceCenter.Tests.Helpers.DataHelpers
{
    public class DataHelper
    {
        private static Client client = GetClient();
        private static Device device = GetDevice();
        private static Master master = GetMaster();
        private static Order order = GetOrder();

        public static Client GetClient()
        {
            return new Client()
            {
                ClientId = 1,
                Email = "ExpectedEmail1@mail.ru",
                FirstName = "ClientFirstName",
                LastName = "ClientLastName",
            };
        }

        public static List<Client> GetManyClients()
        {
            var clients = new List<Client>();

            clients.Add(GetClient());

            return clients;
        }

        public static Device GetDevice()
        {
            return new Device()
            {
                ClientId = 1,
                DeviceId = 1,
                ProblemDescription = "ProblemDescription1",
                Name = "DeviceName",
                OnGuarantee = false,
                Type = KudryavtsevAlexey.ServiceCenter.Enums.DeviceType.ComputerTechnology,
            };
        }

        public static List<Device> GetManyDevices()
        {
            var devices = new List<Device>();

            devices.Add(GetDevice());

            return devices;
        }

        public static Master GetMaster()
        {
            return new Master()
            {
                MasterId = 1,
                FirstName = "FirstName1",
                LastName = "LastName1",
                UniqueDescription = "FirstName1LastName1 Orders count: 1",
            };
        }

        public static List<Master> GetManyMasters()
        {
            var masters = new List<Master>();

            masters.Add(GetMaster());

            return masters;
        }

        public static Order GetOrder()
        {
            var order = new Order()
            {
                OrderId = 1,
                ClientId = 1,
                DeviceId = 1,
                MasterId = 1,
                Status = It.IsAny<KudryavtsevAlexey.ServiceCenter.Enums.Status>(),
            };

            order.AmountToPay = It.IsAny<int>();

            return order;
        }

        public static List<Order> GetManyOrders()
        {
            var orders = new List<Order>();

            orders.Add(GetOrder());

            return orders;
        }
    }
}
