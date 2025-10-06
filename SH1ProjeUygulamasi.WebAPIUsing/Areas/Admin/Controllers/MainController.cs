using Microsoft.AspNetCore.Mvc;

namespace SH1ProjeUygulamasi.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
