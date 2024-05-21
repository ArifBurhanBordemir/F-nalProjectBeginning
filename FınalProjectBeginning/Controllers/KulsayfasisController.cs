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
using FınalProjectBeginning.Migrations;
using System.Security.Claims;
using Microsoft.SqlServer.Management.Smo;

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
            
            var currentUserId = User.Identity.Name;

            var applicationDbContext = await _context.Kulsayfasis
                .Include(k => k.CetUser)
                .Select(k => new Kulsayfasi
                {
                    
                    Id = k.Id,
                    Description = k.Description,
                    CetUser = k.CetUser,
                    IsFollowedByCurrentUser = _context.Takip_Takipcis
                        .Any(tt => tt.TakipEdenUserId == currentUserId && tt.TakipEdilenKisiId == k.CetUser.Id)
                })
                .ToListAsync();

            


            return View(applicationDbContext);
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
                .Include(k => k.CetUser)
                .ThenInclude(c => c.TakipEdilenKisis)
                .Include(k => k.CetUser)
                .ThenInclude(c => c.TakipEdenUsers)
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
        public async Task<IActionResult> Create(IFormFile kulsayImage, [Bind("Id,Description,CetUserId")] Kulsayfasi kulsayfasi)
        {
            //var existingData = _context.Kulsayfasis.FirstOrDefault(k => k.CetUserId == kulsayfasi.CetUserId);

            
            //if (existingData != null)
            //{
            //    // Kullanıcı zaten bir veri oluşturmuş, yeni bir oluşturma işlemine izin verme
            //    //TempData["Message"] = "You have already created a data entry.";
            //    //return RedirectToAction(nameof(Index));

            //    return Unauthorized();
            //}

            var filename = kulsayImage.FileName;
            var extension = Path.GetExtension(filename);
            var newfilename = Guid.NewGuid().ToString().ToLower().Replace("-", "") + extension;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", newfilename);

            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                await kulsayImage.CopyToAsync(stream);
            }
            kulsayfasi.ImageName = newfilename;
            _context.Add(kulsayfasi);

            var CetUser = _context.Users.FirstOrDefault(u=>u.UserName == User.Identity.Name);
            kulsayfasi.CetUserId = CetUser.Id;
            _context.Kulsayfasis.Add(kulsayfasi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        // GET: Posts/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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



        public IActionResult Search(string searchString)
        {
            var users = from u in _context.Kulsayfasis.Include(k => k.CetUser)
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                //users = users.Where(s => s.CetUser.Name.Contains(searchString) || s.CetUser.Surname.Contains(searchString));

                // Kullanıcı adı veya soyadı arama metni ile başlıyorsa
                users = users.Where(s => s.CetUser.Name.StartsWith(searchString) || s.CetUser.Surname.StartsWith(searchString));
            }

            return View(users.ToList());
        }















        private bool KulsayfasiExists(int id)

    {
        return _context.Kulsayfasis.Any(e => e.Id == id);
    }















        //[HttpPost]
        //public IActionResult FollowUser([FromBody] string userId) 
        //{
        //    var currentUser = _context.Users.FirstOrDefault(u => u.Id == User.Identity.Name);
        //    var followedUser = _context.Users.FirstOrDefault(u => u.Id == userId);

        //    if (currentUser != null && followedUser != null)
        //    {
        //        var existingRelationship = _context.Takip_Takipcis.FirstOrDefault(r => r.TakipEdenUserId == currentUser.Id && r.TakipEdilenKisiId == followedUser.Id);

        //        if (existingRelationship != null)
        //        {
        //            _context.Takip_Takipcis.Remove(existingRelationship);
        //            _context.SaveChanges();
        //            return Json("unfollowed");
        //        }
        //        else
        //        {
        //            _context.Takip_Takipcis.Add(new Takip_Takipci { TakipEdenUserId = currentUser.Id, TakipEdilenKisiId = followedUser.Id });
        //            _context.SaveChanges();
        //            return Json("followed");
        //        }
        //    }

        //    return Json("error");
        //}



        [HttpPost]
        public IActionResult FollowUser(string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserExists = _context.CetUsers.Any(u => u.Id == currentUserId);
            var targetUserExists = _context.CetUsers.Any(u => u.Id == userId);

            if (!currentUserExists || !targetUserExists)
            {
                return Json("user_not_found");
            }

            var existingFollow = _context.Takip_Takipcis
                .FirstOrDefault(tt => tt.TakipEdenUserId == currentUserId && tt.TakipEdilenKisiId == userId);

            if (existingFollow == null)
            {
                var follow = new Takip_Takipci
                {
                    TakipEdenUserId = currentUserId,
                    TakipEdilenKisiId = userId
                };
                _context.Takip_Takipcis.Add(follow);
                _context.SaveChanges();
            }

            return Json("followed");
        }

        [HttpPost]
        public IActionResult UnfollowUser(string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentUserExists = _context.CetUsers.Any(u => u.Id == currentUserId);
            var targetUserExists = _context.CetUsers.Any(u => u.Id == userId);

            if (!currentUserExists || !targetUserExists)
            {
                return Json("user_not_found");
            }

            var follow = _context.Takip_Takipcis
                .FirstOrDefault(tt => tt.TakipEdenUserId == currentUserId && tt.TakipEdilenKisiId == userId);

            if (follow != null)
            {
                _context.Takip_Takipcis.Remove(follow);
                _context.SaveChanges();
            }

            return Json("unfollowed");
        }


































    }
}
