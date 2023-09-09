namespace isragram_csharp.Dtos
{
    public class UserRequestDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
