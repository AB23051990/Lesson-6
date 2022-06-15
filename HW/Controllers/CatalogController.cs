using HW.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(ILogger<CatalogController> logger)
        {
            _logger = logger;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


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
        public IActionResult Categories(Category category, Prices prices)
        {
            _catalog.Products.Add(category);
            return View(_catalog);
        }
        


    }
}