using isragram_csharp.Dtos;
using isragram_csharp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace isragram_csharp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBaseWithAuth
    {
        public readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult getUser()
        {
            try
            {
                User user = new User(id: 20, email: "teste@teste.com", username: "teste");
                return Ok(user);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Houve um erro ao obter o usuario. Erro: "+ ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto(
                    status: StatusCodes.Status500InternalServerError, description: "Houve um erro ao obter usuario. Erro: "+ ex.Message));
            }
            
        }
    }
}
