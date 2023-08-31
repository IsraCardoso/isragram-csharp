using DevagramCSharp.Services;
using isragram_csharp.Dtos;
using isragram_csharp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace isragram_csharp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController :ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult LogIn([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                if (!String.IsNullOrEmpty(loginRequest.Email) && !String.IsNullOrEmpty(loginRequest.Password) && 
                    !String.IsNullOrWhiteSpace(loginRequest.Email) && !String.IsNullOrWhiteSpace(loginRequest.Password))
                {
                    string email = "teste";
                    string password = "1234";

                    if (loginRequest.Email == email && loginRequest.Password == password) {
                        User userResponse = new User(id:15, username:"usernameteste", email:email) ;

                        return Ok(new LoginResponseDto(
                            username: userResponse.Username, 
                            email: userResponse.Email, 
                            token: TokenService.CreateToken(userResponse)) );
                    }
                    else { return BadRequest(new ErrorResponseDto(status: 500, description: "email ou senha não batem")); }
                }else
                {
                    ErrorResponseDto errorResponse = new ErrorResponseDto(StatusCodes.Status400BadRequest, "Favor preencher corretamente os campos de email e senha.");
                    return BadRequest(errorResponse);
                }



            }
            catch (Exception ex) 
            {
                _logger.LogError("Ocorreu um erro ao fazer log in: " + ex.Message);
                ErrorResponseDto errorResponse = new ErrorResponseDto(StatusCodes.Status500InternalServerError, "Ocorreu um erro no login.");
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse );
            }
        }


    }
}
