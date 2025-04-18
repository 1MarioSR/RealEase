using Microsoft.AspNetCore.Mvc;
using RealEase.Persistence.Context;

namespace RealEase.Web.Controllers
{
    public class ContractsController : Controller
    {
        private readonly RealEaseDbContext _context;

        public ContractsController(RealEaseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Details(int id)
        {
            var contract = _context.Contracts.FirstOrDefault(u => u.Id == id);
            if (contract == null)
                return NotFound();

            return View(contract);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contract = _context.Contracts.FirstOrDefault(u => u.Id == id);
            if (contract == null)
                return NotFound();

            return View(contract);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contract = _context.Contracts.FirstOrDefault(u => u.Id == id);
            if (contract == null)
                return NotFound();

            return View(contract);
        }
    }
}
