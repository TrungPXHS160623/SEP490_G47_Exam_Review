using Microsoft.EntityFrameworkCore;
using PRN231_API.IRepository;
using PRN231_Library.Common;
using PRN231_Library.Models;

namespace PRN231_API.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly Prn231FinalProjectContext DBcontext;

        public MenuRepository(Prn231FinalProjectContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }

        public async Task<ResultResponse<Menu>> GetMenuByRole(int roleId)
        {
            try
            {
                ResultResponse<Menu> response;

                var menuList = (from m in this.DBcontext.Menus
                                join mr in this.DBcontext.MenuRoles on m.MenuId equals mr.MenuId
                                where mr.RoleId == roleId
                                select m).ToList();

                response = new ResultResponse<Menu>
                {
                    IsSuccessful = true,
                    Items = menuList,
                };

                return response;
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
