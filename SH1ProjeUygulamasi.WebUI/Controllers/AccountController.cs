using Microsoft.AspNetCore.Mvc;

namespace SH1ProjeUygulamasi.WebUI.Controllers
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
    }
}
