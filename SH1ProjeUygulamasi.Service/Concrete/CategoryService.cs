using Microsoft.EntityFrameworkCore;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.Service.Abstract;
using System.Linq.Expressions;

namespace SH1ProjeUygulamasi.Service.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _context;

        public CategoryService(DatabaseContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public List<Category> GetCategories(Expression<Func<Category, bool>> expression)
        {
            return _context.Categories.Where(expression).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Find(id);
        }

        public Category GetCategoryByProducts(int id)
        {
            return _context.Categories.Where(c => c.IsActive && c.Id == id).Include(c => c.Products).FirstOrDefault();
        }

        public void RemoveCategory(Category category)
        {
            _context.Categories.Remove(category);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
