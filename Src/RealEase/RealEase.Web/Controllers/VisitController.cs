using RealEase.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEase.Persistence.Context;
using RealEase.Domain.Entities;


namespace RealEase.Web.Controllers
{
    public class VisitsController : Controller
    {
        private readonly RealEaseDbContext _context;


        public VisitsController(RealEaseDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {

            var vm = new VisitsViewModel();
            //var allVisits = _context.Visits.ToList();
            //var activeVisits = _context.Visits.Where(static st => st.IsActive && !st.IsActive).ToList();
            //ViewBag.VisitInformation = info2;

            //vm.ActiveVisits = activeVisits;
            //vm.AllVisits = allVisits;
            return View(vm);
        }

        // GET: Visits1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Visit = await _context.Visits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Visit == null)
            {
                return NotFound();
            }

            return View(Visit);
        }

        // GET: Visits1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visits1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Visit Visits)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Visits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Visits);
        }

        // GET: Visits1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Visit = await _context.Visits.FindAsync(id);
            if (Visit == null)
            {
                return NotFound();
            }
            return View(Visit);
        }

        // POST: Visits1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Lastname,Address,IsActive")] Visit Visit)
        {
            if (id != Visit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(Visit.Id))
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
            return View(Visit);
        }

        // GET: Visits1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Visit = await _context.Visits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Visit == null)
            {
                return NotFound();
            }

            return View(Visit);
        }

        // POST: Visits1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Visit = await _context.Visits.FindAsync(id);
            if (Visit != null)
            {
                _context.Visits.Remove(Visit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
            return _context.Visits.Any(e => e.Id == id);
        }
    }
}

