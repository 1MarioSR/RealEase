using RealEase.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEase.Persistence.Context;
using RealEase.Domain.Entities;
using RealEase.Web.ViewsModels;


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
            ViewBag.VisitId = id;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.VisitId = id;
            return View();
        }

        public IActionResult Delete(int id)
        {
            ViewBag.VisitId = id;
            return View();
        }
    }
}