using Library.Common;
using Library.Models;
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
        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<ResultResponse<User>> GetAllAsync()
        {
            var data = await dbContext.Users
            .Include("Campus").Include("UserRole")
            .ToListAsync();

            return new ResultResponse<User>
            {
                IsSuccessful = true,
                Items = data,
            };
        }

        public async Task<ResultResponse<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null)
        {
            var users = dbContext.Users.Include(u => u.Campus).Include(u => u.UserRole).AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Mail", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(x => x.Mail.Contains(filterQuery));
                }
            }
            var userList = await users.ToListAsync();
            return new ResultResponse<User>
            {
                IsSuccessful = true,
                Items = userList,
            };
        }

        public async Task<List<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var users = dbContext.Users.Include(u => u.Campus).Include(u => u.UserRole).AsQueryable();

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

        public async Task<RequestResponse> UpdateAsync(User user)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.Mail = user.Mail;
            existingUser.RoleId = user.RoleId;
            existingUser.CampusId = user.CampusId;
            existingUser.IsActive = user.IsActive;

            await dbContext.SaveChangesAsync();
            return new RequestResponse
            {
                IsSuccessful = true,
                Message = " update success "
            };
        }

        Task<RequestResponse> IUserRepository.CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        async Task<RequestResponse> IUserRepository.DeleteAsync(int id)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (existingUser == null)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = "Fail"
                };
            }
            dbContext.Users.RemoveRange(existingUser);
            await dbContext.SaveChangesAsync();
            return new RequestResponse
            {
                IsSuccessful = true,
                Message = " update success "
            };
        }

        async Task<ResultResponse<User>> IUserRepository.GetByIdAsync(int id)
        {
            var data = await dbContext.Users
           .Include("Campus")
           .Include("UserRole")
           .FirstOrDefaultAsync(x => x.UserId == id);
            return new ResultResponse<User>
            {
                IsSuccessful = true,
                Item = data,
            };
        }

        async Task<RequestResponse> IUserRepository.UpdateAsync(User user)
        {
            RequestResponse response = new RequestResponse();
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
            if (existingUser == null)
            {
                return null;

            }
            existingUser.Mail = user.Mail;
            existingUser.RoleId = user.RoleId;
            existingUser.CampusId = user.CampusId;
            existingUser.IsActive = user.IsActive;

            await dbContext.SaveChangesAsync();
            response.IsSuccessful = true;

            response.Message = "Register successfuly";
            return new RequestResponse
            {
                IsSuccessful = true,
                Message = response.Message
            };
        }
    }
}
