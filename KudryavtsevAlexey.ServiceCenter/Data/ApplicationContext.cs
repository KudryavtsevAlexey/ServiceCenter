using KudryavtsevAlexey.ServiceCenter.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>, IContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
		{

		}

		public DbSet<Client> Clients { get; set; }
		public DbSet<Device> Devices { get; set; }
		public DbSet<Master> Masters { get; set; }
		public DbSet<Order> Orders { get; set; }

		public async Task<int> CustomSaveChangesAsync()
        {
			return await SaveChangesAsync();
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Client>()
				.HasMany(o => o.Orders)
				.WithOne(c => c.Client)
				.IsRequired();

			builder.Entity<Client>()
				.HasMany(d => d.Devices)
				.WithOne(c => c.Client)
				.IsRequired().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Device>()
				.HasOne(m => m.Master)
				.WithMany(d => d.Devices)
				.IsRequired();

			builder.Entity<Master>()
				.HasMany(o => o.Orders)
				.WithOne(m => m.Master);

			builder.Entity<Master>()
				.HasMany(d => d.Devices)
				.WithOne(m => m.Master)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Order>()
				.Property(o => o.AmountToPay)
				.HasColumnType("decimal(7,2)");

			base.OnModelCreating(builder);
		}
    }
}
