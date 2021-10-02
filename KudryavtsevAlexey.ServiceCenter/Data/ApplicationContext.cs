using KudryavtsevAlexey.ServiceCenter.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KudryavtsevAlexey.ServiceCenter.Data
{
	public class ApplicationContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
		{

		}

		public DbSet<Client> Clients { get; set; }
		public DbSet<Device> Devices { get; set; }
		public DbSet<Master> Masters { get; set; }
		public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Client>()
				.HasKey(c => c.ClientId);

			builder.Entity<Client>()
				.HasOne(o => o.Order)
				.WithOne(c => c.Client)
				.IsRequired();

			builder.Entity<Client>()
				.HasMany(d => d.Devices)
				.WithOne(c => c.Client)
				.IsRequired().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Device>()
				.HasKey(d => d.DeviceId);

			builder.Entity<Device>()
				.HasOne(c => c.Client)
				.WithMany(d => d.Devices)
				.IsRequired().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Device>()
				.HasOne(m => m.Master)
				.WithMany(d => d.Devices)
				.IsRequired();

			builder.Entity<Device>()
				.HasOne(o => o.Order)
				.WithOne(d => d.Device)
				.IsRequired();

			builder.Entity<Master>()
				.HasKey(m => m.MasterId);

			builder.Entity<Master>()
				.HasOne(o => o.Order)
				.WithOne(m => m.Master);

			builder.Entity<Master>()
				.HasMany(d => d.Devices)
				.WithOne(m => m.Master)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Order>()
				.HasKey(o => o.OrderId);

			builder.Entity<Order>()
				.HasOne(c => c.Client)
				.WithOne(o => o.Order);

			builder.Entity<Order>()
				.HasOne(d=>d.Device)
				.WithOne(o=>o.Order);

			builder.Entity<Order>()
				.HasOne(m=>m.Master)
				.WithOne(o => o.Order);

			builder.Entity<Order>()
				.Property(o => o.AmountToPay)
				.HasColumnType("decimal(7,2)");

			base.OnModelCreating(builder);
		}
	}
}
