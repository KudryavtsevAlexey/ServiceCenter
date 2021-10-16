using AutoMapper;
using KudryavtsevAlexey.ServiceCenter.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KudryavtsevAlexey.ServiceCenter.Controllers
{
	[Authorize(Policy ="Master")]
	public class PanelController : Controller
	{
		private readonly ApplicationContext _db;
		private readonly IMapper _mapper;

		public PanelController(ApplicationContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		public async Task<IActionResult> ManageOrder()
		{
			var orders = await _db.Orders
				.Include(o => o.Client)
				.Include(o => o.Device)
				.Include(o => o.Master)
				.ToListAsync();
			return View(orders);
		}
	}
}
