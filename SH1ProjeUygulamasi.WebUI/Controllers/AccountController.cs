using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Service.Abstract;
using System.Security.Claims;

namespace SH1ProjeUygulamasi.WebUI.Controllers
{
    public class AccountController : Controller
    {
        //private readonly DatabaseContext _context;

        //public AccountController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

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
            var kullanici = _userService.GetUser(u => u.Email == email && u.Password == password && u.IsActive); // _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.IsActive);
            if (kullanici != null)
            {
                var haklar = new List<Claim>() // kullanıcı hakları tanımladık
                    {
                        new(ClaimTypes.Email, kullanici.Email), // claim = hak(kullanıcıya tanımlalan haklar)
                        new(ClaimTypes.Role, kullanici.IsAdmin ? "Admin" : "User") // giriş yapan kullanıcı admin ise admin yetkisiyle değilse user yetkisiyle giriş yasın.
                    };
                var kullaniciKimligi = new ClaimsIdentity(haklar, "Login"); // kullanıcı için bir kimlik oluşturduk
                ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi);
                HttpContext.SignInAsync(claimsPrincipal); // yukardaki yetkilerle sisteme giriş yaptık
                return RedirectToAction("Index", "Home");
            }
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
                    _userService.AddUser(user);
                    _userService.Save();
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
