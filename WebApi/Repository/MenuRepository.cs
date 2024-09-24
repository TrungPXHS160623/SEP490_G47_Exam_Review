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

        public async Task<ResultResponse<Menu>> GetMenu(int userId)
        {
            try
            {
                var data = await this.dbContext.Menus.ToListAsync();

                return new ResultResponse<Menu>
                {
                    IsSuccessful = true,
                    Items = data,
                };
            } catch (Exception ex)
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
