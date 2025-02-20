using Budget_App.Data;
using Budget_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Budget_App.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BudgetContext _context;

        public HomeController(ILogger<HomeController> logger, BudgetContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Year/{year}")]
        public async Task<IActionResult> GetYear(int year)
        {
            Console.WriteLine(year);

            var yearObj = await _context.Years
                .Include(y => y.Months)
                    .ThenInclude(m => m.Earnings)
                .Include(y => y.Months)
                    .ThenInclude(m => m.Expenses)
                .FirstOrDefaultAsync(y => y.YearValue == year);

            if (yearObj == null)
            {
                return NotFound();
            }
            return Json(yearObj);
        }


        [HttpPost]
        public async Task<IActionResult> AddYear([FromBody] Year year)
        {
            if (ModelState.IsValid)
            {
                _context.Years.Add(year);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return Json(year);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteYear(int id)
        {
            var year = await _context.Years.FindAsync(id);
            if (year == null)
            {
                return NotFound();
            }
            _context.Years.Remove(year);
            await _context.SaveChangesAsync();
            return Json(year);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
