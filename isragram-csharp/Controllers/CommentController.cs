using isragram_csharp.Dtos;
using isragram_csharp.Models;
using isragram_csharp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace isragram_csharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBaseWithAuth
    {
        private readonly ILogger<PostController> _logger;
        private readonly ICommentRepository _commentRepository;
        public CommentController(ILogger<PostController> logger, ICommentRepository commentRepository, IUserRepository userRepository) : base(userRepository)
        {
            _logger = logger;
            _commentRepository = commentRepository;
        }

        [HttpPut]
        public IActionResult Comment([FromBody] CommentRequestDto comment)
        {
            try
            {
                if (comment != null)
                {
                    if (String.IsNullOrEmpty(comment.Text) || String.IsNullOrWhiteSpace(comment.Text))
                    {
                        return BadRequest("Por favor digite seu comentário");
                    }
                    Comment newComment = new Comment
                    {
                        Text = comment.Text,
                        PostId = comment.PostId,
                        UserId = ReadToken().Id
                    };
                    _commentRepository.Comment(newComment);
                    return Ok("O comentário foi salvo com sucesso!");
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao comentar: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto(
                    status: StatusCodes.Status500InternalServerError, description: "Houve erro ao comentar"));
            }
        }
    }
}
