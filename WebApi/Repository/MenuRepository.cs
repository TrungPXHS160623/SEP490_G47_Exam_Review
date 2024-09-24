using Library.Common;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly QuizManagementContext dbContext;

        public MenuRepository(QuizManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultResponse<Menu>> GetMenu(int roleId)
        {
            try
            {
                var data = (from m in this.dbContext.Menus
                            join mr in this.dbContext.MenuRoles on m.MenuId equals mr.MenuId
                            join r in this.dbContext.UserRoles on mr.RoleId equals r.RoleId
                            where r.RoleId == roleId
                            select new Menu
                            {
                                MenuId = m.MenuId,
                                MenuLink = m.MenuLink,
                                MenuName = m.MenuName,
                            }).ToList();

                return new ResultResponse<Menu>
                {
                    IsSuccessful = true,
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<Menu>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
    }
}
