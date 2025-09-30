using SH1ProjeUygulamasi.Core.Entities;
using System.Linq.Expressions;

namespace SH1ProjeUygulamasi.Service.Abstract
{
    public interface IUserService
    {
        List<User> GetUsers();
        List<User> GetUsers(Expression<Func<User, bool>> expression);
        User GetUser(int id);
        User GetUser(Expression<Func<User, bool>> expression);
        void AddUser(User user);
        void UpdateUser(User user);
        void RemoveUser(User user);
        int Save();
    }
}
