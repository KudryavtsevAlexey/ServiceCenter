using AutoFixture;
using KudryavtsevAlexey.ServiceCenter.Controllers;
using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceCenter.Tests.Helpers;
using ServiceCenter.Tests.Helpers.AccountHeplers;
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
			var user = UserManagerHelper.GetApplicationUser();

			var loginViewModel = new LoginViewModel() 
			{
				Email = "ExpectedEmail@test.com",
				Password = "ExpectedPassword123"
			};

			_userManager.Setup(m => m.FindByEmailAsync(loginViewModel.Email))
				.ReturnsAsync(user);

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
		public async Task LoginPost_ShouldReturnRedirect_WhenPasswordsMatch()
		{
			// arrange
			var user = UserManagerHelper.GetApplicationUser();

			user.Password = "ExpectedPassword1234";

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

			Assert.Equal(user.Password, loginViewModel.Password);

			var viewResult = Assert.IsType<RedirectToActionResult>(result);

			Assert.Equal("Home", viewResult.ControllerName);

			Assert.Equal("Index", viewResult.ActionName);
		}


		[Fact]
		public async Task RegisterPost_ShouldReturnModelBackToView_WhenModelStateIsInvalid()
		{
			// arrange
			var registerViewModelFixture = new Fixture();
			var registerViewModel = registerViewModelFixture.Create<RegisterViewModel>();
			var controller = new AccountController(_userManager.Object, _signInManager.Object);
			controller.ModelState.AddModelError("Error", "Model state error");

			// act
			var result = await controller.Register(registerViewModel);

			// assert
			Assert.False(controller.ModelState.IsValid);
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<RegisterViewModel>(viewResult.ViewData.Model);
		}

		[Fact]
		public async Task RegisterPost_ShouldReturnModelBackToView_WhenIdentityResultFailed()
		{
			// arrange
			var registerViewModel = RegisterHelper.GetRegisterViewModel();;

			_userManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
				.ReturnsAsync(new FakeIdentityResult(false));

			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			// act
			var result = await controller.Register(registerViewModel);

			// assert
			Assert.True(controller.ModelState.IsValid);
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<RegisterViewModel>(viewResult.ViewData.Model);
		}

		[Fact]
		public async Task RegisterPost_ShouldReturnModelBackToView_WhenSignInResultFailed()
		{
			// arrange
			var registerViewModel = RegisterHelper.GetRegisterViewModel();

			var user = UserManagerHelper.GetApplicationUser();

			_userManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
				.ReturnsAsync(new FakeIdentityResult(true));

			_signInManager.Setup(m => m.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(),
				It.IsAny<bool>(), It.IsAny<bool>()))
				.ReturnsAsync(new FakeSignInResult(false));

			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			// act
			var result = await controller.Register(registerViewModel);

			// assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<RegisterViewModel>(viewResult.ViewData.Model);
		}

		[Fact]
		public async Task RegisterPost_ShouldReturnRedirect_WhenSignInResultSucceeded()
		{
			// arrange
			var registerViewModel = RegisterHelper.GetRegisterViewModel();

			var user = UserManagerHelper.GetApplicationUser();

			_userManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
				.ReturnsAsync(new FakeIdentityResult(true));

			_signInManager.Setup(m => m.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(),
				It.IsAny<bool>(), It.IsAny<bool>()))
				.ReturnsAsync(new FakeSignInResult(true));

			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			// act
			var result = await controller.Register(registerViewModel);

			// assert
			Assert.Equal(user.Password, registerViewModel.Password);

			var viewResult = Assert.IsType<RedirectToActionResult>(result);

			Assert.Equal("Home", viewResult.ControllerName);

			Assert.Equal("Index", viewResult.ActionName);
		}
	}
}
