using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.Service.Abstract;
using SH1ProjeUygulamasi.WebUI.Models;
using SH1ProjeUygulamasi.WebUI.Tools;
using System.Diagnostics;

namespace SH1ProjeUygulamasi.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //private readonly DatabaseContext _context;

        //public HomeController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IService<Slider> _serviceSlider; // generic servis kullanýmý
        private readonly IProductService _productService;

        public HomeController(IService<Slider> serviceSlider, IProductService productService)
        {
            _serviceSlider = serviceSlider;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                Sliders = _serviceSlider.GetAll(), // _context.Sliders.ToList(),
                Products = _productService.GetProducts(p => p.IsActive && p.IsHome) // _context.Products.Where(p => p.IsActive && p.IsHome).ToList()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(string nameSurname, string email, string message)
        {
            string mesaj = $"Ad Soyad: {nameSurname} <hr> E mail: {email} <hr> Mesaj: {message} ";
            try
            {
                MailHelper.SendMail("mail@gmail.com", "Siteden email geldi", mesaj);
                TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Teþekkürler.. Mesajýnýz Gönderildi!</strong> 
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div> ";
            }
            catch (Exception)
            {
                TempData["Message"] = @"<div class=""alert alert-danger alert-dismissible fade show"" role=""alert"">
  <strong>Hata Oluþtu! Mesaj Gönderilemedi!</strong> 
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div> ";
            }
            
            return RedirectToAction("ContactUs");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
