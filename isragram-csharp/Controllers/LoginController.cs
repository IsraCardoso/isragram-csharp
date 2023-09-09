using DevagramCSharp.Services;
using isragram_csharp.Dtos;
using isragram_csharp.Models;
using isragram_csharp.Repository;
using isragram_csharp.Utils;
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
        private readonly IUserRepository _userRepository;
        public LoginController(ILogger<LoginController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
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
                    User userResponse = _userRepository.Login(loginRequest.Email.ToLower(), MD5Utils.GenerateMD5Hash(loginRequest.Password) );
                    if (userResponse != null)
                    {
                        return Ok(new LoginResponseDto(
                            username: userResponse.Username,
                            email: userResponse.Email,
                            token: TokenService.CreateToken(userResponse)));
                    }
                    else
                    {
                        ErrorResponseDto errorResponse = new ErrorResponseDto(StatusCodes.Status400BadRequest, "Email ou senha inválido.");
                        return BadRequest(errorResponse);
                    }
                }
                else
                {
                    ErrorResponseDto errorResponse = new ErrorResponseDto(StatusCodes.Status400BadRequest, "Preencha os campos de email e senha corretamente.");
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
