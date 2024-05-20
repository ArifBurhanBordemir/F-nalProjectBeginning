//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;
//using FinalProjectBeginning.Data;
//using FınalProjectBeginning.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//public class KulsayfasiViewComponent : ViewComponent
//{
//    private readonly UserManager<CetUser> _userManager;
//    private readonly ApplicationDbContext _context;

//    public KulsayfasiViewComponent(UserManager<CetUser> userManager, ApplicationDbContext context)
//    {
//        _userManager = userManager;
//        _context = context;
//    }

//    public async Task<IViewComponentResult> InvokeAsync()
//    {
//        if (HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated)
//        {
//            var user = await _userManager.GetUserAsync(HttpContext.User);
//            var userId = await _userManager.GetUserIdAsync(user);
//            var kulsayfasi = await _context.Kulsayfasis.FirstOrDefaultAsync(k => k.CetUserId == userId);
//            return View(kulsayfasi);
//        }
//        else
//        {
//            // Kullanıcı oturum açmamış veya yetkilendirilmemişse, uygun bir işlem yapabilirsiniz.
//            // Örneğin, kullanıcıyı oturum açma sayfasına yönlendirebilirsiniz.
//            return Content("Kullanıcı oturum açmamış veya yetkilendirilmemiş.");
//        }
//    }


//}
