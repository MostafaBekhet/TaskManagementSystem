using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/teams")]
    [Authorize]
    public class TeamsController : ControllerBase
    {
    }
}
