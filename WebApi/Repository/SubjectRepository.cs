using Library.Common;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly QuizManagementContext DBcontext;

        public SubjectRepository(QuizManagementContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }

        public async Task<ResultResponse<Subject>> GetSubjects()
        {
            try
            {
                var data = await this.DBcontext.Subjects.ToListAsync();

                return new ResultResponse<Subject>
                {
                    IsSuccessful = data != null ? true : false,
                    Message = data != null ? string.Empty : "Cannot found subject",
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<Subject>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
