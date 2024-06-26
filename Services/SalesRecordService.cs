using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
	public class SalesRecordService
	{
		private readonly SalesWebMvcContext _context;
		public SalesRecordService(SalesWebMvcContext context)
		{
			_context = context; 
		}
		public async Task< List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
		{
			var result = from obj in _context.SalesRecord select obj;

			if (minDate.HasValue)
			{
				result = result.Where((item => item.Date >= minDate.Value));
			}
			if (maxDate.HasValue)
			{
				result = result.Where((item => item.Date <= maxDate.Value));
			}
			return await result
				.Include(item => item.Seller)
				.Include(item=> item.Seller.DepartmentId)
				.OrderByDescending(item => item.Date)
				.ToListAsync();
		}
	}
}
