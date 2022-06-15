using HW.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public IActionResult Categories(Category category, Prices price)
        {            
            _catalog.Products.Add(category);
            //_catalog.PricesDict.TryAdd(price); не работает! не знаю почему!
            return View(_catalog);
        }
        public IActionResult Products(Category category, Prices price)
        {          
            _catalog.Products.Remove(category);         
            //_catalog.PricesDict.Remove(price); не работает! не знаю почему!
            return View(_catalog);
        }

    }
}