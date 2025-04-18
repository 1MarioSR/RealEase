using Microsoft.AspNetCore.Mvc;
using RealEase.Persistence.Context;

namespace RealEase.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly RealEaseDbContext _context;

        public UsersController(RealEaseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Details(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return View(user); 
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return View(user);
        }
    }
}
