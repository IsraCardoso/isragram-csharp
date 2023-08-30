using isragram_csharp.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult LogIn([FromBody] LoginRequestDTO loginrequest)
        {
            try
            {
                throw new Exception("erro ao preencher dados");
            }
            catch (Exception ex) 
            {
                _logger.LogError("Ocorreu um erro ao fazer log in: " + ex.Message);
                ErrorResponseDto errorResponse = new ErrorResponseDto(StatusCodes.Status500InternalServerError, "Ocorreu um erro no login");
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse );
            }
        }


    }
}
