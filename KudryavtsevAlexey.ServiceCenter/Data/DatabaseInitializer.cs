using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Data
{
	public class DatabaseInitializer
	{
		public static void Init(IServiceProvider serviceProvider)
		{
			var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
			IEnumerable<Claim> claimsForAdministrator = new List<Claim>
			{
				new Claim(ClaimTypes.Role, "Administrator"),
			};

			IEnumerable<Claim> claimsForMaster = new List<Claim>
			{
				new Claim(ClaimTypes.Role, "Master"),
			};

			CustomCreateUserAsync(userManager, "KudryavtsevAlexey", "a.kudryavcev0812@bk.ru",
							 "Alexey", "Kudryavtsev",
								claimsForAdministrator).GetAwaiter().GetResult();

			CustomCreateUserAsync(userManager, "SidorovKirill", "sidorovk1504@mail.ru",
							 "Kirill", "Sidorov",
								claimsForMaster).GetAwaiter().GetResult();

			CustomCreateUserAsync(userManager, "KerimovMaxim", "kerimovm1806@mail.ru",
							 "Maxim", "Kerimov",
								claimsForMaster).GetAwaiter().GetResult();
		}

		private static async Task CustomCreateUserAsync(UserManager<ApplicationUser> userManager, string userName,
												  string email, string firstName, string lastName, IEnumerable<Claim> claims)
		{
			var user = new ApplicationUser
			{
				UserName = userName,
				Email = email,
				FirstName = firstName,
				LastName = lastName
			};

			var result = await userManager.CreateAsync(user, $"PasswordFor{userName}");

			if (result.Succeeded)
			{
				await userManager.AddClaimsAsync(user, claims);
			}
		}
	}
}
