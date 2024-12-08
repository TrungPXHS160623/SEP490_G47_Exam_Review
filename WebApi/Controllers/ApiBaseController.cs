using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    [Authorize]
    public class ApiBaseController : ControllerBase
    {
        protected string JwtToken => HttpContext.Request.Headers["Authorization"].ToString();


    }
}