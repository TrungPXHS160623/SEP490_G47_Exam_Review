using Library.Common;
using Library.Models;
using Library.Models.Dtos;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

public class ExamRepository : IExamRepository
{
    private readonly QuizManagementContext _context;

    public ExamRepository(QuizManagementContext context)
    {
        _context = context;
    }

    public async Task<RequestResponse> ChangeStatusExam(List<TestDepartmentExamResponse> exam)
    {
        try
        {
            var examIds = exam.Select(x => x.ExamId).ToList();

            var examsToUpdate = await this._context.Exams
                .Where(x => examIds.Contains(x.ExamId))
                .ToListAsync();

            if (examsToUpdate.Count != examIds.Count)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = "Error! Some exams do not exist.",
                };
            }

            foreach (var e in examsToUpdate)
            {
                e.ExamStatusId = 2;
                e.UpdateDate = DateTime.Now;
            }

            await this._context.SaveChangesAsync();

            return new RequestResponse
            {
                IsSuccessful = true,
                Message = "Update Exam Status Successfully",
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

    //public async Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId)
    //{
    //    try
    //    {
    //        var data = (from ex in _context.Exams
    //                    join su in _context.Subjects on ex.SubjectId equals su.SubjectId
    //                    join u1 in _context.CampusUserSubjects on su.HeadOfDepartmentId equals u1.UserId
    //                    join u2 in _context.Users on ex.CreaterId equals u2.UserId
    //                    join st in _context.ExamStatuses on ex.ExamStatusId equals st.ExamStatusId
    //                    join ca in _context.Campuses on ex.CampusId equals ca.CampusId
    //                    where ex.ExamId == examId
    //                    select new TestDepartmentExamResponse
    //                    {
    //                        CreaterId = u2.UserId,
    //                        CreaterName = u2.Mail,
    //                        EndDate = ex.EndDate,
    //                        StartDate = ex.StartDate,
    //                        ExamCode = ex.ExamCode,
    //                        SubjectCode = su.SubjectCode,
    //                        CampusId = ca.CampusId,
    //                        CampusName = ca.CampusName,
    //                        EstimatedTimeTest = ex.EstimatedTimeTest,
    //                        ExamDuration = ex.ExamDuration,
    //                        ExamId = ex.ExamId,
    //                        ExamStatusContent = st.StatusContent,
    //                        ExamStatusId = st.ExamStatusId,
    //                        ExamType = ex.ExamType,
    //                        HeadDepartmentId = u1.UserId,
    //                        HeadDepartmentName = u1.Mail,
    //                        SubjectId = su.SubjectId,
    //                        SubjectName = su.SubjectName,
    //                        UpdateDate = ex.UpdateDate,
    //                    }).FirstOrDefault();

    //        return new ResultResponse<TestDepartmentExamResponse>
    //        {
    //            IsSuccessful = true,
    //            Item = data,
    //        };
    //    }
    //    catch (Exception ex)
    //    {
    //        return new ResultResponse<TestDepartmentExamResponse>
    //        {
    //            IsSuccessful = false,
    //            Message = ex.Message,
    //        };
    //    }
    //}

    public async Task<IEnumerable<ExamInfoDto>> GetExamInfoAsync()
    {
        var examInfo = await _context.InstructorAssignments
            .Include(ia => ia.Exam) // Kết nối với Exam
            .Include(ia => ia.Exam.Subject) // Kết nối với Subject từ Exam
            .Include(ia => ia.Exam.ExamStatus) // Kết nối với ExamStatuses
                                               //.Include(ia => ia.AssignedToNavigation) // Kết nối với giảng viên
            .Select(ia => new ExamInfoDto
            {
                //DepartmentName = ia.Exam.Subject.Department.DepartmentName, // Tên Chuyên Ngành
                SubjectName = ia.Exam.Subject.SubjectName, // Tên Môn Học
                ExamCode = ia.Exam.ExamCode, // Mã Bài Thi
                Status = ia.Exam.ExamStatus.StatusContent, // Trạng Thái
                                                           //InstructorName = ia.AssignedToNavigation.Mail // Tên Giảng Viên
            })
            .ToListAsync();

        return examInfo;
    }

    /*  public async Task<ResultResponse<TestDepartmentExamResponse>> GetExamList(ExamSearchRequest req)
      {
          try
          {
              var data = (from ex in _context.Exams
                          join su in _context.Subjects on ex.SubjectId equals su.SubjectId
                          join u1 in _context.Users on su.HeadOfDepartmentId equals u1.UserId
                          join u2 in _context.Users on ex.CreaterId equals u2.UserId
                          join st in _context.ExamStatuses on ex.ExamStatusId equals st.ExamStatusId
                          join ca in _context.Campuses on ex.CampusId equals ca.CampusId
                          where (req.StatusId == null || ex.ExamStatusId == req.StatusId)
                          && (string.IsNullOrEmpty(req.ExamCode) || ex.ExamCode.Contains(req.ExamCode))
                          select new TestDepartmentExamResponse
                          {
                              CreaterId = u2.UserId,
                              CreaterName = u2.Mail,
                              EndDate = ex.EndDate,
                              StartDate = ex.StartDate,
                              ExamCode = ex.ExamCode,
                              SubjectCode = su.SubjectCode,
                              CampusId = ca.CampusId,
                              CampusName = ca.CampusName,
                              EstimatedTimeTest = ex.EstimatedTimeTest,
                              ExamDuration = ex.ExamDuration,
                              ExamId = ex.ExamId,
                              ExamStatusContent = st.StatusContent,
                              ExamStatusId = st.ExamStatusId,
                              ExamType = ex.ExamType,
                              HeadDepartmentId = u1.UserId,
                              HeadDepartmentName = u1.Mail,
                              SubjectId = su.SubjectId,
                              SubjectName = su.SubjectName,
                              UpdateDate = ex.UpdateDate,
                          }).ToList();

              return new ResultResponse<TestDepartmentExamResponse>
              {
                  IsSuccessful = true,
                  Items = data.OrderByDescending(x => x.UpdateDate).ToList(),
              };
          }
          catch (Exception ex)
          {
              return new ResultResponse<TestDepartmentExamResponse>
              {
                  IsSuccessful = false,
                  Message = ex.Message,
              };
          }
      }*/

    public async Task<RequestResponse> UpdateExam(TestDepartmentExamResponse exam)
    {
        try
        {
            var e = await this._context.Exams.FirstOrDefaultAsync(x => x.ExamId == exam.ExamId);

            if (e == null)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = "Exam not exist!",
                };
            }
            else
            {
                e.EndDate = exam.EndDate;
                e.StartDate = exam.StartDate;
                e.ExamCode = exam.ExamCode;
                e.CampusId = exam.CampusId.Value;
                e.ExamDuration = exam.ExamDuration;
                e.ExamType = exam.ExamType;
                e.UpdateDate = DateTime.Now;

                await this._context.SaveChangesAsync();

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Update Successfully",
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
