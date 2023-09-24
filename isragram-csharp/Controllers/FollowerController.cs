using isragram_csharp.Dtos;
using isragram_csharp.Models;
using isragram_csharp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace isragram_csharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowerController : ControllerBaseWithAuth
    {
        private readonly ILogger<FollowerController> _logger;
        private readonly IFollowerRepository _followerRepository;
        public FollowerController(ILogger<FollowerController> logger, IFollowerRepository followerRepository, IUserRepository userRepository) : base(userRepository)
        {
            _logger = logger;
            _followerRepository = followerRepository;
        }

        [HttpPut]
        public IActionResult Follow(int followedUserId)
        {

            try
            {
                User activeUser = ReadToken();
                User followedUser = _userRepository.GetUserById(followedUserId);

                if (followedUser != null)
                {                    
                    Follower alreadyFollower = _followerRepository.GetFollower(followerUserId:activeUser.Id, followedUserId: followedUserId);
                    if (alreadyFollower != null) 
                    {
                        _followerRepository.Unfollow(alreadyFollower);
                        return Ok("Usuário desseguido com sucesso");
                    }
                    else
                    {
                    Follower newFollower = new Follower()
                    {
                        FollowerUserId = activeUser.Id,
                        FollowedUserId = followedUserId,
                    };
                        _followerRepository.Follow(newFollower);
                        return Ok("Usuário seguido com sucesso");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto(
                    status: StatusCodes.Status500InternalServerError, description: "Houve erro ao seguir/desseguir"));
                }

            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao seguir: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto(
                    status: StatusCodes.Status500InternalServerError, description: "Houve erro ao seguir/desseguir"));
            }

        }


    }
}
