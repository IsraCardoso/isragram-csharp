namespace isragram_csharp.Dtos
{
    public class LoginResponseDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public LoginResponseDto(string username, string email, string token) {
            (Username, Email, Token) = (username, email, token);
        }
    }
}
