using Budget_App.Data;
using Budget_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Budget_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BudgetContext _context;

        public HomeController(ILogger<HomeController> logger, BudgetContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("")]
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


        [HttpPost("AddYear")]
        public async Task<IActionResult> AddYear([FromBody] Year year)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(year.YearValue);

                year.Months = new List<Month>();

                for (int monthIndex = 1; monthIndex <= 12; monthIndex++)
                {
                    var month = new Month
                    {
                        Name = new DateTime(year.YearValue, monthIndex, 1).ToString("MMMM"),
                        YearId = year.Id,
                    };

                    month.Expenses = new Expenses
                    {
                        MonthId = month.Id,
                    };

                    month.Earnings = new Earnings
                    {
                        MonthId = month.Id,
                    };

                    year.Months.Add(month);
                }

                _context.Years.Add(year);
                await _context.SaveChangesAsync();

                // return RedirectToAction("Index");
            }

            return Json(year);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExpenses([FromBody] Expenses expenses)
        {
            if (ModelState.IsValid)
            {
                var existingExpense = await _context.Expenses.FindAsync(expenses.Id);

                if (existingExpense == null)
                {
                    return NotFound();
                }

                existingExpense.Rent = expenses.Rent;
                existingExpense.Tithing = expenses.Tithing;
                existingExpense.Fast = expenses.Fast;
                existingExpense.Groceries = expenses.Groceries;
                existingExpense.Gas = expenses.Gas;
                existingExpense.CarInsurance = expenses.CarInsurance;
                existingExpense.Medical = expenses.Medical;
                existingExpense.EatOut = expenses.EatOut;
                existingExpense.Vacation = expenses.Vacation;
                existingExpense.Holiday = expenses.Holiday;
                existingExpense.Misc = expenses.Misc;

                await _context.SaveChangesAsync();

                return Json(existingExpense);
            }

            return Json(expenses);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEarnings([FromBody] Earnings earnings)
        {
            if (ModelState.IsValid)
            {
                var existingEarnings = await _context.Earnings.FindAsync(earnings.Id);

                if (existingEarnings == null)
                {
                    return NotFound();
                }

                existingEarnings.Primary = earnings.Primary;
                existingEarnings.Secondary = earnings.Secondary;
                existingEarnings.Gifts = earnings.Gifts;

                await _context.SaveChangesAsync();

                return Json(existingEarnings);
            }

            return Json(earnings);
        }

        [HttpDelete("DeleteYear/{id}")]
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

        [Route("{*url}")]
        public IActionResult HandleUnknownRoute(string url)
        {
            _logger.LogWarning($"Unknown route accessed: {url}");

            return Json("Unknown route");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
