using isragram_csharp.Dtos;
using isragram_csharp.Models;
using isragram_csharp.Repository;
using isragram_csharp.Repository.Impl;
using isragram_csharp.Services;
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

        public UserController(ILogger<UserController> logger, IUserRepository userRepository):base(userRepository)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult getUser()
        {
            try
            {
                User currentUser = ReadToken();

                return Ok(new UserResponseDto { Email= currentUser.Email, Username= currentUser.Username});
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
        public IActionResult saveUser([FromForm] UserRequestDto userReq)
        {
            try
            {
                var errorList = new List<string>();
                    
                if (userReq != null )
                {               

                    if ( String.IsNullOrEmpty(userReq.Username) || String.IsNullOrWhiteSpace(userReq.Username)) 
                    {
                        errorList.Add("O nome de usuário digitado é inválido");
                    }
                    if (String.IsNullOrEmpty(userReq.Email) || String.IsNullOrWhiteSpace(userReq.Email) || !userReq.Email.Contains("@"))
                    {
                        errorList.Add("Email digitado não é válido");
                    }
                    if (String.IsNullOrEmpty(userReq.Password) || String.IsNullOrWhiteSpace(userReq.Password))
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

                    userReq.Username = userReq.Username.ToLower();
                    userReq.Email = userReq.Email.ToLower();

                    if (_userRepository.UsernameExists(userReq.Username))
                    {
                        return BadRequest(new ErrorResponseDto(
                                                        status: StatusCodes.Status400BadRequest,
                            description: "O nome de usuário informado já existe! Favor escolher outro nome"
                            )
                        );
                    }

                    if (_userRepository.EmailExists(userReq.Email))
                    {
                        return BadRequest(new ErrorResponseDto(
                                                        status: StatusCodes.Status400BadRequest,
                            description: "O email informado já está cadastrado! "
                            )
                        );
                    }


                    userReq.Password = MD5Utils.GenerateMD5Hash(userReq.Password);

                    CosmicService cosmicService = new CosmicService();

                    User userToSave = new User {
                    Username = userReq.Username,
                    Email= userReq.Email,
                    Password= userReq.Password,
                    Avatar = cosmicService.SendImage(new ImageDto { Image= userReq.Avatar, Name= userReq.Username.Replace(" ","") })
                    };

                    _userRepository.Save(userToSave);
                }

            return Ok("Usuário criado com sucesso!");

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


