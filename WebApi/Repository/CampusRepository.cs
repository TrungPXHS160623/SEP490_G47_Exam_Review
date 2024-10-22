using Library.Common;
using Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class CampusRepository : ICampusRepository
    {
        private readonly QuizManagementContext DBcontext;
        private readonly ILogHistoryRepository logRepository;

        public CampusRepository(QuizManagementContext DBcontext, ILogHistoryRepository logRepository)
        {
            this.DBcontext = DBcontext;
            this.logRepository = logRepository;
        }

        public async Task<RequestResponse> AddCampus(Campus req)
        {
            try
            {
                var data = await this.DBcontext.Campuses.FirstOrDefaultAsync(x => x.CampusName.Equals(req.CampusName));

                if (data != null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Campus alerady exist",
                    };
                }
                else
                {
                    req.IsDeleted = false;

                    await this.DBcontext.Campuses.AddAsync(req);

                    await this.DBcontext.SaveChangesAsync();

                    await logRepository.LogAsync("Add new campus");

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Add Successfully",
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

        public async Task<RequestResponse> DeleteCampus(int campusId)
        {
            try
            {
                var data = await this.DBcontext.Campuses.FirstOrDefaultAsync(x => x.CampusId == campusId);

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
                    this.DBcontext.Campuses.Remove(data);

                    await this.DBcontext.SaveChangesAsync();

                    await logRepository.LogAsync($"Delete Campus {data.CampusName}");

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

        public async Task<ResultResponse<Campus>> GetCampus()
        {
            try
            {
                var data = await this.DBcontext.Campuses.ToListAsync();

                if (data != null)
                {
                    return new ResultResponse<Campus>
                    {
                        IsSuccessful = true,
                        Items = data,
                    };
                } else
                {
                    return new ResultResponse<Campus>
                    {
                        IsSuccessful = false,
                        Message = "There is no campus",
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResultResponse<Campus>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<Campus>> GetCampusById(int campusId)
        {
            try
            {
                var data = await this.DBcontext.Campuses.FirstOrDefaultAsync(x => x.CampusId == campusId);

                if (data != null)
                {
                    return new ResultResponse<Campus>
                    {
                        IsSuccessful = true,
                        Item = data,
                    };
                }
                else
                {
                    return new ResultResponse<Campus>
                    {
                        IsSuccessful = false,
                        Message = "Campus not found",
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResultResponse<Campus>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<RequestResponse> UpdateCampus(Campus req)
        {
            try
            {
                var data = await this.DBcontext.Campuses.FirstOrDefaultAsync(x => x.CampusId == req.CampusId);

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
                    data.CampusName = req.CampusName;
                    data.UpdateDate = DateTime.Now;
                    data.IsDeleted = req.IsDeleted;

                    await this.DBcontext.SaveChangesAsync();

                    await logRepository.LogAsync($"Update campus {req.CampusName}");

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Update Successfuly",
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
