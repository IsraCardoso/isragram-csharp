﻿namespace isragram_csharp.Dtos
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginRequestDto(string email, string password)
        {
            (Email, Password) = (email, password);
        }
    }
}