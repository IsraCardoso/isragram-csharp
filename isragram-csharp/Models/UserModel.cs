namespace isragram_csharp.Models
{
    public class User
    {
       

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }

        public User() { }
        public User(int id, string username, string email, string password)
        {
            (Id, Username, Email, Password) = (id, username, email, password);

        }
        public User(int id, string username, string email)
        {
            (Id, Username, Email) = (id, username, email);
        }
        public User(string username, string email, string password)
        {
            (Username, Email, Password) = (username, email, password);
        }

    }
}
