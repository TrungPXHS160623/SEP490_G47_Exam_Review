using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.Data.SqlClient;
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
                        Message = "Department name cannot be empty!"
                    };
                }

                // Kiểm tra xem học kỳ đã tồn tại hay chưa
                var existingSemester = await DBcontext.Faculties
                    .FirstOrDefaultAsync(s => s.FacultyName == request.FacultyName);
                if (existingSemester != null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Department with this name already exists!"
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
                    Message = "Department created successfully!"
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
                        Message = "There is no Department",
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
        public async Task<ResultResponse<FacutyRequest>> GetFacutiesByUserID(int? userId)
        {
            try
            {
                var data = (from s in this.DBcontext.Faculties
                            join cus in this.DBcontext.CampusUserFaculties on s.FacultyId equals cus.FacultyId into subjectJoin
                            from cus in subjectJoin.DefaultIfEmpty()
                            where (cus.UserId ==  userId)
                            select new FacutyRequest
                            {
                                FacultyId = s.FacultyId,
                                FacultyName = s.FacultyName,
                            }).Distinct().ToList();

                return new ResultResponse<FacutyRequest>
                {
                    IsSuccessful = true,
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<FacutyRequest>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<FacutyRequest>> GetFacutyByIdAsync(int FacutyID)
        {
            try
            {
                var facuty = await DBcontext.Faculties.FirstOrDefaultAsync(x => x.FacultyId == FacutyID);

                if (facuty == null)
                {
                    return new ResultResponse<FacutyRequest>
                    {
                        IsSuccessful = false,
                        Message = "Department not found."
                    };
                }

                var facutyResponse = new FacutyRequest
                {
                    FacultyId = facuty.FacultyId,
                    FacultyName= facuty.FacultyName,
                    Description = facuty.Description,
                };

                return new ResultResponse<FacutyRequest>
                {
                    IsSuccessful = true,
                    Item = facutyResponse
                };
            }
            catch (Exception ex)
            {

                return new ResultResponse<FacutyRequest>
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


        public async Task<RequestResponse> UpdateFacutyAsync(FacutyRequest request)
        {
            try
            {
                var data = await this.DBcontext.Faculties.FirstOrDefaultAsync(x => x.FacultyId == request.FacultyId);

                if (data != null)
                {
                    if (data.FacultyName == request.FacultyName)
                    {
                        return new RequestResponse
                        {
                            IsSuccessful = false,
                            Message = "Department already exists.",
                        };
                    }
                    data.FacultyName = request.FacultyName;
                    data.Description = request.Description;
                    await this.DBcontext.SaveChangesAsync();

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Department updated successfully",
                    };
                }
                else
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Department already exist",
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

        public async Task<RequestResponse> DeleteFaculties(int facultyID)
        {
            try
            {
                var data = await this.DBcontext.Faculties.FirstOrDefaultAsync(x => x.FacultyId == facultyID);

                if (data == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Campus not found",
                    };
                }
                else
                {
                    this.DBcontext.Faculties.Remove(data);

                    await this.DBcontext.SaveChangesAsync();

                    await logRepository.LogAsync($"Delete Deparment {data.FacultyName}");

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Delete Successfully",
                    };
                }

            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Message.Contains("REFERENCE constraint"))
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Cannot delete because there is some data connect to this"
                    };
                }
                else
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = ex.Message,
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
