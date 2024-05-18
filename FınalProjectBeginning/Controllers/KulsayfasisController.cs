using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProjectBeginning.Data;
using FınalProjectBeginning.Models;
using Microsoft.SqlServer.Management.XEvent;
using Microsoft.AspNetCore.Identity;

namespace FınalProjectBeginning.Controllers
{
    public class KulsayfasisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CetUser> _userManager;



        public KulsayfasisController(ApplicationDbContext context, UserManager<CetUser> userManager)
        {
            _context = context;

            _userManager = userManager;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Kulsayfasis.Include(p => p.CetUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kulsayfasi = await _context.Kulsayfasis
                .Include(p => p.CetUser)
                .ThenInclude(u => u.Posts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kulsayfasi == null)
            {
                return NotFound();
            }

            return View(kulsayfasi);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CetUserId"] = new SelectList(_context.CetUsers, "Id", "Id");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile postImage, [Bind("Id,Description,CetUserId")] Kulsayfasi kulsayfasi)
        {
            var filename = postImage.FileName;
            var extension = Path.GetExtension(filename);
            var newfilename = Guid.NewGuid().ToString().ToLower().Replace("-", "") + extension;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", newfilename);

            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                await postImage.CopyToAsync(stream);
            }
            kulsayfasi.ImageName = newfilename;
            _context.Add(kulsayfasi);

            var CetUser = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            kulsayfasi.CetUserId = CetUser.Id;
            _context.Kulsayfasis.Add(kulsayfasi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Kulsayfasis.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CetUserId"] = new SelectList(_context.CetUsers, "Id", "Id", post.CetUserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,CetUserId")] Kulsayfasi kulsayfasi)
        {
            if (id != kulsayfasi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kulsayfasi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KulsayfasiExists(kulsayfasi.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CetUserId"] = new SelectList(_context.CetUsers, "Id", "Id", kulsayfasi.CetUserId);
            return View(kulsayfasi);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kulsayfasi = await _context.Kulsayfasis
                .Include(p => p.CetUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kulsayfasi == null)
            {
                return NotFound();
            }

            return View(kulsayfasi);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var kulsayfasi = await _context.Kulsayfasis.FindAsync(id);
            if (kulsayfasi != null)
            {
                _context.Kulsayfasis.Remove(kulsayfasi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

















        public async Task<IActionResult> MyPosts()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            var cetUser = await _context.CetUsers
                .Include(c => c.Posts) // Posts koleksiyonunu dahil edin
                .FirstOrDefaultAsync(c => c.Id == user.Id); // IdentityUserId'nin user.Id ile eşleştiğinden emin olun

            if (cetUser == null)
            {
                return NotFound();
            }

            var posts = cetUser.Posts; // Kullanıcının postlarını alın
            return View(posts);
        }
    


















    private bool KulsayfasiExists(int id)

    {
        return _context.Kulsayfasis.Any(e => e.Id == id);
    }

}
}
