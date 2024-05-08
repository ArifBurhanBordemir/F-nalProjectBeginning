using FinalProjectBeginning.Data;

using FınalProjectBeginning.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FınalProjectBeginning.Controllers
{
    public class HomeController : Controller
    {
        
       
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var sortedEvents = _context.Events.OrderByDescending(b => b.ReadCount).ToList();
            return View(sortedEvents);
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



        [Authorize(Roles = "Administrator")]
        public ActionResult Admin()
        {
            return View();
        }
    }
}
