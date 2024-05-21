// ViewComponent
using FinalProjectBeginning.Data;
using FınalProjectBeginning.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class KulsayfasiViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<CetUser> _userManager;

    public KulsayfasiViewComponent(ApplicationDbContext context, UserManager<CetUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            var kulsayfasi = await _context.Kulsayfasis.FirstOrDefaultAsync(k => k.CetUserId == userId);
            return View(kulsayfasi);
        }
        return View(null);
    }
}
