using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;
 

namespace MusicStore.Controllers
{
    public class CustomerQueryController : Controller
    {
        private readonly MusicStoreContext _context;

        public CustomerQueryController(MusicStoreContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerQuery.ToListAsync());
        }
        // GET: CustomerQuery/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerQuery = await _context.CustomerQuery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerQuery == null)
            {
                return NotFound();
            }

            return View(customerQuery);
        }


        public IActionResult Create()
        {

            return View( );
        }
        // POST: CustomerQuery/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerQuery customerQuery)
        {
            if (ModelState.IsValid)
            {
                if (_context.CustomerQuery.Any(m => m.Position == customerQuery.Position))
                {
                    TempData["error"] = "Position already in use";
                    return RedirectToAction("ShowContactUs", "Home", new { id = 3 });
                }
                customerQuery.DateAdded = DateTime.Now;
                customerQuery.ModificationDate = DateTime.Now;
                customerQuery.IsAnswered = false;
                _context.CustomerQuery.Add(customerQuery);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Your query has been submitted successfully!";
                return RedirectToAction("ShowContactUs", "Home", new { id = 3 });
            }
            return RedirectToAction("ShowContactUs", "Home", new { id = 3 });
        }

        // GET: CustomerQuery/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var CustomerQuery = await _context.CustomerQuery.FindAsync(id);
            if (CustomerQuery == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }
            return View(CustomerQuery);
        }

        // POST: CustomerQuery/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Edit")]

        public async Task<IActionResult> Edit(int id )
        {
            var customerQuery = await _context.CustomerQuery.FindAsync(id);

            if (customerQuery != null)
            {
                customerQuery.IsAnswered = true;
                customerQuery.ModificationDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Query got Replied";
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomerQuery/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            var customerQuery = await _context.CustomerQuery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerQuery == null)
            {
                TempData["error"] = "Sorry Something Went Wrong";
                return NotFound();
            }

            return View(customerQuery);
        }

        // POST: CustomerQuery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerQuery = await _context.CustomerQuery.FindAsync(id);
            if (customerQuery != null)
            {
                customerQuery.isActive = false;
                customerQuery.DateOfDelete = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Customer Query got Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerQueryExists(int id)
        {
            return _context.CustomerQuery.Any(e => e.Id == id);
        }
    }
}

 