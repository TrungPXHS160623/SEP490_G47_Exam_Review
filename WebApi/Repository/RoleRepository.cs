
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

        public async Task<ResultResponse<UserRole>> GetRolesForAdmin()
        {
            try
            {
                var data = await this.DBcontext.UserRoles.Where(x => x.RoleId == 1 || x.RoleId == 2).ToListAsync();

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

        public async Task<ResultResponse<UserRole>> GetRolesForExaminer()
        {
            try
            {
                var data = await this.DBcontext.UserRoles.Where(x => x.RoleId != 1 || x.RoleId != 2).ToListAsync();

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
