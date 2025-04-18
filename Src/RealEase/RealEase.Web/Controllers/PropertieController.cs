using Microsoft.AspNetCore.Mvc;
using RealEase.Persistence.Context;

namespace RealEase.Web.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly RealEaseDbContext _context;

        public PropertiesController(RealEaseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Details(int id)
        {
            var propertie = _context.Properties.FirstOrDefault(u => u.Id == id);
            if (propertie == null)
                return NotFound();

            return View(propertie);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var propertie = _context.Properties.FirstOrDefault(u => u.Id == id);
            if (propertie == null)
                return NotFound();

            return View(propertie);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var propertie = _context.Properties.FirstOrDefault(u => u.Id == id);
            if (propertie == null)
                return NotFound();

            return View(propertie);
        }
    }
}
    
