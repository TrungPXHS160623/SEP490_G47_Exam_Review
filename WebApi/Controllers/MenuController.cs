using Library.Common;
using Library.Models;
using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class MenuController : ApiBaseController
    {
        private readonly IMenuRepository _menuRepository;
        private readonly QuizManagementContext dbContext;

        public MenuController(IMenuRepository menuRepository,QuizManagementContext DbContext)
        {
            _menuRepository = menuRepository;
            dbContext = DbContext;
        }

        [HttpGet("GetMenu/{role}")]
        public async Task<IActionResult> UserLogin(int role)
        {
            var data = await this._menuRepository.GetMenu(role);

            return Ok(data);
        }

        [HttpGet("check-access")]
        public async Task<IActionResult> CheckMenuAccess(int userId, int menuId)
        {
            RequestResponse resp = new RequestResponse
            {
                IsSuccessful = false,
            };

            // Get the user and their role
            var user = await dbContext.Users
                .Include(u => u.Role) // Assuming navigation property
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return Ok(resp);
            }

            // Get the menu by link
            var menu = await dbContext.Menus
                .FirstOrDefaultAsync(m => m.MenuId == menuId);

            if (menu == null)
            {
                return Ok(resp);
            }

            // Check if the user's role has access to the menu
            var hasAccess = await dbContext.MenuRoles
                .AnyAsync(mr => mr.RoleId == user.RoleId && mr.MenuId == menu.MenuId);

            if (!hasAccess)
            {
                return Ok(resp);
            }

            return Ok(new RequestResponse
            {
                IsSuccessful = true,
            });
        }
    }
}
