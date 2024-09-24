using Library.Common;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class CampusRepository : ICampusRepository
    {
        private readonly QuizManagementContext DBcontext;

        public CampusRepository(QuizManagementContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }

        public async Task<ResultResponse<Campus>> GetCampus()
        {
            try
            {
                var data = await this.DBcontext.Campuses.ToListAsync();

                if (data != null)
                {
                    return new ResultResponse<Campus>
                    {
                        IsSuccessful = true,
                        Items = data,
                    };
                } else
                {
                    return new ResultResponse<Campus>
                    {
                        IsSuccessful = false,
                        Message = "There is no campus",
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResultResponse<Campus>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
