using Microsoft.AspNetCore.Mvc;
using RealEase.Persistence.Context;
using RealEase.Web.ViewsModels;


namespace RealEase.Web.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly RealEaseDbContext _context;

        public PaymentsController(RealEaseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Details(int id)
        {
            var payment = _context.Payments.FirstOrDefault(u => u.Id == id);
            if (payment == null)
                return NotFound();

            return View(payment);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var payment = _context.Payments.FirstOrDefault(u => u.Id == id);
            if (payment == null)
                return NotFound();

            return View(payment);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var payment = _context.Payments.FirstOrDefault(u => u.Id == id);
            if (payment == null)
                return NotFound();

            return View(payment);
        }
    }
}
