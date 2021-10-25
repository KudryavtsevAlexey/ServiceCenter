using KudryavtsevAlexey.ServiceCenter.Models;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Services
{
    public interface IOrderService
	{
		public Task<Order> MapOrder(OrderViewModel model);
	}
}
