using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace isragram_csharp.Controllers
{
    [Authorize]
    public class ControllerBaseWithAuth : ControllerBase
    {
 
    }
}
