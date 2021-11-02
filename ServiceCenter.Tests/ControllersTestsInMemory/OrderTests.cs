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
        private OrderController _controller;
        private readonly ApplicationContext _context;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IMasterService> _masterService;
        private readonly Mock<IOrderService> _orderService;
		private DbContextHelper ContextHelper { get; set; }

        public OrderTests()
        {
			ContextHelper = new DbContextHelper();
            _context = ContextHelper.Context;
            _mapper = new Mock<IMapper>();
            _masterService = new Mock<IMasterService>();
            _orderService = new Mock<IOrderService>();
            _controller = new OrderController(_context, _mapper.Object,
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

			int? id = 0;

			//act
			var result = await _controller.MoreDetails(id);

			//assert

			Assert.IsType<NotFoundResult>(result);
		}


		[Fact]
		public async Task MoreDetails_ShouldReturnView_WhenDeviceFound()
		{
			// arrange
			int? id = 1;
			var device = DataHelper.GetDevice();
			var deviceViewModel = DeviceHelper.GetDeviceViewModel();
			_mapper.Setup(m => m.Map<DeviceViewModel>(It.IsAny<Device>())).Returns(It.IsAny<DeviceViewModel>());

			// act
			var result = await _controller.MoreDetails(id);

			// assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<DeviceViewModel>(viewResult.ViewData.Model);
		}

	}
}
