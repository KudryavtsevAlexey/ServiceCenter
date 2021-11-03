using AutoMapper;
using KudryavtsevAlexey.ServiceCenter.Controllers;
using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.Services;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Moq;
using ServiceCenter.Tests.Helpers.DataHelpers;
using ServiceCenter.Tests.Helpers.OrderHelpers;

using System.Threading.Tasks;
using Xunit;

namespace ServiceCenter.Tests.ControllersTestsInMemory
{
    public class OrderTests
    {
		private static ApplicationContext context = new DbContextHelper().Context;
		private OrderController _controller;
        private readonly Mock<IContext> _context;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IMasterService> _masterService;
        private readonly Mock<IOrderService> _orderService;

        public OrderTests()
        {
            _context = new Mock<IContext>();
            _mapper = new Mock<IMapper>();
            _masterService = new Mock<IMasterService>();
            _orderService = new Mock<IOrderService>();
            _controller = new OrderController(_context.Object, _mapper.Object,
                _masterService.Object, _orderService.Object);
        }

		[Fact]
		public async Task CreateOrderPost_ShouldReturnModelBackToView_WhenModelStateIsInvalid()
		{
			//arrange
		   var orderViewModel = OrderHelper.GetOrderViewModel();

			_controller.ModelState.AddModelError("Error", "Model state Error");
			//act
		   var result = await _controller.CreateOrder(orderViewModel);

			//assert

			Assert.False(_controller.ModelState.IsValid);
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<OrderViewModel>(viewResult.ViewData.Model);
		}


		[Fact]
		public async Task CreateOrderPost_ShouldReturnRedirect_WhenModelStateIsValid()
		{
			//arrange
			var orderViewModel = OrderHelper.GetOrderViewModel();

			var client = OrderHelper.MapClient(orderViewModel.Client);

			var device = OrderHelper.MapDevice(orderViewModel.Device);

			var master = OrderHelper.MapMaster(orderViewModel.Master);

			client.Devices.Add(device);
			device.Master = master;
			device.Client = client;
			master.Devices.Add(device);

			var order = OrderHelper.MapOrder(client, device, master);

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

			//act
			var result = await _controller.CreateOrder(orderViewModel);

			//assert

			Assert.True(_controller.ModelState.IsValid);
			var redirectResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Panel", redirectResult.ControllerName);
			Assert.Equal("ManageOrder", redirectResult.ActionName);
		}


		[Fact]
		public async Task MoreDetails_ShouldReturnNotFound_WhenIdIsNull()
		{
			//arrange

			//act

			var result = await _controller.MoreDetails(null);

			//assert

			Assert.IsType<NotFoundResult>(result);
		}


		[Fact]
		public async Task MoreDetails_ShouldReturnNotFound_WhenDeviceIsNull()
		{
			//arrange

			int? id = -1;

			var device = await context.Devices.FirstOrDefaultAsync(d => d.Order.OrderId == id);

			_controller = new OrderController(context, _mapper.Object, _masterService.Object, _orderService.Object);
			//act
			var result = await _controller.MoreDetails(id);

			//assert
			Assert.Null(device);
			Assert.IsType<NotFoundResult>(result);
		}


		[Fact]
		public async Task MoreDetails_ShouldReturnView_WhenDeviceFound()
		{
			// arrange

			int? id = 2;

			var device = await context.Devices.FirstOrDefaultAsync(d => d.Order.OrderId == id);

			var deviceViewModel = new DeviceViewModel()
			{
				DeviceId = device.DeviceId,
				Name = device.Name,
				ProblemDescription = device.ProblemDescription,
				OnGuarantee = device.OnGuarantee,
				Type = device.Type,
			};

			_mapper.Setup(m => m.Map<DeviceViewModel>(device))
				.Returns(deviceViewModel);

			_controller = new OrderController(context, _mapper.Object, _masterService.Object, _orderService.Object);

			// act
			var result = await _controller.MoreDetails(id);

			// assert
			Assert.NotNull(device);
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<DeviceViewModel>(viewResult.ViewData.Model);
		}

	}
}
