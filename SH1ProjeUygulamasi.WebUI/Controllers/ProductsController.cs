using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.Service.Abstract;
using SH1ProjeUygulamasi.Service.Concrete;

namespace SH1ProjeUygulamasi.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        //private readonly DatabaseContext _context;

        //public ProductsController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index(string q = "")
        {
            // var model = _context.Products.Where(p => p.IsActive && p.Name.Contains(q)).ToList();
            var model = _productService.GetProducts(p => p.IsActive && p.Name.Contains(q));
            return View(model);
        }
        public IActionResult Detail(int id)
        {
            var model = _productService.GetProductByCategory(id); // _context.Products.FirstOrDefault(p => p.IsActive && p.Id == id);
            return View(model);
        }
    }
}
