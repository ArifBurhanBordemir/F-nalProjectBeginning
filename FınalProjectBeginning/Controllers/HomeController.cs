using FinalProjectBeginning.Data;

using FýnalProjectBeginning.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinalProjectBeginning.Controllers
{
    public class HomeController : Controller
    {
        
       
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
		private readonly UserManager<CetUser> _userManager;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context , UserManager<CetUser> userManager)
        {
            _context = context;
            _logger = logger;
			_userManager = userManager;
		}

		
		public  async Task<IActionResult> Index()
        {
            var sortedEvents = _context.Events.OrderByDescending(b => b.ReadCount).ToList();
            var applicationDbContext = _context.Events.Include(a => a.CetUser);
            return View(/*(sortedEvents, */await applicationDbContext.ToListAsync());



        }

        public async Task<IActionResult> Privacy()
		{

			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				ViewBag.UserName = user.UserName; // Kullanýcý adýný ViewBag ile View'e geçiriyoruz
			}

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
