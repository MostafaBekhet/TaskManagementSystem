using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/reminders")]
    [Authorize]
    public class RemindersController : ControllerBase
    {
    }
}
