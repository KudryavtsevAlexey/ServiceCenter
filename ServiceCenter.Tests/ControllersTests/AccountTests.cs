using KudryavtsevAlexey.ServiceCenter.Controllers;
using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceCenter.Tests.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace ServiceCenter.Tests.ControllersTests
{
	public class AccountTests
	{
		private readonly Mock<FakeUserManager> _userManager;
		private readonly Mock<FakeSignInManager> _signInManager;

		public AccountTests()
		{
			_userManager = new Mock<FakeUserManager>();
			_signInManager = new Mock<FakeSignInManager>();
		}


		[Fact]
		public async Task LoginPost_ShouldReturnModelBackToView_WhenModelStateIsInvalid()
		{
			// arrange
			var loginViewModel = new LoginViewModel()
			{
				Email = "ExpectedEmail@test.com",
			};

			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			controller.ModelState.AddModelError("Error", "Model state error");
			// act
			var result = await controller.Login(loginViewModel);

			// assert
			Assert.False(controller.ModelState.IsValid);

			var viewResult = Assert.IsType<ViewResult>(result);

			Assert.IsAssignableFrom<LoginViewModel>(viewResult.ViewData.Model);
		}


		[Fact]
		public async Task LoginPost_ShouldReturnModelBackToView_WhenUserNotFound()
		{
			// arrange
			var loginViewModel = new LoginViewModel() {
				Email = "ExpectedEmail@test.com",
			};

			_userManager.Setup(u => u.FindByEmailAsync(loginViewModel.Email))
				.ReturnsAsync((ApplicationUser)null);

			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			// act
			var result = await controller.Login(loginViewModel);

			// assert
			Assert.True(controller.ModelState.IsValid);

			var viewResult = Assert.IsType<ViewResult>(result);

			Assert.IsAssignableFrom<LoginViewModel>(viewResult.ViewData.Model);
		}

		[Fact]
		public async Task LoginPost_ShouldReturnModelBackToView_WhenPasswordsMismatch()
		{
			// arrange
			var user = new ApplicationUser()
			{
				PasswordHash = UserManagerHelper.GetApplicationUser().Password
			};

			var loginViewModel = new LoginViewModel() 
			{
				Email = "ExpectedEmail@test.com",
				Password = "ExpectedPassword123"
			};

			_userManager.Setup(m => m.FindByEmailAsync(loginViewModel.Email))
				.ReturnsAsync(new ApplicationUser());

			 _signInManager.Setup(m => m.PasswordSignInAsync(
				user, loginViewModel.Password, It.IsAny<bool>(), It.IsAny<bool>()))
				.ReturnsAsync(new FakeSignInResult(false));

			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			// act
			var result = await controller.Login(loginViewModel);

			// assert
			Assert.True(controller.ModelState.IsValid);

			Assert.NotEqual(user.PasswordHash, loginViewModel.Password);

			var viewResult = Assert.IsType<ViewResult>(result);

			Assert.IsAssignableFrom<LoginViewModel>(viewResult.ViewData.Model);
		}

		[Fact]
		public async Task LoginPost_ShouldReturnModelBackToView_WhenPasswordsMatch()
		{
			// arrange
			var user = new ApplicationUser()
			{
				PasswordHash = UserManagerHelper.GetApplicationUser().Password
			};

			var loginViewModel = new LoginViewModel()
			{
				Email = "ExpectedEmail@test.com",
				Password = "ExpectedPassword1234"
			};

			_userManager.Setup(m => m.FindByEmailAsync(loginViewModel.Email))
				.ReturnsAsync(user);

			_signInManager.Setup(m => m.PasswordSignInAsync(
			   user, loginViewModel.Password, It.IsAny<bool>(), It.IsAny<bool>()))
			   .ReturnsAsync(new FakeSignInResult(true));

			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			// act
			var result = await controller.Login(loginViewModel);

			// assert
			Assert.True(controller.ModelState.IsValid);

			Assert.Equal(user.PasswordHash, loginViewModel.Password);

			var viewResult = Assert.IsType<RedirectToActionResult>(result);

			Assert.Equal("Home", viewResult.ControllerName);

			Assert.Equal("Index", viewResult.ActionName);
		}
	}
}
