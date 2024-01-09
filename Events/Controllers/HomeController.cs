using Events.Models;
using Events.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Events.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICalendarRepo _calendarRepo;

        public HomeController(ILogger<HomeController> logger, ICalendarRepo calendarRepo)
        {
            _logger = logger;
            _calendarRepo = calendarRepo;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Now;

            return View(new CalendarViewModel 
            {
                EventsThisMonth = await _calendarRepo.GetAllEventsForThisMonth(today).ConfigureAwait(false),
                ThisMonth = today
            });
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
