using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using RotativaPDFDemo.Models;
using System.Diagnostics;

namespace RotativaPDFDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult HeaderPartial()
        {
            var rootPath = Directory.GetCurrentDirectory();
            ViewBag.RootPath = rootPath; // Now you can access @ViewBag.RootPath inside HeaderPartial.cshtml
            return View();
        }



        public IActionResult GeneratePdf()
        {
            var model = new MyViewModel
            {
                Name = "Example PDF Contract",
                ArabicContent = "هذا نص عربي لاختبار التصدير إلى PDF باستخدام Rotativa.",               
                RootPath = Directory.GetCurrentDirectory()

            };

            return new ViewAsPdf("ContractView", model)
            {
                FileName = "Rot_ContractDocument.pdf",
                PageSize = Size.A4,
                PageMargins = { Top = 30, Bottom = 20, Left = 10, Right = 10 },

                //CustomSwitches = $"--header-html \"https://localhost:44318/Home/HeaderPartial\" --header-spacing 1 --no-header-line --footer-center \"Page [page] of [toPage]\""

                CustomSwitches = $"--header-html \"https://localhost:44318/Home/HeaderPartial\" --header-spacing 1 --no-header-line --footer-center \"Page [page] of [toPage]\" --footer-font-size 6 --footer-font-name Arial"

              
            };

        }

        public IActionResult PreviewContract()
        {
            var model = new MyViewModel
            {
                Name = "Example PDF Contract",
                ArabicContent = "هذا نص عربي لاختبار التصدير إلى PDF باستخدام Rotativa.",
                RootPath = Directory.GetCurrentDirectory()
            };

            return View("ContractView", model);
        }


    }
}
