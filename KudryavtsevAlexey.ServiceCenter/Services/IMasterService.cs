using KudryavtsevAlexey.ServiceCenter.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Services
{
	public interface IMasterService
	{
		public Task<List<MasterViewModel>> GetAllMasters();
	}
}
