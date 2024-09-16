using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PRN231_API.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    [Authorize]
    public class ApiBaseController : ControllerBase
    {
    }
}
