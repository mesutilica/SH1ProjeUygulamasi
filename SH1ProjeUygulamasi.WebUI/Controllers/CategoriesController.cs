using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Data;

namespace SH1ProjeUygulamasi.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var model = _context.Categories.Where(p => p.IsActive && p.Id == id).FirstOrDefault();
            return View(model);
        }
    }
}
