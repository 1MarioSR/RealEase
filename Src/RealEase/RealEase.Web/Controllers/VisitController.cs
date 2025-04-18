using Microsoft.AspNetCore.Mvc;
using RealEase.Persistence.Context;

namespace RealEase.Web.Controllers
{
    public class VisitsController : Controller
    {
        private readonly RealEaseDbContext _context;

        public VisitsController(RealEaseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Details(int id)
        {
            var visit = _context.Visits.FirstOrDefault(u => u.Id == id);
            if (visit == null)
                return NotFound();

            return View(visit);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var visit = _context.Visits.FirstOrDefault(u => u.Id == id);
            if (visit == null)
                return NotFound();

            return View(visit);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var visit = _context.Visits.FirstOrDefault(u => u.Id == id);
            if (visit == null)
                return NotFound();

            return View(visit);
        }
    }
}
