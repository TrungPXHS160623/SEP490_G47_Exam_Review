using Library.Common;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly QuizManagementContext DBcontext;
        private readonly ILogHistoryRepository logRepository;

        public FacultyRepository(QuizManagementContext DBcontext, ILogHistoryRepository logRepository)
        {
            this.DBcontext = DBcontext;
            this.logRepository = logRepository;
        }

        public async Task<ResultResponse<Faculty>> GetFaculties()
        {
            try
            {
                var data = await this.DBcontext.Faculties.ToListAsync();

                if (data != null)
                {
                    return new ResultResponse<Faculty>
                    {
                        IsSuccessful = true,
                        Items = data,
                    };
                }
                else
                {
                    return new ResultResponse<Faculty>
                    {
                        IsSuccessful = false,
                        Message = "There is no faculty",
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultResponse<Faculty>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<Faculty>> GetHeadFaculties(int userId)
        {
            try
            {
                var data = await (from fa in this.DBcontext.Faculties
                                  join cuf in this.DBcontext.CampusUserFaculties on fa.FacultyId equals cuf.FacultyId
                                  where cuf.UserId == userId
                                  select fa).ToListAsync();

                return new ResultResponse<Faculty>
                {
                    IsSuccessful = true,
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<Faculty>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
