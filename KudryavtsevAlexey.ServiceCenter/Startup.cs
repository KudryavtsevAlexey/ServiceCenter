using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.Profiles;
using KudryavtsevAlexey.ServiceCenter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace KudryavtsevAlexey.ServiceCenter
{
	public class Startup
	{
		private readonly IConfiguration Configuration;
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddRazorPages().AddRazorRuntimeCompilation();

			services.AddDbContext<ApplicationContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("DefaultSqlServer"));
			})
				.AddIdentity<ApplicationUser, IdentityRole>(config=>
				{
					config.Password.RequireDigit = false;
					config.Password.RequiredLength = 6;
					config.Password.RequireLowercase = true;
					config.Password.RequireNonAlphanumeric = false;
					config.Password.RequireUppercase = true;
					config.Password.RequiredUniqueChars = 0;
				})
				.AddEntityFrameworkStores<ApplicationContext>();

			services.ConfigureApplicationCookie(config =>
			{
				config.LoginPath = "/Account/Login";
				config.AccessDeniedPath = "/Account/AccessDenied";
			});

			services.AddAuthorization(config=>
			{
				config.AddPolicy("Administrator", builder =>
				{
					builder.RequireAssertion(u => u.User.HasClaim(ClaimTypes.Role, "Administrator"));
				});

				config.AddPolicy("Master", builder =>
				{
					builder.RequireAssertion(u => u.User.HasClaim(ClaimTypes.Role, "Administrator") 
												|| u.User.HasClaim(ClaimTypes.Role, "Master"));
				});

				config.AddPolicy("Client", builder =>
				{
					builder.RequireAssertion(u => u.User.HasClaim(ClaimTypes.Role, "Administrator")
												|| u.User.HasClaim(ClaimTypes.Role, "Master") 
												|| u.User.HasClaim(ClaimTypes.Role, "Client"));
				});
			});

			services.AddAutoMapper(typeof(MappingProfile));
			services.AddScoped<IMasterService, MasterService>();
			services.AddScoped<IOrderService, OrderService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
