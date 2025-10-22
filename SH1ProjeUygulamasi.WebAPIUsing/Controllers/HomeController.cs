using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.WebAPIUsing.Models;
using SH1ProjeUygulamasi.WebAPIUsing.Tools;
using System.Diagnostics;

namespace SH1ProjeUygulamasi.WebAPIUsing.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        static string _apiAdres = "http://localhost:5018/Api/";

        public async Task<IActionResult> IndexAsync()
        {
            var model = new HomePageViewModel
            {
                Sliders = await _httpClient.GetFromJsonAsync<List<Slider>>(_apiAdres + "Sliders"),
                Products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products/GetHomePageProducts")
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
