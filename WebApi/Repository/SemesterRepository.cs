using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly QuizManagementContext dbContext;
        private readonly ILogHistoryRepository logRepository;
        public SemesterRepository(QuizManagementContext dbContext, ILogHistoryRepository logRepository)
        {
            this.dbContext = dbContext;
            this.logRepository = logRepository;
        }
        public async Task<RequestResponse> CreateSemesterAsync(SemesterRequest request)
        {
            try
            {
                // Kiểm tra xem các trường yêu cầu có hợp lệ không
                if (string.IsNullOrEmpty(request.SemesterName))
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Semester name cannot be empty!"
                    };
                }

                // Kiểm tra xem học kỳ đã tồn tại hay chưa
                var existingSemester = await dbContext.Semesters
                    .FirstOrDefaultAsync(s => s.SemesterName == request.SemesterName);
                if (existingSemester != null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Semester with this name already exists!"
                    };
                }
                // Kiểm tra xem EndDate có lớn hơn StartDate không
                if (request.EndDate <= request.StartDate)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "End date must be greater than start date!"
                    };
                }
                // Lấy học kỳ gần nhất (theo thời gian kết thúc)
                var lastSemester = await dbContext.Semesters
                    .OrderByDescending(s => s.EndDate)
                    .FirstOrDefaultAsync();

                // Kiểm tra xem thời gian bắt đầu của học kỳ mới có nhỏ hơn thời gian kết thúc của học kỳ gần nhất không
                if (lastSemester != null && request.StartDate < lastSemester.EndDate)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Start date of the new semester cannot be earlier than the end date of the last semester!"
                    };
                }
                // Tạo đối tượng học kỳ mới
                var newSemester = new Semester
                {
                    SemesterName = request.SemesterName,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    IsActive = request.IsActive,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };

                // Lưu học kỳ mới vào cơ sở dữ liệu
                await dbContext.Semesters.AddAsync(newSemester);
                await dbContext.SaveChangesAsync();

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

        public async Task<RequestResponse> DeleteSemesterAsync(int semesterId)
        {
            try
            {
                var data = await this.dbContext.Semesters.FirstOrDefaultAsync(x => x.SemesterId == semesterId);

                if (data != null)
                {
                    var relatedExams = await dbContext.Exams.AnyAsync(e => e.SemesterId == semesterId);
                    if (relatedExams)
                    {
                        throw new InvalidOperationException("Cannot delete semester because it is related to existing exams.");
                    }
                    this.dbContext.Semesters.Remove(data);

                    await this.dbContext.SaveChangesAsync();

                    await logRepository.LogAsync($"Delete semester [{data.SemesterName}] ");

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Delete Semester Successfully",
                    };
                }
                else
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Semester not exist",
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

        public async Task<ResultResponse<SemesterRequest>> GetActiveSemestersAsync()
        {
            try
            {
                // Lấy danh sách tất cả các học kỳ từ cơ sở dữ liệu
                var listSemester = await dbContext.Semesters.Where(x => x.IsActive == true).ToListAsync();

                // Chuyển đổi danh sách Semester thành SemesterResponse
                var semesterResponses = listSemester.Select(semester => new SemesterRequest
                {
                    SemesterID = semester.SemesterId,
                    SemesterName = semester.SemesterName,
                    StartDate = semester.StartDate,
                    EndDate = semester.EndDate,
                    IsActive = semester.IsActive,
                    CreatedDate = semester.CreatedDate,
                    UpdatedDate = semester.UpdatedDate
                }).ToList();

                return new ResultResponse<SemesterRequest>
                {
                    IsSuccessful = true,
                    Items = semesterResponses
                };

            }
            catch (Exception ex)
            {

                return new ResultResponse<SemesterRequest>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultResponse<SemesterRequest>> GetSemesterByIdAsync(int semesterId)
        {
            try
            {
                // Lấy học kỳ theo ID từ cơ sở dữ liệu
                var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.SemesterId == semesterId);

                // Kiểm tra nếu không tìm thấy học kỳ
                if (semester == null)
                {
                    return new ResultResponse<SemesterRequest>
                    {
                        IsSuccessful = false,
                        Message = "Semester not found."
                    };
                }

                // Chuyển đổi đối tượng Semester thành SemesterResponse
                var semesterResponse = new SemesterRequest
                {
                    SemesterID = semester.SemesterId,
                    SemesterName = semester.SemesterName,
                    StartDate = semester.StartDate,
                    EndDate = semester.EndDate,
                    IsActive = semester.IsActive,
                    CreatedDate = semester.CreatedDate,
                    UpdatedDate = semester.UpdatedDate
                };

                return new ResultResponse<SemesterRequest>
                {
                    IsSuccessful = true,
                    Item = semesterResponse
                };
            }
            catch (Exception ex)
            {

                return new ResultResponse<SemesterRequest>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultResponse<Semester>> GetSemestersAsync()
        {
            try
            {
                // Lấy danh sách tất cả các học kỳ từ cơ sở dữ liệu
                var listSemester = await dbContext.Semesters.ToListAsync();

                // Chuyển đổi danh sách Semester thành SemesterResponse
                var semesterResponses = listSemester.Select(semester => new Semester
                {
                    SemesterId = semester.SemesterId,
                    SemesterName = semester.SemesterName,
                    StartDate = semester.StartDate,
                    EndDate = semester.EndDate,
                    IsActive = semester.IsActive,
                    CreatedDate = semester.CreatedDate,
                    UpdatedDate = semester.UpdatedDate
                }).ToList();

                return new ResultResponse<Semester>
                {
                    IsSuccessful = true,
                    Items = semesterResponses
                };

            }
            catch (Exception ex)
            {

                return new ResultResponse<Semester>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<bool> ToggleSemesterStatusAsync(int semesterId)
        {
            try
            {
                // Lấy học kỳ từ cơ sở dữ liệu
                var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.SemesterId == semesterId);

                // Kiểm tra xem học kỳ có tồn tại hay không
                if (semester == null)
                {
                    return false; // Nếu không tìm thấy, trả về false
                }

                // Chuyển đổi trạng thái của học kỳ
                semester.IsActive = !semester.IsActive;

                // Lưu thay đổi vào cơ sở dữ liệu
                await dbContext.SaveChangesAsync();

                return true; // Trả về true nếu thành công
            }
            catch (Exception ex)
            {

                return false; // Trả về false nếu có lỗi xảy ra
            }
        }

        public async Task<RequestResponse> UpdateSemesterAsync(SemesterRequest request)
        {
            try
            {
                // Kiểm tra xem học kỳ có tồn tại không
                var existingSemester = await dbContext.Semesters.FirstOrDefaultAsync(s => s.SemesterId == request.SemesterID);
                if (existingSemester == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Semester not found!"
                    };
                }

                var duplicateSemester = await dbContext.Semesters
                    .FirstOrDefaultAsync(s => s.SemesterName == request.SemesterName && s.SemesterId != request.SemesterID);
                if (duplicateSemester != null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Another semester with this name already exists!"
                    };
                }

                // Kiểm tra EndDate có lớn hơn StartDate không
                if (request.EndDate <= request.StartDate)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "End date must be greater than start date!"
                    };
                }
                existingSemester.SemesterName = request.SemesterName;
                existingSemester.StartDate = request.StartDate;
                existingSemester.EndDate = request.EndDate;
                existingSemester.IsActive = request.IsActive;
                existingSemester.UpdatedDate = DateTime.Now;

                dbContext.Semesters.Update(existingSemester);
                await dbContext.SaveChangesAsync();

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Semester updated successfully!"
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

        Task<ResultResponse<SemesterResponse>> ISemesterRepository.GetActiveSemestersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
