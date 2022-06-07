using HW.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW.Controllers
{
    public class CatalogController : Controller
    {
        private static Catalog _catalog = new();
        [HttpGet]
        public IActionResult Categories()
        {
            return View(_catalog);
        }
        public IActionResult Products()
        {
            return View(_catalog);
        }
        [HttpPost]
        public IActionResult Categories(Category model)
        {
            _catalog.Products.Add(model);               
            return View(_catalog);
        }
    }
}