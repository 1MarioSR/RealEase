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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            ViewBag.PropertieId = id;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.PropertieId = id;
            return View();
        }

        public IActionResult Delete(int id)
        {
            ViewBag.PropertieId = id;
            return View();
        }
    }
}