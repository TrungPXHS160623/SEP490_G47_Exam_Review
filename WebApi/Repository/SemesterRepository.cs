using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Repository
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly QuizManagementContext dbContext;

        public SemesterRepository(QuizManagementContext dbContext)
        {
            this.dbContext = dbContext;
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

        public async Task<bool> DeleteSemesterAsync(int semesterId)
        {
            try
            {
                // Kiểm tra sự tồn tại của học kỳ
                var semester = await dbContext.Semesters.FirstOrDefaultAsync(s => s.SemesterId == semesterId);
                if (semester == null)
                {
                    return false; // Trả về false nếu học kỳ không tồn tại
                }

                // Kiểm tra xem học kỳ có liên quan đến các bản ghi khác (ví dụ như kỳ thi, lớp học, v.v.)
                var relatedExams = await dbContext.SemesterCampusUserSubjects.AnyAsync(e => e.SemesterId == semesterId);
                if (relatedExams)
                {
                    // Nếu có kỳ thi nào liên quan đến học kỳ này, không thể xóa
                    throw new InvalidOperationException("Cannot delete semester because it is related to existing exams.");
                }

                // Xóa học kỳ khỏi cơ sở dữ liệu
                dbContext.Semesters.Remove(semester);
                await dbContext.SaveChangesAsync();

                return true; // Trả về true nếu xóa thành công
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu có (nếu cần)
                // Log.Error(ex.Message);
                return false; // Trả về false nếu có lỗi xảy ra
            }
        }

        public async Task<ResultResponse<SemesterResponse>> GetActiveSemestersAsync()
        {
            try
            {
                // Lấy danh sách tất cả các học kỳ từ cơ sở dữ liệu
                var listSemester = await dbContext.Semesters.Where(x => x.IsActive == true).ToListAsync();

                // Chuyển đổi danh sách Semester thành SemesterResponse
                var semesterResponses = listSemester.Select(semester => new SemesterResponse
                {
                    SemesterId = semester.SemesterId,
                    SemesterName = semester.SemesterName,
                    StartDate = semester.StartDate,
                    EndDate = semester.EndDate,
                    IsActive = semester.IsActive,
                    CreatedDate = semester.CreatedDate,
                    UpdatedDate = semester.UpdatedDate
                }).ToList();

                return new ResultResponse<SemesterResponse>
                {
                    IsSuccessful = true,
                    Items = semesterResponses
                };

            }
            catch (Exception ex)
            {

                return new ResultResponse<SemesterResponse>
                {
                    IsSuccessful = true,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultResponse<SemesterResponse>> GetSemesterByIdAsync(int semesterId)
        {
            try
            {
                // Lấy học kỳ theo ID từ cơ sở dữ liệu
                var semester = await dbContext.Semesters.FirstOrDefaultAsync(x => x.SemesterId == semesterId);

                // Kiểm tra nếu không tìm thấy học kỳ
                if (semester == null)
                {
                    return new ResultResponse<SemesterResponse>
                    {
                        IsSuccessful = false,
                        Message = "Semester not found."
                    };
                }

                // Chuyển đổi đối tượng Semester thành SemesterResponse
                var semesterResponse = new SemesterResponse
                {
                    SemesterId = semester.SemesterId,
                    SemesterName = semester.SemesterName,
                    StartDate = semester.StartDate,
                    EndDate = semester.EndDate,
                    IsActive = semester.IsActive,
                    CreatedDate = semester.CreatedDate,
                    UpdatedDate = semester.UpdatedDate
                };

                return new ResultResponse<SemesterResponse>
                {
                    IsSuccessful = true,
                    Item = semesterResponse
                };
            }
            catch (Exception ex)
            {

                return new ResultResponse<SemesterResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultResponse<SemesterResponse>> GetSemestersAsync()
        {
            try
            {
                // Lấy danh sách tất cả các học kỳ từ cơ sở dữ liệu
                var listSemester = await dbContext.Semesters.ToListAsync();

                // Chuyển đổi danh sách Semester thành SemesterResponse
                var semesterResponses = listSemester.Select(semester => new SemesterResponse
                {
                    SemesterId = semester.SemesterId,
                    SemesterName = semester.SemesterName,
                    StartDate = semester.StartDate,
                    EndDate = semester.EndDate,
                    IsActive = semester.IsActive,
                    CreatedDate = semester.CreatedDate,
                    UpdatedDate = semester.UpdatedDate
                }).ToList();

                return new ResultResponse<SemesterResponse>
                {
                    IsSuccessful = true,
                    Items = semesterResponses
                };

            }
            catch (Exception ex)
            {

                return new ResultResponse<SemesterResponse>
                {
                    IsSuccessful = true,
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

        public async Task<RequestResponse> UpdateSemesterAsync(int semesterId, SemesterRequest request)
        {
            try
            {
                // Kiểm tra xem học kỳ có tồn tại không
                var existingSemester = await dbContext.Semesters.FirstOrDefaultAsync(s => s.SemesterId == semesterId);
                if (existingSemester == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Semester not found!"
                    };
                }

                // Kiểm tra xem tên học kỳ có bị trùng với học kỳ khác không (ngoại trừ chính nó)
                var duplicateSemester = await dbContext.Semesters
                    .FirstOrDefaultAsync(s => s.SemesterName == request.SemesterName && s.SemesterId != semesterId);
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

                // Lấy học kỳ gần nhất (theo thời gian kết thúc) nhưng khác với học kỳ hiện tại
                var lastSemester = await dbContext.Semesters
                    .Where(s => s.SemesterId != semesterId)
                    .OrderByDescending(s => s.EndDate)
                    .FirstOrDefaultAsync();

                // Kiểm tra xem thời gian bắt đầu của học kỳ mới có nhỏ hơn thời gian kết thúc của học kỳ gần nhất không
                if (lastSemester != null && request.StartDate < lastSemester.EndDate)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Start date of the updated semester cannot be earlier than the end date of the last semester!"
                    };
                }

                // Cập nhật thông tin học kỳ
                existingSemester.SemesterName = request.SemesterName;
                existingSemester.StartDate = request.StartDate;
                existingSemester.EndDate = request.EndDate;
                existingSemester.IsActive = request.IsActive;
                existingSemester.UpdatedDate = DateTime.Now;

                // Lưu thay đổi vào cơ sở dữ liệu
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
        
    }
}
