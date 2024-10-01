﻿using Library.Common;
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


        public async Task<RequestResponse> CreateReport(ReportRequest reportRequest)
        {
            try
            {
                // Kiểm tra ExamId
                var examExists = await dbContext.Exams.AnyAsync(e => e.ExamId == reportRequest.ExamId);
                if (!examExists)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Exam does not exist."
                    };
                }

                // Kiểm tra UserId
                var userExists = await dbContext.Users.AnyAsync(u => u.UserId == reportRequest.UserId);
                if (!userExists)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "User does not exist."
                    };
                }

                // Kiểm tra QuestionNumber
                if (reportRequest.QuestionNumber <= 0)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Question number must be greater than zero."
                    };
                }

                // Kiểm tra ReportContent
                if (string.IsNullOrWhiteSpace(reportRequest.ReportContent))
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Report content cannot be empty."
                    };
                }

                // Kiểm tra Score
                if (reportRequest.Score < 0 || reportRequest.Score > 10)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Score must be between 0 and 10."
                    };
                }
                // Kiểm tra CreateDate
                if (reportRequest.CreateDate > DateTime.UtcNow)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Create date cannot be in the future."
                    };
                }
                var newReport = new Report
                {
                    ExamId = reportRequest.ExamId,
                    UserId = reportRequest.UserId,
                    QuestionNumber = reportRequest.QuestionNumber,
                    ReportContent = reportRequest.ReportContent,
                    QuestionSolutionDetail = reportRequest.QuestionSolutionDetail,
                    Score = reportRequest.Score,
                    CreateDate = DateTime.Now,
                };
                await dbContext.Reports.AddAsync(newReport);
                await dbContext.SaveChangesAsync();
                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Create Report successfully!"
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
				existingReport.ExamId = reportRequest.ExamId;
				existingReport.UserId = reportRequest.UserId;
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
            // Xác thực ID
            if (lecturerId <= 0)
            {
                return new ResultResponse<ReportResponse>
                {
                    IsSuccessful = false,
                    Message = "ID người dùng không hợp lệ."
                };
            }

            try
            {
                // Kiểm tra xem người dùng có tồn tại không
                var userExists = await this.dbContext.Users.AnyAsync(u => u.UserId == lecturerId);
                if (!userExists)
                {
                    return new ResultResponse<ReportResponse>
                    {
                        IsSuccessful = false,
                        Message = "Người dùng không tồn tại."
                    };
                }

                var data = (from r in this.dbContext.Reports
                            join e in this.dbContext.Exams on r.ExamId equals e.ExamId
                            join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId
                            join uReviewer in this.dbContext.Users on r.UserId equals uReviewer.UserId into uReviewerJoin
                            from uReviewer in uReviewerJoin.DefaultIfEmpty()
                            where uReviewer.UserId == lecturerId
                            select new ReportResponse
                            {
                                ExamCode = e.ExamCode,
                                SubjectName = s.SubjectName,
                                ReviewerMail = uReviewer.Mail,
                                QuestionNumber = r.QuestionNumber,
                                ReportContent = r.ReportContent,
                                QuestionSolutionDetail = r.QuestionSolutionDetail,
                                Score = r.Score,
                                CreateDate = r.CreateDate,
                                UpdateDate = r.UpdateDate
                            }).ToList();

                // Kiểm tra dữ liệu trả về
                if (data == null || !data.Any())
                {
                    return new ResultResponse<ReportResponse>
                    {
                        IsSuccessful = false,
                        Message = "Không tìm thấy báo cáo nào với giảng viên tương ứng"
                    };
                }

                return new ResultResponse<ReportResponse>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<ReportResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
