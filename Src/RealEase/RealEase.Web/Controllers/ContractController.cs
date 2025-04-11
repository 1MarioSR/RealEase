using RealEase.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEase.Persistence.Context;
using RealEase.Domain.Entities;


namespace RealEase.Web.Controllers
{
    public class ContractsController : Controller
    {
        private readonly RealEaseDbContext _context;


        public ContractsController(RealEaseDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {

            var vm = new ContractsViewModel();
            var allContracts = _context.Contracts.ToList();
            //var activeContracts = _context.Contracts.Where(static st => st.IsActive && !st.IsActive).ToList();
            //ViewBag.ContractInformation = info2;

            //vm.ActiveContracts = activeContracts;
            //vm.AllContracts = allContracts;
            return View(vm);
        }

        // GET: Contracts1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Contract = await _context.Contracts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Contract == null)
            {
                return NotFound();
            }

            return View(Contract);
        }

        // GET: Contracts1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contracts1/Create
        // To protect from overposting attacks, enable the specific Contracts you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract Contracts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Contracts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Contracts);
        }

        // GET: Contracts1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Contract = await _context.Contracts.FindAsync(id);
            if (Contract == null)
            {
                return NotFound();
            }
            return View(Contract);
        }

        // POST: Contracts1/Edit/5
        // To protect from overposting attacks, enable the specific Contracts you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Lastname,Address,IsActive")] Contract Contract)
        {
            if (id != Contract.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(Contract.Id))
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
            return View(Contract);
        }

        // GET: Contracts1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Contract = await _context.Contracts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Contract == null)
            {
                return NotFound();
            }

            return View(Contract);
        }

        // POST: Contracts1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Contract = await _context.Contracts.FindAsync(id);
            if (Contract != null)
            {
                _context.Contracts.Remove(Contract);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.Id == id);
        }
    }
}

