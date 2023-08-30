namespace isragram_csharp.Dtos
{
    public class LoginRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginRequestDTO (string email, string password)
        {
            (Email, Password) = (email, password);
        }
    }
}
