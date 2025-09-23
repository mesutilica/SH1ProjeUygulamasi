using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Data;

namespace SH1ProjeUygulamasi.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index(string q = "")
        {
            var model = _context.Products.Where(p => p.IsActive && p.Name.Contains(q)).ToList();
            return View(model);
        }
        public IActionResult Detail(int id)
        {
            var model = _context.Products.FirstOrDefault(p => p.IsActive && p.Id == id);
            return View(model);
        }
    }
}
