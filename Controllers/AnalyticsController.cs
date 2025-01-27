using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetManagerAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;


namespace BudgetManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : Controller
    {
        private readonly BudgetContext _context;
        public AnalyticsController(BudgetContext context)
        {
            _context = context;
        }


        //get för API/Analytics/Cate
        [HttpGet("CategorySummary")]
        public async Task<ActionResult<IEnumerable<CategorySummaryDto>>> GetCategorySummary()
        {
            var Summary = await _context.Transactions
                .Include(t => t.category)
                .GroupBy(t => t.category.Name)
                .Select(g => new CategorySummaryDto
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .ToListAsync();
            return Summary;

        }

        //get för API/Analytics/dateSummary

        [HttpGet("DateSummary")]
        public async Task<ActionResult<IEnumerable<DateSummaryDto>>> GetDateSummary()
        {
            var summary = await _context.Transactions
                .GroupBy(t => t.Date.Date)
                .Select(g => new DateSummaryDto
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .ToListAsync();
            return summary;

        }
}

}