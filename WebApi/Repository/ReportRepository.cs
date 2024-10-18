using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly QuizManagementContext dbContext;

        public ReportRepository(QuizManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<RequestResponse> AddEditReport(LectureExamResponse reportRequest, bool isSubmit)
        {
            try
            {
                var list = await (from rp in dbContext.Reports
                                  where rp.AssignmentId == reportRequest.AssignmentId
                                  select rp).ToListAsync();

                var deleteRecord = list.Where(x => !reportRequest.ReportList.Any(y => y.RerportId == x.ReportId)).ToList();

                this.dbContext.Reports.RemoveRange(deleteRecord);


                foreach (var item in reportRequest.ReportList)
                {
                    var data = await this.dbContext.Reports.FirstOrDefaultAsync(x => x.ReportId == item.RerportId);

                    if (data == null)
                    {
                        //Khi tạo mới báo cáo (chưa ấn nút submit):
                        var newRecord = new Report
                        {
                            QuestionNumber = item.QuestionNumber,
                            ReportContent = item.ReportContent,
                            QuestionSolutionDetail = item.QuestionSolutionDetail,
                            CreateDate = item.CreateDate != null ? item.CreateDate : DateTime.Now,  // Sử dụng thời gian hiện tại nếu không có CreateDate
                            UpdateDate = item.UpdateDate,  // UpdateDate có thể được cập nhật sau
                            AssignmentId = reportRequest.AssignmentId,

                        };

                        await this.dbContext.Reports.AddAsync(newRecord);
                    }
                    else
                    {
                        //Khi chỉnh sửa báo cáo(nhưng chưa ấn submit):
                        data.QuestionNumber = item.QuestionNumber;
                        data.ReportContent = item.ReportContent;
                        data.QuestionSolutionDetail = item.QuestionSolutionDetail;
                        data.UpdateDate = DateTime.Now;  // Khi chỉnh sửa, UpdateDate được cập nhật với thời gian hiện tại
                    }
                }

                if (isSubmit)
                {
                    var assignment = await this.dbContext.InstructorAssignments.FirstOrDefaultAsync(x => x.AssignmentId == reportRequest.AssignmentId);
                    assignment.AssignStatusId = 5;  // Đặt trạng thái là đã nộp
                    assignment.UpdateDate = DateTime.Now;  // Cập nhật thời gian submit
                }


                await this.dbContext.SaveChangesAsync();

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Report saves successfully!"
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

        public async Task<RequestResponse> EditReportById(int reportId, ReportRequest reportRequest)
        {
            try
            {
                // Check if the report exists
                var existingReport = await dbContext.Reports.FindAsync(reportId);
                if (existingReport == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Report not found."
                    };
                }

                // Check if the ExamId exists
                var examExists = await dbContext.Exams.AnyAsync(e => e.ExamId == reportRequest.ExamId);
                if (!examExists)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Exam does not exist."
                    };
                }

                // Check if the UserId exists
                var userExists = await dbContext.Users.AnyAsync(u => u.UserId == reportRequest.UserId);
                if (!userExists)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "User does not exist."
                    };
                }

                // Validate QuestionNumber
                if (reportRequest.QuestionNumber <= 0)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Question number must be greater than zero."
                    };
                }

                // Validate ReportContent
                if (string.IsNullOrWhiteSpace(reportRequest.ReportContent))
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Report content cannot be empty."
                    };
                }

                // Validate Score
                if (reportRequest.Score < 0 || reportRequest.Score > 10)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Score must be between 0 and 10."
                    };
                }

                // Update the existing report with new values
                existingReport.QuestionNumber = reportRequest.QuestionNumber;
                existingReport.ReportContent = reportRequest.ReportContent;
                existingReport.QuestionSolutionDetail = reportRequest.QuestionSolutionDetail;
                existingReport.Score = reportRequest.Score;
                existingReport.UpdateDate = DateTime.Now;

                dbContext.Reports.Update(existingReport);
                await dbContext.SaveChangesAsync();

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Report updated successfully!"
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

        public async Task<ResultResponse<ReportDurationResponse>> GetReportDuration(int assignmentId)
        {
            try
            {
                //tìm kiếm report dựa theo id của phần phân công
                var reports = await dbContext.Reports.Where(r => r.AssignmentId == assignmentId).ToListAsync();

                //nếu không tìm thấy report nào 
                if (reports == null || reports.Count == 0)
                {
                    return new ResultResponse<ReportDurationResponse>
                    {
                        IsSuccessful = false,
                        Message = "No reports found for this assignment."
                    };
                }

                // nếu tìm thấy report 
                // Tính thời gian từng report
                var reportDurations = reports.Select(r => new ReportDurationDetail
                {
                    ReportId = r.ReportId,
                    QuestionNumber = r.QuestionNumber.HasValue ? r.QuestionNumber.Value : 0,
                    // Tính toán tổng số giờ giữa thời điểm tạo và thời điểm cập nhật.
                    // Nếu cả CreateDate và UpdateDate đều không null, tính thời gian chênh lệch giữa chúng.
                    // Nếu chỉ có CreateDate không null, tính thời gian từ CreateDate đến thời điểm hiện tại.
                    // Nếu cả hai đều null, gán giá trị 0.

                    DurationHours = r.UpdateDate.HasValue && r.CreateDate.HasValue ? (r.UpdateDate.Value - r.CreateDate.Value).TotalHours: 0,

                    //DurationHours = r.CreateDate != null && r.UpdateDate != null ? (r.UpdateDate.Value - r.CreateDate.Value).TotalHours : r.CreateDate != null? (DateTime.Now - r.CreateDate.Value).TotalHours: 0,
                }).ToList();

                // Tính tổng thời gian
                var totalDuration = reportDurations.Sum(rd => rd.DurationHours);

                // Tạo DTO trả về
                var responseDto = new ReportDurationResponse
                {
                    AssignmentId = assignmentId,
                    TotalDurationHours = totalDuration,
                    ReportDurations = reportDurations
                };

                return new ResultResponse<ReportDurationResponse>
                {
                    IsSuccessful = true,
                    Items = new List<ReportDurationResponse> { responseDto }
                };
            
            }
            catch (Exception ex )
            {

                return new ResultResponse<ReportDurationResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultResponse<ReportResponse>> GetReportsByLecturerId(int lecturerId)
        {
            //// Xác thực ID
            //if (lecturerId <= 0)
            //{
            //    return new ResultResponse<ReportResponse>
            //    {
            //        IsSuccessful = false,
            //        Message = "ID người dùng không hợp lệ."
            //    };
            //}

            //try
            //{
            //    // Kiểm tra xem người dùng có tồn tại không
            //    var userExists = await this.dbContext.Users.AnyAsync(u => u.UserId == lecturerId);
            //    if (!userExists)
            //    {
            //        return new ResultResponse<ReportResponse>
            //        {
            //            IsSuccessful = false,
            //            Message = "Người dùng không tồn tại."
            //        };
            //    }

            //    var data = (from r in this.dbContext.Reports
            //                join e in this.dbContext.Exams on r.ExamId equals e.ExamId
            //                join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId
            //                join uReviewer in this.dbContext.Users on r.UserId equals uReviewer.UserId into uReviewerJoin
            //                from uReviewer in uReviewerJoin.DefaultIfEmpty()
            //                where uReviewer.UserId == lecturerId
            //                select new ReportResponse
            //                {
            //                    ExamCode = e.ExamCode,
            //                    SubjectName = s.SubjectName,
            //                    ReviewerMail = uReviewer.Mail,
            //                    QuestionNumber = r.QuestionNumber,
            //                    ReportContent = r.ReportContent,
            //                    QuestionSolutionDetail = r.QuestionSolutionDetail,
            //                    Score = r.Score,
            //                    CreateDate = r.CreateDate,
            //                    UpdateDate = r.UpdateDate
            //                }).ToList();

            //    // Kiểm tra dữ liệu trả về
            //    if (data == null || !data.Any())
            //    {
            //        return new ResultResponse<ReportResponse>
            //        {
            //            IsSuccessful = false,
            //            Message = "Không tìm thấy báo cáo nào với giảng viên tương ứng"
            //        };
            //    }

            //    return new ResultResponse<ReportResponse>
            //    {
            //        IsSuccessful = true,
            //        Items = data
            //    };
            //}
            //catch (Exception ex)
            //{
            //    return new ResultResponse<ReportResponse>
            //    {
            //        IsSuccessful = false,
            //        Message = ex.Message,
            //    };
            //}

            return null;
        }
    }
}
