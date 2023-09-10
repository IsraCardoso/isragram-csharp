using isragram_csharp.Models;

namespace isragram_csharp.Repository
{
    public interface IUserRepository
    {
        public void Save (User user);
        public bool UsernameExists (string username);
        public bool EmailExists(string email);
        public User Login(string email, string password);
        public User GetUserById(int id);
        public void UpdateUser(User user);
    }
}
