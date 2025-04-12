using RealEase.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEase.Persistence.Context;
using RealEase.Domain.Entities;


namespace RealEase.Web.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly RealEaseDbContext _context;


        public PaymentsController(RealEaseDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {

            var vm = new PaymentsViewModel();
            //var allPayments = _context.Payments.ToList();
            //var activePayments = _context.Payments.Where(static st => st.IsActive && !st.IsActive).ToList();
            //ViewBag.PaymentInformation = info2;

            //vm.ActivePayments = activePayments;
            //vm.AllPayments = allPayments;
            return View(vm);
        }

        // GET: Payments1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Payment == null)
            {
                return NotFound();
            }

            return View(Payment);
        }

        // GET: Payments1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payments1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment Payments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Payments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Payments);
        }

        // GET: Payments1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Payment = await _context.Payments.FindAsync(id);
            if (Payment == null)
            {
                return NotFound();
            }
            return View(Payment);
        }

        // POST: Payments1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Lastname,Address,IsActive")] Payment Payment)
        {
            if (id != Payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(Payment.Id))
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
            return View(Payment);
        }

        // GET: Payments1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Payment == null)
            {
                return NotFound();
            }

            return View(Payment);
        }

        // POST: Payments1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Payment = await _context.Payments.FindAsync(id);
            if (Payment != null)
            {
                _context.Payments.Remove(Payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}

