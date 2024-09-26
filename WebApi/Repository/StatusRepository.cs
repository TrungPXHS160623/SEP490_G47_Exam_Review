using Library.Common;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly QuizManagementContext dbContext;

        public StatusRepository(QuizManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<ResultResponse<ExamStatus>> GetStatus()
        {
            try
            {
                var data = await this.dbContext.ExamStatuses.ToListAsync();

                if (data == null || data.Count == 0)
                {
                    return new ResultResponse<ExamStatus>
                    {
                        IsSuccessful = false,
                        Message = "There is no status",
                    };
                } else
                {
                    return new ResultResponse<ExamStatus>
                    {
                        IsSuccessful = true,
                        Items = data,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultResponse<ExamStatus>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
