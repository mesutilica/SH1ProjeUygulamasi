using Microsoft.AspNetCore.Mvc;

namespace SH1ProjeUygulamasi.WebAPIUsing.Controllers
{
    public class APICRUDController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JsileAPIKullanimOrnegi()
        {
            return View();
        }

        public IActionResult Detail(int? id)
        {
            if (id is null)
            {
                return BadRequest("Geçersiz İstek!");
            }
            return View();
        }

        public IActionResult Brands()
        {
            return View();
        }

        public IActionResult FetchCrud()
        {
            return View();
        }

        public IActionResult JqueryCrud()
        {
            return View();
        }

        public IActionResult JsLogin()
        {
            return View();
        }
    }
}
