using Azure;
using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly QuizManagementContext dbContext;

        public UserRepository(QuizManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<RequestResponse> CreateAsync(UserRequest user)
        {
            try
            {
                var data = await this.dbContext.Users.FirstOrDefaultAsync(x => x.Mail.Equals(user.Email));

                if (data != null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Mail already exist!"
                    };
                }
                else
                {
                    var newUser = new User
                    {
                        Mail = user.Email+"@fpt.edu.vn",
                        RoleId = user.RoleId,
                        CampusId = user.CampusId,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        IsActive = user.IsActive.Value,
                    };
                    await dbContext.Users.AddAsync(newUser);
                    await dbContext.SaveChangesAsync();
                }


                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Create account successfully!"
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

        public async Task<ResultResponse<UserResponse>> GetAllAsync()
        {
            try
            {
                var data = (from u in this.dbContext.Users
                            join c in this.dbContext.Campuses on u.CampusId equals c.CampusId into campusJoin
                            from c in campusJoin.DefaultIfEmpty() // Left join for Campuses
                            join r in this.dbContext.UserRoles on u.RoleId equals r.RoleId into roleJoin
                            from r in roleJoin.DefaultIfEmpty() // Left join for UserRoles
                            select new UserResponse
                            {
                                Email = u.Mail,
                                CampusName = c != null ? c.CampusName : null, // Handle possible null from left join
                                IsActive = u.IsActive,
                                RoleName = r != null ? r.RoleName : null,     // Handle possible null from left join
                                UserId = u.UserId,
                                UpdateDt = u.UpdateDate,
                            }).ToList();

                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = true,
                    Items = data.OrderByDescending(x => x.UpdateDt).ToList(),
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetUserForAdmin(string filterQuery)
        {
            var data = (from u in this.dbContext.Users
                        join c in this.dbContext.Campuses on u.CampusId equals c.CampusId into campusJoin
                        from c in campusJoin.DefaultIfEmpty() // Left join for Campuses
                        join r in this.dbContext.UserRoles on u.RoleId equals r.RoleId into roleJoin
                        from r in roleJoin.DefaultIfEmpty() // Left join for UserRoles
                        where (string.IsNullOrEmpty(filterQuery) || u.Mail.Contains(filterQuery))
                        && (u.RoleId == 1 || u.RoleId == 2)
                        select new UserResponse
                        {
                            Email = u.Mail,
                            CampusName = c != null ? c.CampusName : null, // Handle possible null from left join
                            IsActive = u.IsActive,
                            RoleName = r != null ? r.RoleName : null,     // Handle possible null from left join
                            UserId = u.UserId,
                            UpdateDt = u.UpdateDate,
                        }).ToList();

            return new ResultResponse<UserResponse>
            {
                IsSuccessful = true,
                Items = data.OrderByDescending(x => x.UpdateDt).ToList(),
            };
        }

        public async Task<ResultResponse<UserResponse>> GetUserForExaminer(string filterQuery)
        {
            var data = (from u in this.dbContext.Users
                        join c in this.dbContext.Campuses on u.CampusId equals c.CampusId into campusJoin
                        from c in campusJoin.DefaultIfEmpty() // Left join for Campuses
                        join r in this.dbContext.UserRoles on u.RoleId equals r.RoleId into roleJoin
                        from r in roleJoin.DefaultIfEmpty() // Left join for UserRoles
                        where (string.IsNullOrEmpty(filterQuery) || u.Mail.Contains(filterQuery))
                        && (u.RoleId != 1 && u.RoleId != 2)
                        select new UserResponse
                        {
                            Email = u.Mail,
                            CampusName = c != null ? c.CampusName : null, // Handle possible null from left join
                            IsActive = u.IsActive,
                            RoleName = r != null ? r.RoleName : null,     // Handle possible null from left join
                            UserId = u.UserId,
                            UpdateDt = u.UpdateDate,
                        }).ToList();

            return new ResultResponse<UserResponse>
            {
                IsSuccessful = true,
                Items = data.OrderByDescending(x => x.UpdateDt).ToList(),
            };
        }

        public async Task<List<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var users = dbContext.Users.Include(u => u.Campus).Include(u => u.Role).AsQueryable();

            // Kiểm tra filter
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Mail", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(x => x.Mail.Contains(filterQuery));
                }
            }

            // Kiểm tra sortBy
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Mail", StringComparison.OrdinalIgnoreCase))
                {
                    users = isAscending ? users.OrderBy(x => x.Mail) : users.OrderByDescending(x => x.Mail);
                }
            }
            return await users.ToListAsync();
        }

        public async Task<RequestResponse> DeleteAsync(int id)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (existingUser == null)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = "Account no exist"
                };
            }
            dbContext.Users.RemoveRange(existingUser);
            await dbContext.SaveChangesAsync();
            return new RequestResponse
            {
                IsSuccessful = true,
                Message = " Delete success "
            };
        }

        public async Task<ResultResponse<UserRequest>> GetByIdAsync(int id)
        {
            var data = (from u in this.dbContext.Users
                        join c in this.dbContext.Campuses on u.CampusId equals c.CampusId into campusJoin
                        from c in campusJoin.DefaultIfEmpty() // Left join for Campuses
                        join r in this.dbContext.UserRoles on u.RoleId equals r.RoleId into roleJoin
                        from r in roleJoin.DefaultIfEmpty() // Left join for UserRoles
                        where u.UserId == id
                        select new UserRequest
                        {
                            Email = u.Mail.Replace("@fpt.edu.vn",string.Empty),
                            CampusId = u.CampusId,                        // Keep the CampusId from the Users table
                            CampusName = c != null ? c.CampusName : null, // Handle possible null from left join
                            IsActive = u.IsActive,
                            RoleId = u.RoleId,                            // Keep the RoleId from the Users table
                            RoleName = r != null ? r.RoleName : null,     // Handle possible null from left join
                            UserId = u.UserId,
                        }).FirstOrDefault();
            return new ResultResponse<UserRequest>
            {
                IsSuccessful = true,
                Item = data,
            };
        }

        public async Task<RequestResponse> UpdateAsync(UserRequest user)
        {
            try
            {
                RequestResponse response = new RequestResponse();

                var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
                if (existingUser == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "User not exist"
                    };
                }
                existingUser.Mail = user.Email + "@fpt.edu.vn";
                existingUser.RoleId = user.RoleId;
                existingUser.CampusId = user.CampusId;
                existingUser.IsActive = user.IsActive.Value;

                await dbContext.SaveChangesAsync();
                response.IsSuccessful = true;

                response.Message = "Update account successfuly";
                return response;
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetHeadOfDepartment(int subjectId, int campusId)
        {
            try
            {
                var user = await (from u in dbContext.Users
                            join cus in dbContext.CampusUserSubjects on u.UserId equals cus.UserId
                            join s in dbContext.Subjects on cus.SubjectId equals s.SubjectId
                            join c in dbContext.Campuses on cus.CampusId equals c.CampusId
                            where s.SubjectId == subjectId && c.CampusId == campusId
                            select new UserResponse
                            {
                                Email = u.Mail,
                                UserId = u.UserId,
                            }).FirstOrDefaultAsync();
                
                if(user == null)
                {
                    return new ResultResponse<UserResponse>
                    {
                        IsSuccessful = true,
                        Message = "Cannot find Head of Department.",
                    };
                }

                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = true,
                    Item = user
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetLecture()
        {
            try
            {
                var data = (from u in this.dbContext.Users
                            join c in this.dbContext.Campuses on u.CampusId equals c.CampusId into campusJoin
                            from c in campusJoin.DefaultIfEmpty() // Left join for Campuses
                            join r in this.dbContext.UserRoles on u.RoleId equals r.RoleId into roleJoin
                            from r in roleJoin.DefaultIfEmpty() // Left join for UserRoles
                            where r.RoleId == 4
                            select new UserResponse
                            {
                                Email = u.Mail,
                                CampusName = c != null ? c.CampusName : null, // Handle possible null from left join
                                IsActive = u.IsActive,
                                RoleName = r != null ? r.RoleName : null,     // Handle possible null from left join
                                UserId = u.UserId,
                                UpdateDt = u.UpdateDate,
                            }).ToList();

                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = true,
                    Items = data.OrderByDescending(x => x.UpdateDt).ToList(),
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
