using KudryavtsevAlexey.ServiceCenter.Models;
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
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			IEnumerable<Claim> claimsForAdministrator = new List<Claim>
			{
				new Claim(ClaimTypes.Role, "Administrator"),
			};

			IEnumerable<Claim> claimsForMaster = new List<Claim>
			{
				new Claim(ClaimTypes.Role, "Master"),
			};

			CustomCreateUserAsync(serviceProvider, userManager, "KudryavtsevAlexey", "a.kudryavcev0812@bk.ru",
							 "Alexey", "Kudryavtsev",
								claimsForAdministrator).GetAwaiter().GetResult();

			CustomCreateUserAsync(serviceProvider, userManager, "SidorovKirill", "sidorovk1504@mail.ru",
							 "Kirill", "Sidorov",
								claimsForMaster).GetAwaiter().GetResult();

			CustomCreateUserAsync(serviceProvider, userManager, "KerimovMaxim", "kerimovm1806@mail.ru",
							 "Maxim", "Kerimov",
								claimsForMaster).GetAwaiter().GetResult();
		}

		private static async Task CustomCreateUserAsync(IServiceProvider serviceProvider,UserManager<ApplicationUser> userManager, string userName,
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

			foreach (var claim in claims)
			{
				if (claim.Type == ClaimTypes.Role && claim.Value == "Master")
				{
					int id = 1;
					var db = serviceProvider.GetRequiredService<ApplicationContext>();
					var master = new Master
					{
						FirstName = user.FirstName,
						LastName = user.LastName,
						UniqueDescription = $"Master №{id++}: {firstName} {lastName}",
					};
					await db.Masters.AddAsync(master);
					await db.SaveChangesAsync();
					break;
				}
			}

		}
	}
}
