using DAL.Context;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    /// <summary>Репозиторий пользователей</summary>
    public class UsersRepository
    {
        private readonly QuizDB _db;
        private readonly DbSet<User> _set;

        public UsersRepository(QuizDB db)
        {
            _db = db;
            _set = db.Set<User>();
        }
        public IQueryable<User> Users => _set;

        public User Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }
    }
}
