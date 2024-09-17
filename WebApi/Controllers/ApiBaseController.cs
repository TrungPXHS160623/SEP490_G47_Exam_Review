using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    [Authorize]
    public class ApiBaseController : ControllerBase
    {
    }
}
