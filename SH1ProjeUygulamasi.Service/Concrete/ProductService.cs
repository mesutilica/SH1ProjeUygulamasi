using Microsoft.EntityFrameworkCore;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.Service.Abstract;
using System.Linq.Expressions;

namespace SH1ProjeUygulamasi.Service.Concrete
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _context;

        public ProductService(DatabaseContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public List<Product> GetProducts(Expression<Func<Product, bool>> expression)
        {
            return _context.Products.Where(expression).ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public Product GetProductByCategory(int id)
        {
            return _context.Products.Where(c => c.IsActive && c.Id == id).Include(c => c.Category).FirstOrDefault();
        }

        public Product GetProductByCategoryAndProductImages(int id)
        {
            return _context.Products.Where(c => c.IsActive && c.Id == id).Include(c => c.Category).Include(c => c.ProductImages).FirstOrDefault(); // burada ürüne ürün resimleri de dahil edilecek!
        }

        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
