using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;
using static MudBlazor.Colors;

namespace WebApi.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly QuizManagementContext DBcontext;

        public SubjectRepository(QuizManagementContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }

        public async Task<RequestResponse> AddSubject(Subject req)
        {
            try
            {
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectCode.Equals(req.SubjectCode) && x.IsDeleted != true);

                if (data == null)
                {
                    await this.DBcontext.Subjects.AddAsync(req);

                    await this.DBcontext.SaveChangesAsync();

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Add Subject Successfully",
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

        public async Task<RequestResponse> DeleteSubject(int subjectId)
        {
            try
            {
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == subjectId && x.IsDeleted != true);

                if (data != null)
                {
                    data.IsDeleted = true;

                    await this.DBcontext.SaveChangesAsync();

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Delete Subject Successfully",
                    };
                }
                else
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Subject not exist",
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

        public async Task<ResultResponse<Subject>> GetSubjectById(int subjectId)
        {
            try
            {
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == subjectId && x.IsDeleted != true);

                return new ResultResponse<Subject>
                {
                    IsSuccessful = data != null ? true : false,
                    Message = data != null ? string.Empty : "Cannot found subject",
                    Item = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<Subject>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<Subject>> GetSubjects()
        {
            try
            {
                var data = await this.DBcontext.Subjects.Where(x => x.IsDeleted != true).ToListAsync();

                return new ResultResponse<Subject>
                {
                    IsSuccessful = data != null ? true : false,
                    Message = data != null ? string.Empty : "Cannot found subject",
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<Subject>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<RequestResponse> UpdateSubject(Subject req)
        {
            try
            {
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == req.SubjectId && x.IsDeleted != true);

                if (data != null)
                {
                    if (data.SubjectCode != req.SubjectCode)
                    {
                        var existingSubjectWithSameCode = await this.DBcontext.Subjects
                            .AnyAsync(x => x.SubjectCode == req.SubjectCode && x.SubjectId != req.SubjectId && x.IsDeleted != true);

                        if (existingSubjectWithSameCode)
                        {
                            return new RequestResponse
                            {
                                IsSuccessful = false,
                                Message = "SubjectCode already exists.",
                            };
                        }
                    }

                    data.SubjectCode = req.SubjectCode;
                    data.SubjectName = req.SubjectName;

                    await this.DBcontext.SaveChangesAsync();

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Subject updated successfully",
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

        public async Task<ResultResponse<SubjectResponse>> GetSubjectByRole(int roleId, int userId,int campusId)
        {
            try
            {
                var data = (from s in this.DBcontext.Subjects
                            join cus in this.DBcontext.CampusUserSubjects on s.SubjectId equals cus.SubjectId into subjectJoin
                            from cus in subjectJoin.DefaultIfEmpty()
                            where
                            (roleId == 4 && (
                                // Điều kiện 1: Các môn mà UserId = X đang là chủ nhiệm tại campus của họ
                                (cus != null && cus.UserId == userId && cus.IsLecturer == false && cus.CampusId == campusId)
                                // Điều kiện 2: Hoặc các môn chưa có chủ nhiệm tại campus của user X
                                || (cus == null || !this.DBcontext.CampusUserSubjects
                                    .Any(other => other.SubjectId == s.SubjectId && other.IsLecturer == false && other.CampusId == campusId))
                            ))
                            // Nếu RoleId = 3, lấy tất cả môn học
                            || roleId == 3
                            select new SubjectResponse
                            {
                                SubjectId = s.SubjectId,
                                SubjectCode = s.SubjectCode,
                                SubjectName = s.SubjectName
                            }).Distinct().ToList();


                return new ResultResponse<SubjectResponse>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<SubjectResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
