using SH1ProjeUygulamasi.Core.Entities;
using System.Linq.Expressions;

namespace SH1ProjeUygulamasi.Service.Abstract
{
    public interface IService<T> where T : class, IEntity, new()
    {
        // IService interface i dışarıdan T olarak bir parametre alarak çalışacak, where T : class, IEntity, new() kodu buraya gelecek T tipinin şartlarını belirler, class olmalı Ientity interfaceini kullanmalı ve new lenebilir bir yapı olmalı (string olmamalı)
        // Senkron metot imzaları
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        T Get(Expression<Func<T, bool>> expression);
        T Find(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int SaveChanges();

        //Asenkron metotlar
        Task<T> FindAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
