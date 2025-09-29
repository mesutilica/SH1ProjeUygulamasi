using SH1ProjeUygulamasi.Core.Entities;
using System.Linq.Expressions;

namespace SH1ProjeUygulamasi.Service.Abstract
{
    public interface IProductService
    {
        List<Product> GetProducts();
        List<Product> GetProducts(Expression<Func<Product, bool>> expression);
        Product GetProduct(int id);
        Product GetProductByCategory(int id);
        Product GetProductByCategoryAndProductImages(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void RemoveProduct(Product product);
        int Save();
    }
}
