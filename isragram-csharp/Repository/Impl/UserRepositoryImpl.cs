using DevagramCSharp.Models;
using isragram_csharp.Models;

namespace isragram_csharp.Repository.Impl
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly IsragramContext _context;

        public UserRepositoryImpl(IsragramContext context)
        {
            _context = context;
        }
        public void Save(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
        public bool UsernameExists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public User Login(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User GetUserById (int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
