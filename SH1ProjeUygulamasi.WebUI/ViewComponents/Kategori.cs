using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Data;

namespace SH1ProjeUygulamasi.WebUI.ViewComponents
{
    public class Kategori : ViewComponent
    {
        private readonly DatabaseContext _context;

        public Kategori(DatabaseContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.Categories.Where(c => c.IsActive));
        }
    }
}
