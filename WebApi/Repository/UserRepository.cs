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

        public async Task<User?> DeleteAsync(int id)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (existingUser == null)
            {
                return null;
            }
            dbContext.Users.Remove(existingUser);
            dbContext.SaveChangesAsync();
            return existingUser;

        }

        public async Task<List<User>> GetAllAsync()
        {
            return await dbContext.Users
            .Include("Campus").Include("UserRole")
            .ToListAsync();
        }

        public async Task<List<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null)
        {
            var users = dbContext.Users.Include(u => u.Campus).Include(u => u.UserRole).AsQueryable();

            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                //if (filterOn.Equals("CampusName", StringComparison.OrdinalIgnoreCase))
                //{
                //    users = users.Where(x => x.Campus.CampusName.Contains(filterQuery));
                //}
                if (filterOn.Equals("Mail", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(x => x.Mail.Contains(filterQuery));
                }
            }


            return await users.ToListAsync();
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


        public async Task<User?> GetByIdAsync(int id)
        {
            return await dbContext.Users
           .Include("Campus")
           .Include("UserRole")
           .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<User?> UpdateAsync(int id, User user)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (existingUser == null)
            {
                return null;

            }
            existingUser.Mail = user.Mail;
            existingUser.RoleId = user.RoleId;
            existingUser.CampusId = user.CampusId;
            existingUser.IsActive = user.IsActive;

            await dbContext.SaveChangesAsync();
            return existingUser;
        }
    }
}
