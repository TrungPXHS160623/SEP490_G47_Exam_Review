using Library.Common;
using Library.Models;
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
                                  where rp.ExamId == reportRequest.ExamId
                                  select rp).ToListAsync();

                var deleteRecord = list.Where(x => !reportRequest.ReportList.Any(y => y.ReportId == x.ReportId)).ToList();

                this.dbContext.Reports.RemoveRange(deleteRecord);

                foreach (var item in reportRequest.ReportList)
                {
                    var data = await this.dbContext.Reports.FirstOrDefaultAsync(x => x.ReportId == item.ReportId);
                    var newRecord = new Report();

                    if (data == null)
                    {
                        //Khi tạo mới báo cáo (chưa ấn nút submit):
                        newRecord = new Report
                        {
                            ExamId = reportRequest.ExamId,
                            QuestionNumber = item.QuestionNumber,
                            ReportContent = item.ReportContent,
                            QuestionSolutionDetail = item.QuestionSolutionDetail,
                            CreateDate = item.CreateDate != null ? item.CreateDate : DateTime.Now,
                            UpdateDate = item.UpdateDate,
                        };

                        await this.dbContext.Reports.AddAsync(newRecord);
                        await this.dbContext.SaveChangesAsync();

                    }
                    else
                    {
                        data.QuestionNumber = item.QuestionNumber;
                        data.ReportContent = item.ReportContent;
                        data.QuestionSolutionDetail = item.QuestionSolutionDetail;
                        data.UpdateDate = DateTime.Now;
                    }

                    if (item.ImageList.Count > 0)
                    {
                        var imageList = await (from rf in dbContext.ReportFiles
                                               where rf.ReportId == item.ReportId
                                               select rf).ToListAsync();

                        var deleteImage = imageList.Where(x => !item.ImageList.Any(y => y.FileId == x.FileId)).ToList();

                        if (deleteImage.Any())
                        {
                            this.dbContext.ReportFiles.RemoveRange(deleteImage);
                        }

                        var addImage = item.ImageList.Where(x => !imageList.Any(y => y.FileId == x.FileId)).ToList();

                        if (addImage.Any())
                        {
                            foreach (var image in addImage)
                            {
                                var i = new ReportFile
                                {
                                    ReportId = item.ReportId ?? newRecord.ReportId,

                                    FilePath = image.FileData,
                                };

                                await this.dbContext.ReportFiles.AddAsync(i);
                            }
                        }

                    }

                }

                var exam = await this.dbContext.Exams.FirstOrDefaultAsync(x => x.ExamId == reportRequest.ExamId);
                exam.GeneralFeedback = reportRequest.Summary;
                if (isSubmit)
                {
                    if(reportRequest.ReportList.Count > 0)
                    {
                        exam.ExamStatusId = 5; 
                    } else
                    {
                        exam.ExamStatusId = 6;  
                    }
                    exam.UpdateDate = DateTime.Now;  // Cập nhật thời gian submit
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

        public async Task<ResultResponse<ReportDurationResponse>> GetReportDuration(int examId)
        {
            try
            {
                //tìm kiếm report dựa theo id của phần phân công
                var reports = await dbContext.Reports.Where(r => r.ExamId == examId).ToListAsync();

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

                    DurationHours = r.UpdateDate.HasValue && r.CreateDate.HasValue ? (r.UpdateDate.Value - r.CreateDate.Value).TotalHours : 0,

                    //DurationHours = r.CreateDate != null && r.UpdateDate != null ? (r.UpdateDate.Value - r.CreateDate.Value).TotalHours : r.CreateDate != null? (DateTime.Now - r.CreateDate.Value).TotalHours: 0,
                }).ToList();

                // Tính tổng thời gian
                var totalDuration = reportDurations.Sum(rd => rd.DurationHours);

                // Tạo DTO trả về
                var responseDto = new ReportDurationResponse
                {
                    //AssignmentId = assignmentId,
                    TotalDurationHours = totalDuration,
                    ReportDurations = reportDurations
                };

                return new ResultResponse<ReportDurationResponse>
                {
                    IsSuccessful = true,
                    Items = new List<ReportDurationResponse> { responseDto }
                };

            }
            catch (Exception ex)
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

        public async Task<RequestResponse> UploadFiles(int reportId, IList<IFormFile> files)
        {
            var response = new RequestResponse();
            var errors = new List<string>();
            var existingFileNames = new HashSet<string>();
            try
            {
                // Kiểm tra file đính kèm có bằng null không
                if (files == null || files.Count == 0)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "No files uploaded."
                    };
                }

                // Danh sách các định dạng tệp và MIME được hỗ trợ
                var supportedTypes = new[] { "xlsx", "pdf", "docx", "doc", "xls", "jpg", "png", "zip" };
                var allowedMimeTypes = new[]
                {
                  "application/pdf",
                  "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                  "application/msword",
                  "application/vnd.ms-excel",
                  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                  "image/jpeg",
                  "image/png",
                  "application/zip"
                 };

                // Giới hạn dung lượng file (ví dụ: 5MB)
                const long MaxFileSize = 5 * 1024 * 1024;

                // Thiết lập thư mục lưu file upload
                var baseUploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Reports");
                if (!Directory.Exists(baseUploadsFolder))
                {
                    Directory.CreateDirectory(baseUploadsFolder);
                }

                // Thư mục cho báo cáo cụ thể
                var uploadsFolder = Path.Combine(baseUploadsFolder, reportId.ToString());
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }


                foreach (var file in files)
                {
                    /*
                     Nếu file.FileName là "image.JPG", thì:
                     Path.GetExtension(file.FileName) sẽ trả về ".JPG".
                     Substring(1) sẽ trả về "JPG".
                     ToLower() sẽ chuyển đổi nó thành "jpg".
                     */
                    var fileExtension = Path.GetExtension(file.FileName).Substring(1).ToLower();

                    // Kiểm tra định dạng tệp
                    if (!supportedTypes.Contains(fileExtension))
                    {
                        errors.Add($"File type '{file.FileName}' is not supported.");
                        continue; // Bỏ qua file không hợp lệ
                    }

                    // Kiểm tra loại MIME
                    if (!allowedMimeTypes.Contains(file.ContentType.ToLower()))
                    {
                        errors.Add($"MIME type '{file.ContentType}' is not supported for file '{file.FileName}'.");
                        continue; // Bỏ qua file quá lớn
                    }

                    // Kiểm tra kích thước file
                    if (file.Length > MaxFileSize)
                    {
                        errors.Add($"File {file.FileName} size exceeds the maximum allowed limit of {MaxFileSize / (1024 * 1024)}MB.");
                        continue;
                    }
                    // Kiểm tra xem tên file đã tồn tại trong HashSet chưa
                    if (existingFileNames.Contains(file.FileName))
                    {
                        errors.Add($"Duplicate file name '{file.FileName}' within the current upload.");
                        continue; // Bỏ qua file trùng lặp
                    }

                    // Thêm tên file vào HashSet để kiểm tra sau
                    existingFileNames.Add(file.FileName);

                    if (file.FileName.Length > 255)
                    {
                        errors.Add($"File {file.FileName} 's name is too long. ");
                        continue;
                    }

                    // Lấy danh sách các ký tự không hợp lệ
                    char[] invalidChars = Path.GetInvalidFileNameChars();

                    // Kiểm tra xem tên tệp có chứa ký tự không hợp lệ không
                    if (invalidChars.Any(c => file.FileName.Contains(c)) || file.FileName.Contains(".."))
                    {
                        errors.Add("Invalid file name or path.");
                        continue;
                    }

                    var existingFile = await dbContext.ReportFiles.FirstOrDefaultAsync(f => f.FileName == file.FileName && f.ReportId == reportId);
                    if (existingFile != null)
                    {
                        errors.Add($"File '{file.FileName}' already exists.");
                        continue;
                    }
                    // Đường dẫn file
                    var filePath = Path.Combine(uploadsFolder, Path.GetFileName(file.FileName));

                    // Ghi file lên server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Tạo đối tượng ReportFile và lưu vào database
                    var reportFile = new ReportFile
                    {
                        ReportId = reportId,
                        FileName = file.FileName,
                        FilePath = filePath,
                        FileType = fileExtension,
                        FileSize = file.Length,
                        UploadDate = DateTime.Now
                    };

                    dbContext.ReportFiles.Add(reportFile);
                }

                await dbContext.SaveChangesAsync();
                if (errors.Any())
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = $"There were errors during file upload: {string.Join("; ", errors)}"
                    };
                }
                else
                {
                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "All files uploaded successfully!"
                    };
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<RequestResponse> UploadReportWithFiles(LectureExamResponseFinal reportRequest, bool isSubmit)
        {
            try
            {
                // Lấy danh sách báo cáo hiện tại dựa trên AssignmentId
                var existingReports = await dbContext.Reports
                                                     .Where(rp => rp.ExamId == reportRequest.ExamId)
                                                     .ToListAsync();

                // Xóa các báo cáo không còn trong request
                var deleteRecord = existingReports.Where(x => !reportRequest.ReportList.Any(y => y.ReportId == x.ReportId)).ToList();
                dbContext.Reports.RemoveRange(deleteRecord);

                foreach (var item in reportRequest.ReportList)
                {
                    // Kiểm tra nếu báo cáo đã tồn tại trong cơ sở dữ liệu
                    var existingReport = await dbContext.Reports.FirstOrDefaultAsync(x => x.ReportId == item.ReportId);

                    if (existingReport == null)
                    {
                        // Tạo mới báo cáo
                        var newReport = new Report
                        {
                            QuestionNumber = item.QuestionNumber,
                            ReportContent = item.ReportContent,
                            QuestionSolutionDetail = item.QuestionSolutionDetail,
                            CreateDate = item.CreateDate ?? DateTime.Now, // Sử dụng thời gian hiện tại nếu không có CreateDate
                            UpdateDate = DateTime.Now, // Cập nhật ngay tại đây
                            ExamId = reportRequest.ExamId,
                        };

                        await dbContext.Reports.AddAsync(newReport);
                        await dbContext.SaveChangesAsync(); // Lưu thay đổi để có ReportId

                        // Tải lên tệp ngay sau khi tạo báo cáo mới
                        var uploadResponse = await UploadFiles(newReport.ReportId, item.Files); // Truyền files từ item
                        if (!uploadResponse.IsSuccessful)
                        {
                            return new RequestResponse
                            {
                                IsSuccessful = false,
                                Message = uploadResponse.Message
                            };
                        }
                    }
                    else
                    {
                        // Khi chỉnh sửa báo cáo
                        existingReport.QuestionNumber = item.QuestionNumber;
                        existingReport.ReportContent = item.ReportContent;
                        existingReport.QuestionSolutionDetail = item.QuestionSolutionDetail;
                        existingReport.UpdateDate = DateTime.Now; // Cập nhật thời gian chỉnh sửa

                        // Cập nhật file nếu cần thiết
                        // Gọi UploadFiles nếu có file mới
                        var uploadResponse = await UploadFiles(existingReport.ReportId, item.Files); // Truyền files từ item
                        if (!uploadResponse.IsSuccessful)
                        {
                            return new RequestResponse
                            {
                                IsSuccessful = false,
                                Message = uploadResponse.Message
                            };
                        }
                    }
                }

                // Cập nhật trạng thái nộp nếu isSubmit là true
                if (isSubmit)
                {
                    var assignment = await dbContext.Exams.FirstOrDefaultAsync(x => x.ExamId == reportRequest.ExamId);
                    if (assignment != null)
                    {
                        assignment.ExamStatusId = 5; // Đặt trạng thái là đã nộp
                        assignment.UpdateDate = DateTime.Now; // Cập nhật thời gian submit
                    }
                }

                await dbContext.SaveChangesAsync(); // Lưu tất cả thay đổi

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Report saved successfully!"
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
