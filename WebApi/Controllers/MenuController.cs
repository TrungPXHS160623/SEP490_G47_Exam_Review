using Microsoft.AspNetCore.Mvc;
using PRN231_API.IRepository;

namespace PRN231_API.Controllers
{
    public class MenuController : ApiBaseController
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        [HttpGet("GetMenuByRole/{roleId}")]
        public async Task<IActionResult> GetMenuByRole(int roleId)
        {
            var data = await this._menuRepository.GetMenuByRole(roleId);
            return Ok(data);
        }
    }
}
