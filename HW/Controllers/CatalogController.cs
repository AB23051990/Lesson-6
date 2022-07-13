using HW.Models;
using HW.Models.MailKit;
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


        private static InvalidPropertyException _catalog = new();

        private readonly IEmailSender _emailSender;

        public CatalogController (IEmailSender emailSender, InvalidPropertyException catalog)
        {
            _emailSender = emailSender;
            _catalog = catalog;
        }

        [HttpGet]
        public IActionResult Categories()
        {
            return View(_catalog);
        }        
        public IActionResult Products()
        {
            return View(_catalog);
        }
        public IActionResult SendMessage()
        {
            return View(_catalog);
        }

        [HttpPost]
        public IActionResult Categories(Category category)
        {            
            //_catalog.Product.Add(category);
            _emailSender.Send(senderName: "Оповещатель", to: "AB23051990@yandex.ru", subject: "",  htmlBody: "", senderEmail: "");
            
            return View(_catalog);
        }
       
    }
}