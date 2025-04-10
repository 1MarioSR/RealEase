using RealEase.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEase.Persistence.Context;
using RealEase.Domain.Entities;


namespace RealEase.Web.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly RealEaseDbContext _context;


        public PropertiesController(RealEaseDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {

            var vm = new PropertiesViewModel();
            var allProperties = _context.Properties.ToList();
            //var activeProperties = _context.Properties.Where(static st => st.IsActive && !st.IsActive).ToList();
            //ViewBag.PropertieInformation = info2;

            //vm.ActiveProperties = activeProperties;
            //vm.AllProperties = allProperties;
            return View(vm);
        }

        // GET: Properties1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Propertie = await _context.Properties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Propertie == null)
            {
                return NotFound();
            }

            return View(Propertie);
        }

        // GET: Properties1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Properties1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Propertie Properties)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Properties);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Properties);
        }

        // GET: Properties1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Propertie = await _context.Properties.FindAsync(id);
            if (Propertie == null)
            {
                return NotFound();
            }
            return View(Propertie);
        }

        // POST: Properties1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Lastname,Address,IsActive")] Propertie Propertie)
        {
            if (id != Propertie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Propertie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertieExists(Propertie.Id))
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
            return View(Propertie);
        }

        // GET: Properties1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Propertie = await _context.Properties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Propertie == null)
            {
                return NotFound();
            }

            return View(Propertie);
        }

        // POST: Properties1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Propertie = await _context.Properties.FindAsync(id);
            if (Propertie != null)
            {
                _context.Properties.Remove(Propertie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertieExists(int id)
        {
            return _context.Properties.Any(e => e.Id == id);
        }
    }
}

