using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.WebUI.Tools;

namespace SH1ProjeUygulamasi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            // return View(_context.Products.Include("Category")); // 1. yol string olarak include etmek
            return View(_context.Products.Include(p => p.Category)); // 2. yol lambda ile include etmek
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Products.Find(id));
        }

        void Load()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name"); // dropdown için kategorileri getir
            ViewBag.BrandId = new SelectList(_context.Brands, "Id", "Name");
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            Load();
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product collection, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                        collection.Image = FileHelper.FileLoader(Image);
                    _context.Products.Add(collection);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            Load();
            return View(collection);
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            Load();
            return View(_context.Products.Find(id));
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product collection, IFormFile? Image, bool resmiSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (resmiSil == true)
                    {
                        FileHelper.FileRemover(collection.Image);
                        collection.Image = string.Empty;
                    }
                    if (Image is not null)
                        collection.Image = FileHelper.FileLoader(Image);
                    _context.Products.Update(collection);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            Load();
            return View(collection);
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_context.Products.Find(id));
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(collection.Image))
                    FileHelper.FileRemover(collection.Image);
                _context.Products.Remove(collection);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
