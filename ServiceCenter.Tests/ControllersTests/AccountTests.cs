using AutoFixture;
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
		public async Task IndexPost_ReturnModelBackToView_WhenModelStateIsInvalid()
		{
			// arrange
			var loginViewModelFixture = new Fixture();
			var loginViewModel = loginViewModelFixture.Create<LoginViewModel>();
			loginViewModel.Email += "@mail.ru";
			var controller = new AccountController(_userManager.Object, _signInManager.Object);
			controller.ModelState.AddModelError("", "Model state error");

			// act
			var result = await controller.Login(loginViewModel);

			// assert
			Assert.False(controller.ModelState.IsValid);
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsAssignableFrom<LoginViewModel>(viewResult.ViewData.Model);
		}

		[Fact]
		public void IndexPost_ContinuesAuthorization_WhenModelStateIsValid()
		{
			// arrange
			var loginViewModelFixture = new Fixture();
			var loginViewModel = loginViewModelFixture.Create<LoginViewModel>();
			loginViewModel.Email += "@mail.ru";
			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			// act

			// assert
			Assert.True(controller.ModelState.IsValid);
		}

		[Fact]
		public void IndexPost_UserShouldNotBeFound_WhenEmailsDoesNotMatches()
		{
			// arrange
			var loginViewModel = new LoginViewModel() 
			{
				Email = "ExpectedEmail@mail.ru",
				Password = "ExpectedPassword123",
			};
			
			var actualEmail = "ActualEmail@mail.ru";
			ApplicationUser user = null;
			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			// act
			var result = controller.Login(loginViewModel);

			// assert
			Assert.True(controller.ModelState.IsValid);
			Assert.NotEqual(actualEmail, loginViewModel.Email);
			Assert.Null(user);
		}

		[Fact]
		public void IndexPost_UserShouldBeFound_WhenEmailsMatches()
		{
			// arrange
			var loginViewModel = new LoginViewModel() {
				Email = "ExpectedEmail@mail.ru",
				Password = "ExpectedPassword123",
			};

			var actualEmail = "ExpectedEmail@mail.ru";
			var user = new FakeApplicationUser() 
			{
				Email = "ExpectedEmail@mail.ru",
			};
			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			// act
			var result = controller.Login(loginViewModel);

			// assert
			Assert.True(controller.ModelState.IsValid);
			Assert.Equal(actualEmail, loginViewModel.Email);
			Assert.NotNull(user);
		}

		[Fact]
		public void IndexPost_UserShouldNotBeAuthorized_WhenPasswordsDoesNotMathes()
		{
			// arrange
			var loginViewModel = new LoginViewModel() 
			{
				Email = "ExpectedEmail@mail.ru",
				Password = "ExpectedPassword123",
			};

			var user = new FakeApplicationUser() 
			{
				Email = "ExpectedEmail@mail.ru",
				Password = "ActualPassword123",
			};
			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			// act
			var result = controller.Login(loginViewModel);

			// assert
			Assert.NotEqual(user.Password, loginViewModel.Email);
			Assert.NotNull(user);
		}

		[Fact]
		public void IndexPost_UserShouldBeAuthorized_WhenPasswordsMathes()
		{
			// arrange
			var loginViewModel = new LoginViewModel() {
				Email = "ExpectedEmail@mail.ru",
				Password = "ExpectedPassword123",
			};

			var user = new FakeApplicationUser() {
				Email = "ExpectedEmail@mail.ru",
				Password = "ExpectedPassword123",
			};
			var controller = new AccountController(_userManager.Object, _signInManager.Object);

			// act
			var result = controller.Login(loginViewModel);
			var redirectResult = controller.RedirectToAction("Index", "Home");

			// assert
			Assert.Equal(user.Password, loginViewModel.Password);
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(redirectResult);
			Assert.Equal("Home", redirectToActionResult.ControllerName);
			Assert.Equal("Index", redirectToActionResult.ActionName);
		}
	}
}
