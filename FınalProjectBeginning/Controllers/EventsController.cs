using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using FınalProjectBeginning.Models;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Drawing;
using FinalProjectBeginning.Data;


namespace FınalProjectBeginning.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Events.Include(b => b.CetUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(b => b.CetUser)
                .Include(c=>c.Menus)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            if (@event.ReadCount == null)
            { @event.ReadCount = 1; }
            else { @event.ReadCount++; }
            await _context.SaveChangesAsync();

            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = "Restoran")]
        
        public IActionResult Create()
        {
            ViewData["CetUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Body,CreatedDate,EventDate,EventLocation,CetUserId")] Event @event)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        @event.CreatedDate = DateTime.Now;
        //        var CetUser = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
        //        @event.CetUserId = CetUser.Id;
        //        _context.Add(@event);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CetUserId"] = new SelectList(_context.Set<CetUser>(), "Id", "Id", @event.CetUserId);
        //    return View(@event);
        //}

        public async Task<IActionResult> Create(IFormFile eventImage,[Bind("Id,Title,Body,CreatedDate,EventDate,EventLocation,CetUserId")] Event @event)
        {
            var filename = eventImage.FileName;
            var extension = Path.GetExtension(filename);
            var newfilename = Guid.NewGuid().ToString().ToLower().Replace("-","")+ extension;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", newfilename);

            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                await eventImage.CopyToAsync(stream);
            }
            @event.ImageName = newfilename;
            _context.Add(@event);

            @event.CreatedDate = DateTime.Now;
            var CetUser = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            @event.CetUserId = CetUser.Id;
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{
            //    _context.Add(@event);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CetUserId"] = new SelectList(_context.Users, "Id", "Id", @event.CetUserId);
            //return View(@event);
        }

        ////public async Task<IActionResult> Create([Bind("Id,Title,Body,CreatedDate,CetUserId")] Blog blog)
        ////{
        ////    blog.CreatedDate = DateTime.Now;
        ////    var CetUser = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
        ////    blog.CetUserId = CetUser.Id;
        ////    _context.Blogs.Add(blog);
        ////    await _context.SaveChangesAsync();
        ////    return RedirectToAction(nameof(Index));

        ////    if (ModelState.IsValid)
        ////    {
        ////        _context.Add(blog);
        ////        await _context.SaveChangesAsync();
        ////        return RedirectToAction(nameof(Index));
        ////    }
        ////    ViewData["CetUserId"] = new SelectList(_context.Users, "Id", "Id", blog.CetUserId);
        ////    return View(blog);
        ////}


        //public async Task<IActionResult> Create([Bind("Id,Title,Body,CreatedDate,EventDate,EventLocation,CetUserId")] Event event)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        blog.CreatedDate = DateTime.Now;
        //        _context.Add(@event);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CetUserId"] = new SelectList(_context.Set<CetUser>(), "Id", "Id", @event.CetUserId);
        //    return View(@event);
        //}
        // GET: Events/Edit/5

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            var userID = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;
            if (@event.CetUserId != userID) { return Unauthorized(); }


            ViewData["CetUserId"] = new SelectList(_context.Set<CetUser>(), "Id", "Id", @event.CetUserId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,CreatedDate,EventDate,EventLocation,CetUserId")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            ViewData["CetUserId"] = new SelectList(_context.Set<CetUser>(), "Id", "Id", @event.CetUserId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(b => b.CetUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            var userID = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;
            if (@event.CetUserId != userID) { return Unauthorized(); }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddMenu(int id, [Bind("Name,Description,Fee")] Menu menu)
        //{
        //    var @event = await _context.Events.FindAsync(id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Eğer menü için etkinlik zaten belirlenmişse, ilişkilendirme yap
        //        menu.EventId = id.ToString();

        //        // Menüyü ekleyip kaydet
        //        _context.Menus.Add(menu);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Details), new { id });
        //    }

        //    // ModelState geçersiz ise, etkinlik detaylarının olduğu sayfaya geri dön
        //    return RedirectToAction(nameof(Details), new { id });
        //}


    }
}
