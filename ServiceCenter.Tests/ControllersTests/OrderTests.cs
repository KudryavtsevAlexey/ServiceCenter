using AutoMapper;
using KudryavtsevAlexey.ServiceCenter.Controllers;
using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.Services;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceCenter.Tests.Helpers.AccountHeplers;
using ServiceCenter.Tests.Helpers.OrderHeplers;

using System.Threading.Tasks;
using Xunit;

namespace ServiceCenter.Tests.ControllersTests
{
    public class OrderTests
    {
		private readonly Mock<IWrapper> _wrapper;
		private readonly Mock<IContext> _context;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IMasterService> _masterService;
        private readonly Mock<IOrderService> _orderService;

        public OrderTests()
        {
            _wrapper = new Mock<IWrapper>();
            _context = new Mock<IContext>();
            _mapper = new Mock<IMapper>();
            _masterService = new Mock<IMasterService>();
            _orderService = new Mock<IOrderService>();
        }

        [Fact]
        public async Task CreateOrderPost_ShouldReturnModelBackToView_WhenModelStateIsInvalid()
        {
            // arrange
            var orderViewModel = OrderHelper.GetOrderViewModel();

            var controller = new OrderController(_context.Object, _mapper.Object, _masterService.Object, _orderService.Object);
            controller.ModelState.AddModelError("Error", "Model state Error");

            // act
            var result = await controller.CreateOrder(orderViewModel);

            // assert
            Assert.False(controller.ModelState.IsValid);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<OrderViewModel>(viewResult.ViewData.Model);
        }


        [Fact]
        public async Task CreateOrderPost_ShouldReturnRedirect_WhenModelStateIsValid()
        {
            // arrange
            var orderViewModel = OrderHelper.GetOrderViewModel();

            var client = OrderHelper.MapClient(orderViewModel.Client);

            var device = OrderHelper.MapDevice(orderViewModel.Device);

            var master = OrderHelper.MapMaster(orderViewModel.Master);

            client.Devices.Add(device);
            device.Master = master;
            device.Client = client;
            master.Devices.Add(device);

            var order = OrderHelper.MapOrder(client, device, master);

            _context.Setup(c => c.Masters.FindAsync(orderViewModel.Master.MasterId))
                .ReturnsAsync(master);

            _context.Setup(c => c.SaveChangesAsync())
                .ReturnsAsync(It.IsAny<int>());

            _context.Setup(c => c.Clients.AddAsync(order.Client, It.IsAny<System.Threading.CancellationToken>()));
            _context.Setup(c => c.Devices.AddAsync(order.Device, It.IsAny<System.Threading.CancellationToken>()));
            _context.Setup(c => c.Orders.AddAsync(order, It.IsAny<System.Threading.CancellationToken>()));

            _mapper.Setup(m => m.Map<Client>(orderViewModel.Client))
                .Returns(client);

            _mapper.Setup(m => m.Map<Device>(orderViewModel.Device))
                .Returns(device);

            _mapper.Setup(m => m.Map<Order>(order))
                .Returns(order);

            _orderService.Setup(s => s.MapOrder(orderViewModel))
                .ReturnsAsync(order);

            var controller = new OrderController(_context.Object, _mapper.Object, _masterService.Object, _orderService.Object);

            // act
            var result = await controller.CreateOrder(orderViewModel);

            // assert
            Assert.True(controller.ModelState.IsValid);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Panel", redirectResult.ControllerName);
            Assert.Equal("ManageOrder", redirectResult.ActionName);
        }


        [Fact]
        public async Task MoreDetails_ShouldReturnNotFound_WhenInIsNull()
        {
            // arrange
            var controller = new OrderController(_context.Object, _mapper.Object, _masterService.Object, _orderService.Object);

            // act
            var result = await controller.MoreDetails(null);

            // assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task MoreDetails_ShouldReturnNotFound_WhenDeviceIsNull()
        {
            //arrange
            var controller = new OrderController(_context.Object, _mapper.Object, _masterService.Object, _orderService.Object);
            int? id = 1;

            Device device = null;

            _context.Setup(c => c.FirstOrDefaultDeviceAsyncWrapper(id))
                .ReturnsAsync(device);

            //act
            var result = await controller.MoreDetails(id);

            //assert
            Assert.True(controller.ModelState.IsValid);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task MoreDetails_ShouldReturnView_WhenDeviceIsNotNull()
        {
            //arrange
            var controller = new OrderController(_context.Object, _mapper.Object, _masterService.Object, _orderService.Object);
            int? id = 1;

            Device device = DeviceHelper.GetDevice();
            var deviceViewModel = new DeviceViewModel()
            {
                DeviceId = device.DeviceId,
                ProblemDescription = device.ProblemDescription,
                OnGuarantee = device.OnGuarantee,
                Name = device.Name,
                Type = device.Type,
            };

            _context.Setup(c => c.FirstOrDefaultDeviceAsyncWrapper(id))
                .ReturnsAsync(device);

            _mapper.Setup(m => m.Map<DeviceViewModel>(device))
                .Returns(deviceViewModel);

            //act
            var result = await controller.MoreDetails(id);

            //assert
            Assert.NotNull(device);
            Assert.True(controller.ModelState.IsValid);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<DeviceViewModel>(viewResult.ViewData.Model);
        }
    }   
}
