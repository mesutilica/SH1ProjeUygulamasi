using Microsoft.EntityFrameworkCore;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.Service.Abstract;
using System.Linq.Expressions;

namespace SH1ProjeUygulamasi.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUser(Expression<Func<User, bool>> expression)
        {
            return _context.Users.FirstOrDefault(expression);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public List<User> GetUsers(Expression<Func<User, bool>> expression)
        {
            return _context.Users.Where(expression).ToList();
        }

        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
    }
}
