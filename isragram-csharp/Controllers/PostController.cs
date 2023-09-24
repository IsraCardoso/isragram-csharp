using isragram_csharp.Dtos;
using isragram_csharp.Models;
using isragram_csharp.Repository;
using isragram_csharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace isragram_csharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBaseWithAuth
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _postRepository;
        public PostController(ILogger<PostController> logger, IPostRepository postRepository, IUserRepository userRepository) : base(userRepository)
        {
            _logger = logger;
            _postRepository = postRepository;
        }

        [HttpPost]
        public IActionResult Post([FromForm] PostRequestDto postRequest)
        {
            try
            {
                if (postRequest != null ) 
                {
                    if(postRequest.Image == null)
                    {
                        _logger.LogError("Usuário não enviou imagem. ");
                        return BadRequest("É obrigatória a descrição da publicação");
                    }
                    if(String.IsNullOrEmpty(postRequest.Description) )
                    {
                        _logger.LogError("Usuário não enviou descrição. ");
                        return BadRequest("É obrigatória a descrição da publicação");
                    }

                    User activeUser = ReadToken();
                    CosmicService cosmicService = new ();
                    Post newPost = new ()
                    {
                        Description = postRequest.Description,
                        Image = cosmicService.SendImage(new ImageDto { Image = postRequest.Image, Name="post"}),
                        UserId = activeUser.Id
                    };
                    _postRepository.Post(newPost);
                    return Ok("Publicação salva com sucesso!"); 
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro na publicacao: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto(
                    status: StatusCodes.Status500InternalServerError, description: "Houve erro ao postar. Seu post não foi publicado."));
            }
        }

       
    }
}
