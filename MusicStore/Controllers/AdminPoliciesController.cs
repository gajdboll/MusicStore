using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;

namespace MusicStore.Controllers
{
    public class AdminPoliciesController : Controller
    {

        private readonly MusicStoreContext _context;

        public AdminPoliciesController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: AdminPolicies
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdminPolicies.ToListAsync());
        }

        // GET: AdminPolicies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policies = await _context.AdminPolicies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policies == null)
            {
                return NotFound();
            }

            return View(policies);
        }

        // GET: AdminPolicies/Create
        public IActionResult Create()
        {
            return View(new AdminPolicies());
        }

        // POST: AdminPolicies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminPolicies policies)
        {
            if (ModelState.IsValid)
            {
                if (_context.AdminPolicies.Any(m => m.Position == policies.Position))
                {
                    TempData["error"] = "Position already in use";
                    return View(policies);
                }
                policies.DateAdded = DateTime.Now;
                policies.ModificationDate = DateTime.Now;
                _context.AdminPolicies.Add(policies);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Admin Policies got added";
                return RedirectToAction(nameof(Index));

            }
            return View(policies);
        }

        // GET: AdminPolicies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var policies = await _context.AdminPolicies.FindAsync(id);
            if (policies == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            return View(policies);
        }

        // POST: AdminPolicies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdminPolicies policies)
        {
            if (id != policies.Id)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool positionInUse = await _context.AdminPolicies
                        .AnyAsync(c => c.Position == policies.Position && c.Id != policies.Id);

                if (positionInUse)
                {
                    TempData["error"] = "Sorry Something Went Wrong";
                    return View(policies);
                }

                try
                {
                    var existingTop = await _context.AdminPolicies.FindAsync(id);

                    if (existingTop == null)
                    {
                        TempData["error"] = "Sorry Something Went Wrong";
                        return NotFound();
                    }

                    // update all the fields
                    existingTop.Name = policies.Name;
                    existingTop.Descriptions = policies.Descriptions;
                    existingTop.isActive = policies.isActive;
                    existingTop.ModificationDate = DateTime.Now;
                    existingTop.Position = policies.Position;

                    _context.Update(existingTop);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Admin Policies got Updated";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminPoliciesExists(policies.Id))
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
            return View(policies);
        }

        // GET: AdminPolicies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var policies = await _context.AdminPolicies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policies == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(policies);
        }

        // POST: AdminPolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policies = await _context.AdminPolicies.FindAsync(id);
            if (policies != null)
            {
                policies.isActive = false;
                policies.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "AdminPolicies got Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool AdminPoliciesExists(int id)
        {
            return _context.AdminPolicies.Any(e => e.Id == id);
        }
    }
}
