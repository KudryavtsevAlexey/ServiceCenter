using AutoMapper;
using KudryavtsevAlexey.ServiceCenter.Data;
using KudryavtsevAlexey.ServiceCenter.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Services
{
	public class MasterService : IMasterService
	{
		private readonly ApplicationContext _db;
		private readonly IMapper _mapper;

		public MasterService(ApplicationContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		public IMapper Mapper { get; }

		public async Task<List<MasterViewModel>> GetAllMasters()
		{
			var allMasters = await _db.Masters
				.Include(m => m.Orders)
				.Select(m => new MasterViewModel {
					MasterId = m.MasterId,
					UniqueDescription = m.UniqueDescription,
					OrdersCount = m.Orders.Count()
				})
				.ToListAsync();

			allMasters.OrderBy(m => m.OrdersCount);

			var masters = new List<MasterViewModel>();
			foreach (var master in allMasters)
			{
				master.UniqueDescription += $"; Orders count = {master.OrdersCount}";
				masters.Add(master);
			}

			return masters;
		}
	}
}
