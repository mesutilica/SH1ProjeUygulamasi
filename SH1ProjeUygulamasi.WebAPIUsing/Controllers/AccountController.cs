using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;

namespace SH1ProjeUygulamasi.WebAPIUsing.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(); // çıkış yap
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.IsActive = true;
                    user.IsAdmin = false;
                    //_context.Users.Add(user);
                    //_context.SaveChanges();
                    //_userService.AddUser(user);
                    //_userService.Save();
                    TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Kayıt işlemi başarılı! Giriş yapabilirsiniz.</strong> 
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div> ";
                    return RedirectToAction("Login", "Account");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu!");
                }
            }
            return View(user);
        }
    }
}
