using SH1ProjeUygulamasi.Core.Entities;
using System.Linq.Expressions;

namespace SH1ProjeUygulamasi.Service.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        List<Category> GetCategories(Expression<Func<Category, bool>> expression);
        Category GetCategory(int id);
        Category GetCategoryByProducts(int id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void RemoveCategory(Category category);
        int Save();
    }
}
