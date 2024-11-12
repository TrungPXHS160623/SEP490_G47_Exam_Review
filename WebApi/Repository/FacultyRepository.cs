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

        public async Task<ResultResponse<Faculty>> GetFacutiesByUserID(int? userId)
        {
            try
            {
                var data = (from s in this.DBcontext.Faculties
                            join cus in this.DBcontext.CampusUserFaculties on s.FacultyId equals cus.FacultyId into subjectJoin
                            from cus in subjectJoin.DefaultIfEmpty()
                            where (cus.UserId ==  userId)
                            select new Faculty
                            {
                                FacultyId = s.FacultyId,
                                FacultyName = s.FacultyName,
                            }).Distinct().ToList();

                return new ResultResponse<Faculty>
                {
                    IsSuccessful = true,
                    Items = data
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

        public async Task<RequestResponse> UpdateFaculties(Faculty req)
        {
            try
            {
                var data = await this.DBcontext.Faculties.FirstOrDefaultAsync(x => x.FacultyId == req.FacultyId);

                if (data != null)
                {
                    if (data.FacultyName == req.FacultyName)
                    {
                        return new RequestResponse
                        {
                            IsSuccessful = false,
                            Message = "Facuty already exists.",
                        };
                    }
                    data.FacultyName = req.FacultyName;
                    data.Description = req.Description;
                    await this.DBcontext.SaveChangesAsync();

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Facuty updated successfully",
                    };
                }
                else
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Subject Code already exist",
                    };
                }
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
