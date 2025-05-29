using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using RotativaPDFDemo.Models;
using System.Diagnostics;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Drawing;
using RotativaPDFDemo.Utility;

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

       
        public IActionResult WatermarkedView()
        {
            var userName = "Srikanth-999"; // User.Identity?.Name ?? "Anonymous";
            ViewBag.UserName = userName;

            return View();
        }

        [HttpGet]
        public IActionResult EncryptText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return BadRequest("Empty text");

            var encrypted = PdfEncryptionHelper.EncryptShort(text);
            return Content(encrypted);
        }

        [HttpGet]
        public IActionResult DecryptText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return BadRequest("Empty text");
           
         // string text = "9O6MDMuc4XU51mDSoQlFl05TZTikG4FWpc5kdCfdW5y8vNURd8yVCKci9geUD2Kw";

            string decrypted = PdfEncryptionHelper.DecryptShort(text);                      
            return Content(decrypted);
        }

        [HttpGet("view-watermarked")]
        public async Task<IActionResult> ViewWatermarkedPdf()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdf", "sample.pdf");
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            using var inputStream = new MemoryStream(bytes);
            using var outputStream = new MemoryStream();

            //  Your watermark logic using PDF-lib.js or QuestPDF (if done server-side)

            // For this example, just return the PDF directly (replace this with your generated stream)
            return File(outputStream.ToArray(), "application/pdf");
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

       

    public IActionResult AnnotatePdf()
    {
        string inputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdf", "sample.pdf");
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdf", "annotated_sample.pdf");

        try
        {
            using (var inputDocument = PdfReader.Open(inputPath, PdfDocumentOpenMode.Modify))
            {
                var firstPage = inputDocument.Pages[0];
                XGraphics gfx = XGraphics.FromPdfPage(firstPage);
                XFont font = new XFont("Arial", 10, XFontStyle.Bold);
                XBrush brush = new XSolidBrush(XColor.FromArgb(128, 0, 0, 0)); // Light grey

                string docNumber = "1244122/Doc/Out";
                double x = firstPage.Width - gfx.MeasureString(docNumber, font).Width - 30;
                double y = 50;

                gfx.DrawString(docNumber, font, brush, new XPoint(x, y));
                inputDocument.Save(outputPath);
            }

            TempData["Message"] = "Annotated PDF generated successfully!";
        }
        catch (Exception ex)
        {
            TempData["Message"] = $" to annotate PDF: {ex.Message}";
        }

        return RedirectToAction("Index");
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
