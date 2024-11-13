using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
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

        public async Task<RequestResponse> CreateFacutyAsync(FacutyRequest request)
        {
            try
            {
                // Kiểm tra xem các trường yêu cầu có hợp lệ không
                if (string.IsNullOrEmpty(request.FacultyName))
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Semester name cannot be empty!"
                    };
                }

                // Kiểm tra xem học kỳ đã tồn tại hay chưa
                var existingSemester = await DBcontext.Semesters
                    .FirstOrDefaultAsync(s => s.SemesterName == request.FacultyName);
                if (existingSemester != null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Semester with this name already exists!"
                    };
                }
                // Tạo đối tượng học kỳ mới
                var newFacuty = new Faculty
                {
                    FacultyName = request.FacultyName,
                    Description = request.Description,
                    CreateDate = DateTime.Now,
                };

                // Lưu học kỳ mới vào cơ sở dữ liệu
                await DBcontext.Faculties.AddAsync(newFacuty);
                await DBcontext.SaveChangesAsync();

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Semester created successfully!"
                };
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

        public async Task<ResultResponse<FacutyResponse>> GetFacutyByIdAsync(int FacutyID)
        {
            try
            {
                var facuty = await DBcontext.Faculties.FirstOrDefaultAsync(x => x.FacultyId == FacutyID);

                if (facuty == null)
                {
                    return new ResultResponse<FacutyResponse>
                    {
                        IsSuccessful = false,
                        Message = "Semester not found."
                    };
                }

                var facutyResponse = new FacutyResponse
                {
                    FacultyId = facuty.FacultyId,
                    FacultyName= facuty.FacultyName,
                    Description = facuty.Description,
                };

                return new ResultResponse<FacutyResponse>
                {
                    IsSuccessful = true,
                    Item = facutyResponse
                };
            }
            catch (Exception ex)
            {

                return new ResultResponse<FacutyResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultResponse<FacutyResponse>> GetFacutyByRole(int roleId, int userId, int campusId)
        {
            try
            {
                var data = (from s in this.DBcontext.Faculties
                            join cuf in this.DBcontext.CampusUserFaculties on s.FacultyId equals cuf.FacultyId into facutyJoin
                            from cuf in facutyJoin.DefaultIfEmpty()
                            where
                            (roleId == 4 && (
                                (cuf != null && cuf.UserId == userId  && cuf.CampusId == campusId)
                                || (cuf == null || !this.DBcontext.CampusUserFaculties
                                    .Any(other => other.FacultyId == s.FacultyId  && other.CampusId == campusId))
                            ))
                            || roleId == 3
                            select new FacutyResponse
                            {
                                FacultyId = s.FacultyId,
                                FacultyName = s.FacultyName,
                                Description = s.Description
                            }).Distinct().ToList();

                return new ResultResponse<FacutyResponse>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<FacutyResponse>
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

        public Task<RequestResponse> UpdateFacutyAsync(int facutyID, FacutyRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
