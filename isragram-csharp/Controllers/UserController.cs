using isragram_csharp.Dtos;
using isragram_csharp.Models;
using isragram_csharp.Repository;
using isragram_csharp.Repository.Impl;
using isragram_csharp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace isragram_csharp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBaseWithAuth
    {
        public readonly ILogger<UserController> _logger;
        public readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
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
                _logger.LogError("Houve um erro ao obter ao usuario. Erro: "+ ex.Message);
                List<string> errorsList = new List<string>
                {
                    ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto(
                    status: StatusCodes.Status500InternalServerError, description: "Houve um erro ao obter usuario", errors : errorsList));
            }
            
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult saveUser([FromBody] User userToSave)
        {
            try
            {
                var errorList = new List<string>();
                    
                if (userToSave != null )
                {               

                    if ( String.IsNullOrEmpty(userToSave.Username) || String.IsNullOrWhiteSpace(userToSave.Username)) 
                    {
                        errorList.Add("O nome de usuário digitado é inválido");
                    }
                    if (String.IsNullOrEmpty(userToSave.Email) || String.IsNullOrWhiteSpace(userToSave.Email) || !userToSave.Email.Contains("@"))
                    {
                        errorList.Add("Email digitado não é válido");
                    }
                    if (String.IsNullOrEmpty(userToSave.Password) || String.IsNullOrWhiteSpace(userToSave.Password))
                    {
                        errorList.Add("A senha digitada não é válida");
                    }
                    if (errorList.Count > 0)
                    {
                        return BadRequest(new ErrorResponseDto(
                            status: StatusCodes.Status400BadRequest,
                            description : "Houve erro ao criar novo usuário" ,
                            errors : errorList)
                        );
                    }
                    userToSave.Username = userToSave.Username.ToLower();
                    userToSave.Email = userToSave.Email.ToLower();
                    userToSave.Password = MD5Utils.GenerateMD5Hash(userToSave.Password);

                    if (_userRepository.UsernameExists(userToSave.Username))
                    {
                        return BadRequest(new ErrorResponseDto(
                                                        status: StatusCodes.Status400BadRequest,
                            description: "O nome de usuário informado já existe! Favor escolher outro nome"
                            )
);
                    }
                    if (_userRepository.EmailExists(userToSave.Email))
                    {
                        return BadRequest(new ErrorResponseDto(
                                                        status: StatusCodes.Status400BadRequest,
                            description: "O email informado já está cadastrado! "
                            )
);
                    }
                    _userRepository.Save(userToSave);
                }
            
            return Ok(userToSave);

            }
            catch (Exception ex)
            {
                _logger.LogError("Houve um erro ao obter ao salvar. Erro: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto(
                    status: StatusCodes.Status500InternalServerError, description: "Houve um erro ao obter usuario"));
            }

        }
    }
}


