
using Library.Common;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly QuizManagementContext DBcontext;

        public RoleRepository(QuizManagementContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }

        public async Task<ResultResponse<UserRole>> GetRoles()
        {
            try
            {
                var data = await this.DBcontext.UserRoles.ToListAsync();

                if (data != null)
                {
                    return new ResultResponse<UserRole>
                    {
                        IsSuccessful = true,
                        Items = data,
                    };
                }
                else
                {
                    return new ResultResponse<UserRole>
                    {
                        IsSuccessful = false,
                        Message = "There is no role",
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserRole>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }

        }
    }
}
