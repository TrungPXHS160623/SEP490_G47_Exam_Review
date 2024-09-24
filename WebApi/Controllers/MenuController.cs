using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class MenuController : ApiBaseController
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        [HttpGet("GetMenu/{userId}")]
        public async Task<IActionResult> UserLogin(int userId)
        {
            var data = await this._menuRepository.GetMenu(userId);

            return Ok(data);
        }
    }
}
