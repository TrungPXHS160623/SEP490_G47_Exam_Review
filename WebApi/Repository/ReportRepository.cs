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


        public async Task<RequestResponse> AddEditReport(LectureExamResponse reportRequest)
        {
            try
            {
                var list = await (from rp in dbContext.Reports
                                  join ia in dbContext.InstructorAssignments on rp.AssignemtId equals ia.AssignmentId
                                  where ia.ExamId == reportRequest.ExamId && ia.AssignedUserId == reportRequest.AssignmentUserId
                                  select rp).ToListAsync();

                var deleteRecord = list.Where(x => !reportRequest.ReportList.Any(y => y.RerportId == x.ReportId)).ToList();

                this.dbContext.Reports.RemoveRange(deleteRecord);


                foreach (var item in reportRequest.ReportList)
                {
                    var data = await this.dbContext.Reports.FirstOrDefaultAsync(x => x.ReportId == item.RerportId);

                    if(data == null)
                    {
                        var newRecord = new Report
                        {
                            QuestionNumber = item.QuestionNumber,
                            ReportContent = item.ReportContent,
                            QuestionSolutionDetail = item.QuestionSolutionDetail,
                            CreateDate = item.CreateDate,
                            UpdateDate = item.UpdateDate,
                            AssignemtId = reportRequest.AssignmentId,
                        };

                        await this.dbContext.Reports.AddAsync(newRecord);
                    } else
                    {
                        data.QuestionNumber = item.QuestionNumber;
                        data.ReportContent = item.ReportContent;
                        data.QuestionSolutionDetail = item.QuestionSolutionDetail;
                        data.UpdateDate = item.UpdateDate;
                    }
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
