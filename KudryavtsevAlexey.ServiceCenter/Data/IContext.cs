using KudryavtsevAlexey.ServiceCenter.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Data
{
    public interface IContext
    {
        DbSet<Client> Clients { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<Master> Masters { get; set; }
        DbSet<Order> Orders { get; set; }

        Task<int> CustomSaveChangesAsync();
    }
}
