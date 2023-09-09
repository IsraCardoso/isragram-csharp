using isragram_csharp.Models;
using isragram_csharp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace isragram_csharp.Controllers
{
    [Authorize]
    public class ControllerBaseWithAuth : ControllerBase
    {
        protected readonly IUserRepository _userRepository;

        public ControllerBaseWithAuth( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected User ReadToken()
        {
            string currentUserId = User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(currentUserId))
            {
                return null;
            }
            else
            {
                return _userRepository.GetUserById(int.Parse(currentUserId));
            }
        }

    }
}
