using System.Collections.Generic;
using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Moq;

using ServiceCenter.Tests.Helpers.DataHelpers;

namespace ServiceCenter.Tests.Helpers.OrderHelpers
{
    public class OrderHelper
    {
        private static List<Client> clientsCopy = DataHelper.GetManyClients();
        private static List<Device> devicesCopy = DataHelper.GetManyDevices();
        private static List<Master> mastersCopy = DataHelper.GetManyMasters();

        public static OrderViewModel GetOrderViewModel()
		{
            var client = ClientHelper.GetClientViewModel();
            var device = DeviceHelper.GetDeviceViewModel();
            var master = MasterHelper.GetMasterViewModel();

            var order = new OrderViewModel()
            {
                OrderId = 1,
                Status = It.IsAny<KudryavtsevAlexey.ServiceCenter.Enums.Status>(),
                Client = client,
                Device = device,
                Master = master,
            };

            order.AmountToPay = order.Device.OnGuarantee ? 0 : It.IsAny<int>();

            return order;
		}

        public static Client MapClient(ClientViewModel clientViewModel)
        {
            return new Client()
            {
                ClientId = It.IsAny<int>(),
                Email = clientViewModel.Email,
                FirstName = clientViewModel.FirstName,
                LastName = clientViewModel.LastName,
            };
        }

        public static Device MapDevice(DeviceViewModel deviceViewModel)
        {
            return new Device()
            {
                DeviceId = It.IsAny<int>(),
                Name = deviceViewModel.Name,
                OnGuarantee = deviceViewModel.OnGuarantee,
                ProblemDescription = deviceViewModel.ProblemDescription,
                Type = deviceViewModel.Type,
            };
        }

        public static Master MapMaster(MasterViewModel masterViewModel)
        {
            return new Master()
            {
                MasterId = It.IsAny<int>(),
                FirstName = masterViewModel.FirstName,
                LastName = masterViewModel.LastName,
                UniqueDescription = masterViewModel.UniqueDescription,
            };
        }

        public static Order MapOrder(Client client, Device device, Master master)
        {
            var order = new Order()
            {
                OrderId = It.IsAny<int>(),
                Client = client,
                ClientId = client.ClientId,
                DeviceId = device.DeviceId,
                Device = device,
                MasterId = master.MasterId,
                Master = master,
                Status = It.IsAny<KudryavtsevAlexey.ServiceCenter.Enums.Status>(),
            };

            if (order.Device.OnGuarantee || ((int)order.Status) > 2)
            {
                order.AmountToPay = 0;
            }
            else
            {
                order.AmountToPay = It.IsAny<int>();
            }

            order.Client = client;
            order.Device = device;
            order.Master = master;

            client.Orders.Add(order);
            device.Order = order;
            master.Orders.Add(order);

            return order;
        }
    }
}
