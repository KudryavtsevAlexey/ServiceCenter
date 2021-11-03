using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Moq;

namespace ServiceCenter.Tests.Helpers.OrderHelpers
{
    public class OrderHelper
    {
        public static Order GetOrder()
		{
            return new Order()
            {
                OrderId = 1,
                ClientId = 1,
                DeviceId = 1,
                MasterId = 1,
                AmountToPay = 0,
                Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.Accepted,
            };
		}

        public static OrderViewModel GetOrderViewModel()
		{
            return new OrderViewModel()
            {
                OrderId = 1,
                AmountToPay = 0,
                Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.Accepted,
                Client = ClientHelper.GetClientViewModel(),
                Device = DeviceHelper.GetDeviceViewModel(),
                Master = MasterHelper.GetMasterViewModel(),
            };
		}

        public static List<Order> GetManyOrders()
		{
            return new List<Order>()
            {
                new Order() 
                {
                    OrderId = 2,
                    ClientId = 2,
                    DeviceId = 2,
                    MasterId = 2,
                    AmountToPay = 0,
                    Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.AwaitingPayment,
                },

                new Order()
                {
                    OrderId = 3,
                    ClientId = 3,
                    DeviceId = 3,
                    MasterId = 3,
                    AmountToPay = 3333,
                    Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.Issued,
                },

                new Order()
                {
                    OrderId = 4,
                    ClientId = 4,
                    DeviceId = 4,
                    MasterId = 4,
                    AmountToPay = 4444,
                    Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.PaidUp,
                },

                new Order()
                {
                    OrderId = 5,
                    ClientId = 5,
                    DeviceId = 5,
                    MasterId = 5,
                    AmountToPay = 5555,
                    Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.ReadyToIssue,
                },

                new Order()
                {
                    OrderId = 6,
                    ClientId = 6,
                    DeviceId = 6,
                    MasterId = 6,
                    AmountToPay = 0,
                    Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.UnderRepair,
                },
            };
		}

        public static List<OrderViewModel> GetManyOrderViewModels()
		{
            return new List<OrderViewModel>()
            {
                new OrderViewModel()
				{
                    OrderId = 2,
                    AmountToPay = 0,
                    Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.AwaitingPayment,
                },

                new OrderViewModel()
                {
                    OrderId = 3,
                    AmountToPay = 3333,
                    Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.Issued,
                },

                new OrderViewModel()
                {
                    OrderId = 4,
                    AmountToPay = 4444,
                    Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.PaidUp,
                },

                new OrderViewModel()
                {
                    OrderId = 5,
                    AmountToPay = 5555,
                    Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.ReadyToIssue,
                },

                new OrderViewModel()
                {
                    OrderId = 6,
                    AmountToPay = 0,
                    Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.UnderRepair,
                },
            };
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
