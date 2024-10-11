using Library.Common;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class CampusRepository : ICampusRepository
    {
        private readonly QuizManagementContext DBcontext;

        public CampusRepository(QuizManagementContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }

        public async Task<RequestResponse> AddCampus(Campus req)
        {
            try
            {
                var data = await this.DBcontext.Campuses.FirstOrDefaultAsync(x => x.CampusName.Equals(req.CampusName) && x.IsDeleted != true);

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
                    await this.DBcontext.Campuses.AddAsync(req);

                    await this.DBcontext.SaveChangesAsync();

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
                var data = await this.DBcontext.Campuses.FirstOrDefaultAsync(x => x.CampusId == campusId && x.IsDeleted != true);

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
                    data.IsDeleted = true;

                    await this.DBcontext.SaveChangesAsync();

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Delete Successfully",
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
                var data = await this.DBcontext.Campuses.Where(x => x.IsDeleted != true).ToListAsync();

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
                var data = await this.DBcontext.Campuses.FirstOrDefaultAsync(x => x.CampusId == campusId && x.IsDeleted != true);

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
                var data = await this.DBcontext.Campuses.FirstOrDefaultAsync(x => x.CampusId == req.CampusId && x.IsDeleted != true);

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

                    await this.DBcontext.SaveChangesAsync();

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
