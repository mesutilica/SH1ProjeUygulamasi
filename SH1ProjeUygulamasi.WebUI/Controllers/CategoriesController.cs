using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Service.Abstract;

namespace SH1ProjeUygulamasi.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        //private readonly DatabaseContext _context; // uygulamada direk dbcontext kullanılmamalı, servisler üzerinden db işlemleri yapılmalı.

        //public CategoriesController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index(int id)
        {
            //var model = _context.Categories.Where(p => p.IsActive && p.Id == id).Include(c => c.Products).FirstOrDefault();
            //return View(model);
            var model = _categoryService.GetCategoryByProducts(id);
            return View(model);
        }
    }
}
