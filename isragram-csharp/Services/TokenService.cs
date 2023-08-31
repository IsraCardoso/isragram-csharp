using isragram_csharp;
using isragram_csharp.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevagramCSharp.Services
{
    public class TokenService
    {
        public static string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var cryptogaphyKey = Encoding.ASCII.GetBytes(JWTKey.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(cryptogaphyKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}