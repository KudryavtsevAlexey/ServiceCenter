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
			int? id = 1;

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


		[Fact]
		public async Task EditOrderGet_ShouldReturnNotFound_WhenIdIsNull()
		{
			// arrange
			int? id = null;
			// act
			var result = await _controller.EditOrder(id);

			// assert
			Assert.IsType<NotFoundResult>(result);
		}


		[Fact]
		public async Task EditOrderGet_ShouldReturnNotFound_WhenOrderIsNull()
		{
			// arrange
			int? id = -1;
			_controller = new OrderController(context, _mapper.Object, _masterService.Object, _orderService.Object);

			var order = await context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

			// act
			var result = await _controller.EditOrder(id);
			// assert
			Assert.Null(order);
			Assert.IsType<NotFoundResult>(result);
		}


		[Fact]
		public async Task EditOrderGet_ShouldReturnView_WhenOrderFound()
		{
			// arrange
			int? id = 1;

			var order = await context.Orders.
				Include(c=>c.Client)
				.Include(d=>d.Device)
				.Include(m=>m.Master)
				.FirstOrDefaultAsync(o => o.OrderId == id);

			//TODO: Добавить это в OrderHelper
			var mappedClient = OrderHelper.MapClientViewModel(order.Client);

			var mappedDevice = OrderHelper.MapDeviceViewModel(order.Device);

			var mappedMaster = OrderHelper.MapMasterViewModel(order.Master);

			_mapper.Setup(m => m.Map<ClientViewModel>(order.Client))
				.Returns(mappedClient);
			_mapper.Setup(m => m.Map<DeviceViewModel>(order.Device))
				.Returns(mappedDevice);
			_mapper.Setup(m => m.Map<MasterViewModel>(order.Master))
				.Returns(mappedMaster);

			_masterService.Setup(s => s.GetAllMasters()).ReturnsAsync(MasterHelper.GetManyMasterViewModels());
			
			_controller = new OrderController(context, _mapper.Object, _masterService.Object, _orderService.Object);

			// act
			var result = await _controller.EditOrder(id);

			// assert
			Assert.NotNull(order);
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<OrderViewModel>(viewResult.ViewData.Model);
		}


		[Fact]
		public async Task EditOrderPost_ShouldReturnModelBackToView_WhenModelStateInvalid()
		{
			// arrange
			var orderViewModel = OrderHelper.GetOrderViewModel();
			_controller.ModelState.AddModelError("Error", "Model state error");

			// act
			var result = await _controller.EditOrder(orderViewModel);

			// assert
			Assert.False(_controller.ModelState.IsValid);
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<OrderViewModel>(viewResult.ViewData.Model);
		}


		[Fact]
		public async Task EditOrderPost_ShouldReturnRedirect_WhenModelStateValid()
		{
			// arrange
			var orderViewModel = OrderHelper.GetOrderViewModel();

			var order = OrderHelper.GetOrder();

			var orderToDelete = await context.Orders.FindAsync(orderViewModel.OrderId);
			context.Orders.Remove(orderToDelete);

			order.Status = KudryavtsevAlexey.ServiceCenter.Enums.Status.Issued;

			_orderService.Setup(m => m.MapOrder(orderViewModel))
				.ReturnsAsync(order);

			_controller = new OrderController(context, _mapper.Object, _masterService.Object, _orderService.Object);

			// act
			var result = await _controller.EditOrder(orderViewModel);

			// assert
			Assert.True(_controller.ModelState.IsValid);
			var redirectResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("ManageOrder", redirectResult.ActionName);
			Assert.Equal("Panel", redirectResult.ControllerName);
		}


		[Fact]
		public async Task DeleteOrder_ShouldReturnNotFound_WhenIdIsNull()
		{
			// arrange
			int? id = null;

			// act
			var result = await _controller.DeleteOrder(id);

			// assert
			Assert.IsType<NotFoundResult>(result);
		}


		[Fact]
		public async Task DeleteOrder_ShouldReturnRedirect_WhenOrderFound()
		{
			// arrange
			int? id = 6;

			_controller = new OrderController(context, _mapper.Object, _masterService.Object, _orderService.Object);

			var countBefore = context.Orders.Local.Count;

			// act
			var result = await _controller.DeleteOrder(id);

			countBefore--;
			var countAfter = context.Orders.Local.Count;

			// assert
			var redirectResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("ManageOrder", redirectResult.ActionName);
			Assert.Equal("Panel", redirectResult.ControllerName);
			Assert.Equal(countBefore, countAfter);
		}

	}
}
